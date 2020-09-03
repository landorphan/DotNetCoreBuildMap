namespace Landorphan.Abstractions.FileSystem.Paths.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Converters;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;
    using Newtonsoft.Json;

    [JsonConverter(typeof(ParsedPathConverter))]
    public abstract class ParsedPath : IPath //, IYamlConvertible
    {
        internal SimplificationLevel Simplification;
        internal IPath SimplifiedForm;
        internal IPath SuppliedForm;

        public object Clone()
        {
            return DeepClone();
        }

        public IPath DeepClone()
        {
            var clones = CloneSegments(Segments);
            return CreateFromSegments(clones);
        }

        public abstract PathAnchor Anchor { get; }

        public string Extension => SimplifiedForm.TrailingSegment.Extension;

        public bool HasExtension => SimplifiedForm.TrailingSegment.HasExtension;

        public bool IsDiscouraged => (
                                         from s in Segments
                                         where s.IsDiscouraged()
                                         select s).Any() &&
                                     Status == PathStatus.Legal;

        public bool IsFullyQualified => SimplifiedForm.LeadingSegment.SegmentType == SegmentType.RootSegment ||
                                        SimplifiedForm.LeadingSegment.SegmentType == SegmentType.VolumeRelativeSegment ||
                                        SimplifiedForm.LeadingSegment.SegmentType == SegmentType.RemoteSegment;

        public ISegment LeadingSegment { get; private set; }

        public string Name => SimplifiedForm.TrailingSegment.Name;

        public string NameWithoutExtension => SimplifiedForm.TrailingSegment.NameWithoutExtension;

        public abstract PathType PathType { get; }

        public ISegment RootSegment => SimplifiedForm.LeadingSegment.IsRootSegment ? SimplifiedForm.LeadingSegment : Segment.GetEmptySegment(PathType);

        public IReadOnlyList<ISegment> Segments { get; private set; }

        public SerializationForm SerializationMethod { get; set; } = PathUtilities.DefaultSerializationMethod;

        public SimplificationLevel SimplificationLevel => Simplification;

        public PathStatus Status
        {
            get
            {
                if (this == SimplifiedForm)
                {
                    var loc = 0;
                    if ((
                        from s in Segments
                        where !s.IsLegalForSegmentOffset(loc++)
                        select s).Any())
                    {
                        return PathStatus.Illegal;
                    }

                    return PathStatus.Legal;
                }

                return SimplifiedForm.Status;
            }
        }

        public IPath SuppliedPath { get; private set; }

        public string SuppliedPathString { get; private set; }

        public ISegment TrailingSegment { get; private set; }

        public IPath AppendSegmentAfter(int offset, ISegment segment)
        {
            return AddToSegments(offset, true, segment);
        }

        public IPath AppendSegmentAtEnd(ISegment segment)
        {
            return AddToSegments(Segments.Count, true, segment);
            //if (segment == null)
            //{
            //    segment = Segment.GetEmptySegment(PathType);
            //}
            //var clones = CloneSegments(this.Segments);
            //clones.Add(segment.DeepClone());
            //return CreateFromSegments(clones);
        }

        public IPath ChangeExtension(string newExtension)
        {
            if (newExtension == null)
            {
                newExtension = string.Empty;
            }

            if (newExtension.Length == 1 && newExtension[0] == '.')
            {
                newExtension = string.Empty;
            }
            else if (newExtension.Length > 1 && newExtension[0] == '.')
            {
                newExtension = newExtension.Substring(1);
            }

            string newName;
            if (string.IsNullOrWhiteSpace(newExtension))
            {
                newName = NameWithoutExtension;
            }
            else
            {
                newName = string.Join(".", NameWithoutExtension, newExtension);
            }

            return ReplaceSegment(Segments.Count - 1, CreateSegment(TrailingSegment.SegmentType, newName));
        }

        public IPath ConvertToRelativePath()
        {
            var path = (ParsedPath)Simplify();
            while (path.Anchor == PathAnchor.Absolute)
            {
                if (path.Segments.Count > 1)
                {
                    path = CreateFromSegments(path.Segments.Skip(1));
                }
                else
                {
                    path = CreateFromSegments(new[] {(Segment)Segment.GetSelfSegment(path.PathType)});
                }
            }

            return path;
        }

        public abstract ISegment CreateSegment(SegmentType segmentType, string name);

        public IPath GetParent()
        {
            var parentPath = AppendSegmentAtEnd(Segment.GetParentSegment(PathType));
            return parentPath.Simplify();
        }

        public IPath InsertSegmentAtBeginning(ISegment segment)
        {
            return AddToSegments(0, false, segment);
        }

        public IPath InsertSegmentBefore(int offset, ISegment segment)
        {
            return AddToSegments(offset, false, segment);
        }

        public IPath Join(IPath other)
        {
            if (ReferenceEquals(null, other))
            {
                return AppendSegmentAtEnd(Segment.GetNullSegment(PathType));
            }

            var clones = CloneSegments(Segments);
            clones.AddRange(other.Segments.Select(s => s.DeepClone()));
            return CreateFromSegments(clones);
        }

        public IPath ReplaceSegment(int offset, ISegment segment)
        {
            var clonedSegments = CloneSegments(Segments).ToArray();
            clonedSegments[offset] = segment;
            return CreateFromSegments(clonedSegments);
        }

        public IPath Simplify()
        {
            return SimplifiedForm;
        }

        public string ToPathSegmentNotation()
        {
            var builder = new StringBuilder();
            builder.Append(PathSegmentNotationComponents.OpenBracket);
            builder.Append(PathSegmentNotationComponents.PathSegmentNotationHeader);
            builder.Append(PathSegmentNotationComponents.Colon);
            if (PathType == PathType.Windows)
            {
                builder.Append(PathSegmentNotationComponents.WindowsPathType);
            }
            else
            {
                builder.Append(PathSegmentNotationComponents.PosixPathType);
            }

            builder.Append(PathSegmentNotationComponents.CloseBracket);
            foreach (var segment in Segments)
            {
                builder.Append(PathSegmentNotationComponents.ForwardSlash);
                builder.Append(segment.ToPathSegmentNotation());
            }

            return builder.ToString();
        }

        public override string ToString()
        {
            return ConvertToString(Segments);
        }

        internal static IPath CreateFromSegments(PathType pathType, string suppliedPath, IEnumerable<ISegment> segments)
        {
            ParsedPath retval;
            if (PathType.Windows == pathType)
            {
                retval = new WindowsPath();
            }
            else
            {
                retval = new PosixPath();
            }

            retval.InitializeFromSuppliedPathAndSegments(suppliedPath, segments);
            return retval;
        }

        internal static IPath CreateSimplifiedForm(IPath suppliedPath)
        {
            ParsedPath retval;
            if (PathType.Windows == suppliedPath.PathType)
            {
                retval = new WindowsPath();
            }
            else
            {
                retval = new PosixPath();
            }

            var simplifiedSegments = SimplifySegments(suppliedPath.PathType, suppliedPath.Segments.Cast<Segment>().ToArray());

            retval.Segments = TraverseSegmentChain(suppliedPath.PathType, simplifiedSegments);
            retval.LeadingSegment = retval.Segments[0];
            retval.TrailingSegment = retval.Segments[retval.Segments.Count - 1];
            retval.SuppliedForm = suppliedPath;
            retval.SuppliedPathString = suppliedPath.SuppliedPathString;
            retval.SuppliedPath = suppliedPath;
            retval.SimplifiedForm = retval;
            retval.Simplification = GetSimplificationLevel(retval);
            return retval;
        }

        internal static ISegment[] SimplifySegments(PathType pathType, ISegment[] segments)
        {
            var stack = new Stack<ISegment>(segments);
            var result = new Stack<ISegment>();

            var popDepth = 0;
            Func<bool> ShouldDiscard = () =>
            {
                if (popDepth > 0)
                {
                    popDepth--;
                    return true;
                }

                return false;
            };

            ISegment forcedRoot = null;
            ISegment current;
            while (stack.Count > 0)
            {
                current = stack.Pop();
                switch (current.SegmentType)
                {
                    // Any Device Segment anywhere in the path resolves immediately to
                    // that device and causes all other segments to be irrelevant.
                    case SegmentType.DeviceSegment:
                        return new[] {current};

                    // Handle all Absolute Segment types
                    // "True" Absolute segments must be the first segment.
                    // If this is a "true" Fully Qualified segment, It can not be removed
                    // If however, this is not the first segment, then it is treated as 
                    // just a generic segment which can be removed.
                    case SegmentType.RemoteSegment:
                    case SegmentType.RootSegment:
                    case SegmentType.VolumelessRootSegment:
                        if (stack.Count == 0)
                        {
                            // If there is no count, this is a root segment ... 
                            // keep the segment and throw out the popDepth.
                            popDepth = 0;
                            result.Push(current);
                        }
                        else if (!ShouldDiscard())
                        {
                            // If there is no pop depth, keep the segment 
                            // It will be considered a "generic" segment for 
                            // other purposes after this.
                            result.Push(current);
                        }

                        break;

                    // Special case Vol Rel Segment
                    // If this is a true "Fully Qualified" segment, 
                    // It can not be removed.  However, It can still be affected by 
                    // parent segments.
                    case SegmentType.VolumeRelativeSegment:
                        if (!ShouldDiscard())
                        {
                            if (stack.Count == 0)
                            {
                                forcedRoot = current;
                            }
                            else
                            {
                                result.Push(current);
                            }
                        }

                        break;

                    // Parents simply increase the "pop" count.
                    case SegmentType.ParentSegment:
                        popDepth++;
                        break;

                    // Self segments to resolve:
                    // Self segments can safely be removed.  The rare case where it can't
                    // be removed (the case of a SelfReferenceOnly path) is handled below
                    // by converting any "empty" set of segments into a Self reference only.
                    // HOWEVER: These segment types never alter the popDepth (they 
                    // always pop).
                    case SegmentType.NullSegment:
                    case SegmentType.EmptySegment:
                    case SegmentType.SelfSegment:
                        //if (stack.Count == 0)
                        //{
                        //    result.Push((Segment) Segment.GetSelfSegment(pathType));
                        //}
                        break;
                    // case SegmentType.GenericSegment:
                    default:
                        if (!ShouldDiscard())
                        {
                            result.Push(current);
                        }

                        break;
                }
            }

            for (var i = 0; i < popDepth; i++)
            {
                if (pathType == PathType.Posix)
                {
                    result.Push(PosixSegment.ParentSegment);
                }
                else
                {
                    result.Push(WindowsSegment.ParentSegment);
                }
            }

            if (forcedRoot != null)
            {
                result.Push(forcedRoot);
            }

            if (result.Count == 0)
            {
                if (pathType == PathType.Posix)
                {
                    result.Push(PosixSegment.SelfSegment);
                }
                else
                {
                    result.Push(WindowsSegment.SelfSegment);
                }
            }

            return result.ToArray();
        }

        internal IPath AddToSegments(int offset, bool after, ISegment segment)
        {
            if (after)
            {
                offset++;
            }

            var newSegments = new List<ISegment>();
            if (segment == null)
            {
                segment = Segment.GetEmptySegment(PathType);
            }

            var clones = CloneSegments(Segments);

            newSegments.AddRange(clones.Take(offset));
            newSegments.Add(segment);
            newSegments.AddRange(clones.Skip(offset));

            return CreateFromSegments(newSegments);
        }

        protected abstract string ConvertToString(IEnumerable<ISegment> segments);

        private static List<ISegment> CloneSegments(IEnumerable<ISegment> originalSegments)
        {
            var segments = new List<ISegment>(
                from s in originalSegments
                select (Segment)s.DeepClone());
            return segments;
        }

        private static SimplificationLevel GetSimplificationLevel(IPath path)
        {
            var retval = SimplificationLevel.Fully;

            // This is a special case where the leading self reference can't be removed because 
            // it would result in an empty path ... so in this case it's as normalized as possible 
            if (path.Segments.Count == 1 && path.Segments[0].SegmentType == SegmentType.SelfSegment)
            {
                return SimplificationLevel.SelfReferenceOnly;
            }

            var leadingParentsConsumed = false;
            foreach (var segment in path.Segments)
            {
                switch (segment.SegmentType)
                {
                    case SegmentType.ParentSegment:
                        if (leadingParentsConsumed)
                        {
                            return SimplificationLevel.NotNormalized;
                        }

                        retval = SimplificationLevel.LeadingParentsOnly;
                        continue;
                    case SegmentType.EmptySegment:
                    case SegmentType.NullSegment:
                    case SegmentType.SelfSegment:
                        return SimplificationLevel.NotNormalized;
                    default:
                        leadingParentsConsumed = true;
                        continue;
                }
            }

            return retval;
        }

        private static Segment[] TraverseSegmentChain(PathType pathType, IEnumerable<ISegment> segments)
        {
            var retval = new List<Segment>();
            var selfSegment = pathType == PathType.Windows ? WindowsSegment.SelfSegment : (Segment)PosixSegment.SelfSegment;
            if (segments == null ||
                !segments.Any() ||
                segments.Count() == 1 && (segments.First().SegmentType == SegmentType.NullSegment || segments.First().SegmentType == SegmentType.EmptySegment))
            {
                segments = new[] {(Segment)selfSegment.DeepClone()};
            }

            foreach (var segment in segments)
            {
                var newSegment = (Segment)segment.DeepClone();
                retval.Add(newSegment);
            }

            return retval.ToArray();
        }

        private ParsedPath CreateFromSegments(IEnumerable<ISegment> segments)
        {
            var pathString = ConvertToString(CloneSegments(segments).ToArray());
            return (ParsedPath)CreateFromSegments(PathType, pathString, segments.Cast<Segment>().ToArray());
        }

        private void InitializeFromSuppliedPathAndSegments(string suppliedPath, IEnumerable<ISegment> segments)
        {
            segments = TraverseSegmentChain(PathType, segments);
            Segments = segments.ToArray();
            LeadingSegment = Segments[0];
            TrailingSegment = segments.Last();
            SuppliedForm = this;
            SuppliedPathString = suppliedPath;
            SuppliedPath = this;
            SimplifiedForm = CreateSimplifiedForm(this);
            Simplification = GetSimplificationLevel(this);
        }

        //public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        //{
        //    var value = parser.Consume<Scalar>().Value;
        //    IPathParser pathParser = new PathParser();
        //    var tempPath = pathParser.Parse(value);
        //    this.PathType = tempPath.PathType;
        //    InitializeFromSuppliedPathAndSegments(value, tempPath.Segments);
        //}

        //public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        //{
        //    emitter.Emit(new Scalar(null, null, this.ToString(),
        //        ScalarStyle.Any, true, false));
        //}
    }
}
