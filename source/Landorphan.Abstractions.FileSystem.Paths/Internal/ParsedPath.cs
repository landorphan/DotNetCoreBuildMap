namespace Landorphan.Abstractions.FileSystem.Paths.Internal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Transactions;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;

    public abstract class ParsedPath : IPath
    {
        // Used by transition functions
        internal static IPath CreateFromPathAndSegments(IPath suppliedPath, Segment[] segments)
        {
            return CreateFromPathAndSegments(suppliedPath.PathType, suppliedPath, segments);
        }

        internal static IPath CreateFromPathAndSegments(PathType pathType, IPath suppliedPath, Segment[] segments)
        {
            segments = TraverseSegmentChain(pathType, segments);
            ParsedPath retval = null;
            if (Paths.PathType.Windows == pathType)
            {
                retval = new WindowsPath();
            }
            else
            {
                retval = new PosixPath();
            }
            retval.Segments = segments;
            retval.LeadingSegment = segments[0];
            retval.status = PathStatus.Undetermined;
            retval.SetStatus();
            retval.suppliedForm = retval;
            retval.SuppliedPathString = suppliedPath.SuppliedPathString;
            retval.SuppliedPath = suppliedPath;
            retval.simplifiedForm = CreateSimplifiedForm(retval);
            retval.simplificationLevel = GetSimplificationLevel(retval);
            return retval;
        }

        internal IPath suppliedForm;
        internal IPath simplifiedForm;
        internal NormalizationLevel simplificationLevel;

        internal static IPath CreateFromSegments(PathType pathType, string suppliedPath, Segment[] segments)
        {
            segments = TraverseSegmentChain(pathType, segments);

            ParsedPath retval = null;
            if (Paths.PathType.Windows == pathType)
            {
                retval = new WindowsPath();
            }
            else
            {
                retval = new PosixPath();
            }

            retval.Segments = segments;
            retval.LeadingSegment = segments[0];
            retval.TrailingSegment = segments.Last();
            retval.status = PathStatus.Undetermined;
            retval.SetStatus();
            retval.suppliedForm = retval;
            retval.SuppliedPathString = suppliedPath;
            retval.SuppliedPath = retval;
            retval.simplifiedForm = CreateSimplifiedForm(retval);
            retval.simplificationLevel = GetSimplificationLevel(retval);
            return retval;
        }

        internal static IPath CreateSimplifiedForm(IPath suppliedPath)
        {
            ParsedPath retval = null;
            if (PathType.Windows == suppliedPath.PathType)
            {
                retval = new WindowsPath();
            }
            else
            {
                retval = new PosixPath();
            }

            var simplifiedSegments = SimplifySegments(suppliedPath.PathType, suppliedPath.Segments.Cast<Segment>().ToArray());
//            var simplifiedSegments = NormalizeSegments(suppliedPath.PathType, suppliedPath.Segments.Cast<Segment>().ToArray());

            retval.Segments = TraverseSegmentChain(suppliedPath.PathType, simplifiedSegments);
            retval.LeadingSegment = retval.Segments[0];
            retval.SetStatus();
            retval.suppliedForm = suppliedPath;
            retval.SuppliedPathString = suppliedPath.SuppliedPathString;
            retval.SuppliedPath = suppliedPath;
            retval.simplifiedForm = retval;
            retval.simplificationLevel = GetSimplificationLevel(retval);
            return retval;
        }

        private static Segment[] TraverseSegmentChain(PathType pathType, Segment[] segments)
        {
            List<Segment> retval = new List<Segment>();
            Segment selfSegment = pathType == Paths.PathType.Windows ? (Segment)WindowsSegment.SelfSegment : (Segment)PosixSegment.SelfSegment;
            if (segments == null || segments.Length == 0 || 
                (segments.Length == 1 && (segments[0].SegmentType == SegmentType.NullSegment || segments[0].SegmentType == SegmentType.EmptySegment)))
            {
                segments = new[] { (Segment) selfSegment.Clone() };
            }

            Segment lastSegment = null;
            foreach (var segment in segments)
            {
                Segment newSegment = (Segment) segment.Clone();
                //if (lastSegment != null)
                //{
                //    lastSegment.NextSegment = newSegment;
                //}
                //newSegment.LastSegment = lastSegment;
                //lastSegment = newSegment;
                retval.Add(newSegment);
            }

            return retval.ToArray();
        }

        protected abstract void SetStatus();

        public String SuppliedPathString { get; private set; }
        public ISegment LeadingSegment { get; private set; }
        public ISegment TrailingSegment { get; private set; }
        public PathStatus status;

        public PathStatus Status
        {
            get 
            { 
                if (this == simplifiedForm)
                {
                    int loc = 0;
                    if ((from s in Segments
                         where !s.IsLegalForSegmentOffset(loc++)
                         select s).Any())
                    {
                        return PathStatus.Illegal;
                    }

                    return PathStatus.Legal;
                    //if (status == PathStatus.Discouraged)
                    //    return PathStatus.Legal;
                    //return status;
                }

                return simplifiedForm.Status;
            }
        }

        public bool IsDiscouraged => (from s in Segments 
                                     where s.IsDiscouraged()
                                    select s).Any() && (Status == PathStatus.Legal || Status == PathStatus.Discouraged);

        public abstract PathType PathType { get; }
        public ISegment[] Segments { get; private set; }
        public abstract PathAnchor Anchor { get; }
        public IPath SuppliedPath { get; private set; }

        public IPath Parent
        {
            get
            {
                var normalized = this.Normalize();
                var segments = CloneSegments(normalized);
                segments.Add((Segment)Segment.GetParentSegment(this.PathType));
                var parentTempPath = CreateFromPathAndSegments(this, segments.ToArray());
                parentTempPath = CreateFromSegments(this.PathType, parentTempPath.ToString(), segments.ToArray());
                return parentTempPath.Normalize();

                //var normalized = this.Normalize();
                //List<Segment> segments = new List<Segment>(
                //    (from s in normalized.Segments
                //        select (Segment) s.Clone()));
                //if (normalized.NormalizationLevel == NormalizationLevel.SelfReferenceOnly)
                //{
                //    return CreateFromPathAndSegments(this, new[] {parent});
                //}
                //if (normalized.NormalizationLevel == NormalizationLevel.LeadingParentsOnly)
                //{
                //    segments.Add(parent);
                //    return CreateFromPathAndSegments(this, segments.ToArray());
                //}
                //if (normalized.NormalizationLevel == NormalizationLevel.Fully)
                //{
                //    if (normalized.Segments.Length > 1)
                //    {
                //        return CreateFromPathAndSegments(this, segments.Take(segments.Count - 1).ToArray());
                //    }
                //    else
                //    {
                //        // TODO: We need more testing on this!!!
                //        return this;
                //    }
                //}
                //throw new InvalidOperationException("Unhandled case");
            }
        }

        private static List<Segment> CloneSegments(IPath path)
        {
            List<Segment> segments = new List<Segment>(
                (
                    from s in path.Segments
                    select (Segment)s.Clone()));
            return segments;
        }

        public long NormalizationDepth
        {
            get
            {
                int normalizationDepth = 0;
                int internalDepth = 0;
                foreach (var segment in Segments)
                {
                    switch (segment.SegmentType)
                    {
                        case SegmentType.DeviceSegment:
                            return 0;
                        case SegmentType.ParentSegment:
                            internalDepth--;
                            break;
//                        case SegmentType.VolumeRelativeSegment:
                        case SegmentType.GenericSegment:
                            internalDepth++;
                            if (normalizationDepth >= 0)
                            {
                                normalizationDepth++;
                            }
                            break;
                        default:
                            // DO NOTHING TO normalization level 
                            break;
                    }
                    if (internalDepth < normalizationDepth)
                    {
                        normalizationDepth = internalDepth;
                    }
                }

                return normalizationDepth;
            }
        }

        public ISegment RootSegment => this.LeadingSegment.IsRootSegment ? this.LeadingSegment : Segment.GetEmptySegment(this.PathType);

        public NormalizationLevel NormalizationLevel => simplificationLevel;

        private static NormalizationLevel GetSimplificationLevel(IPath path)
        {
            NormalizationLevel retval = NormalizationLevel.Fully;

            // This is a special case where the leading self reference can't be removed because 
            // it would result in an empty path ... so in this case it's as normalized as possible 
            if (path.Segments.Length == 1 && path.Segments[0].SegmentType == SegmentType.SelfSegment)
            {
                return NormalizationLevel.SelfReferenceOnly;
            }

            bool leadingParentsConsumed = false;
            foreach (var segment in path.Segments)
            {
                switch (segment.SegmentType)
                {
                    case SegmentType.ParentSegment:
                        if (leadingParentsConsumed)
                        {
                            return NormalizationLevel.NotNormalized;
                        }

                        retval = NormalizationLevel.LeadingParentsOnly;
                        continue;
                    case SegmentType.EmptySegment:
                    case SegmentType.NullSegment:
                    case SegmentType.SelfSegment:
                        return NormalizationLevel.NotNormalized;
                    default:
                        leadingParentsConsumed = true;
                        continue;
                }
            }

            return retval;
        }

        public string Name => TrailingSegment.Name;
        public string NameWithoutExtension => TrailingSegment.NameWithoutExtension;
        public string Extension => TrailingSegment.Extension;
        public bool HasExtension => TrailingSegment.HasExtension;

        public bool IsFullyQualified => LeadingSegment.SegmentType == SegmentType.RootSegment ||
                                        LeadingSegment.SegmentType == SegmentType.VolumeRelativeSegment ||
                                        LeadingSegment.SegmentType == SegmentType.RemoteSegment;

        public override string ToString()
        {
            if (PathType == PathType.Posix)
            {
                return PosixPath.ConvertToString(this);
            }

            return WindowsPath.ConvertToString(this);
        }

        internal static Segment[] SimplifySegments(PathType pathType, Segment[] segments)
        {
            Stack<Segment> stack = new Stack<Segment>(segments);
            Stack<Segment> result = new Stack<Segment>();

            //Func<Segment, bool> IsTrueRoot = (t) =>
            //{
            //    if (t.SegmentType == SegmentType.RemoteSegment ||
            //        t.SegmentType == SegmentType.RootSegment ||
            //        t.SegmentType == SegmentType.VolumelessRootSegment)
            //    {
            //        if (stack.Count > 0)
            //        {
            //            return true;
            //        }
            //    }
            //    return false;
            //};

            //Func<Segment, bool> IsTrueFullyQualified = (t) =>
            //{
            //    if (t.SegmentType == SegmentType.RemoteSegment ||
            //        t.SegmentType == SegmentType.RootSegment ||
            //        t.SegmentType == SegmentType.VolumeRelativeSegment)
            //    {
            //        if (stack.Count > 0)
            //        {
            //            return true;
            //        }
            //    }

            //    return false;
            //};

            //Func<Segment> SafePeek = () =>
            //{
            //    Segment prior = null;
            //    if (stack.Count > 0)
            //    {
            //        prior = stack.Peek();
            //    }

            //    return prior;
            //};

            int popDepth = 0;
            Func<bool> ShouldDiscard = () =>
            {
                if (popDepth > 0)
                {
                    popDepth--;
                    return true;
                }
                return false;
            };

            Segment forcedRoot = null;
            Segment current = null;
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
                        else if(!ShouldDiscard())
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
                    case SegmentType.GenericSegment:
                        if (!ShouldDiscard())
                        {
                            result.Push(current);
                        }
                        break;
                }
            }

            for (int i = 0; i < popDepth; i++)
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

        internal static Segment[] NormalizeSegments(PathType pathType, Segment[] suppliedSegments)
        {
            Stack<Segment> newSegments = new Stack<Segment>();
            bool isSelfSegmentsOnly = true;
            foreach (ISegment originalSegment in suppliedSegments)
            {
                Segment clone = (Segment) originalSegment.Clone();
                Segment topOfStack = null;
                Action setTopOfStack = () =>
                {
                    if (newSegments.Count > 0)
                    {
                        topOfStack = newSegments.Peek();
                    }
                };
                setTopOfStack();
                switch (originalSegment.SegmentType)
                {
                    case SegmentType.ParentSegment:
                        if (topOfStack != null && topOfStack.IsRootSegment)
                        {
                            // We can't traverse backwards before a root segment.
                            continue;
                        }
                        else if (topOfStack != null && topOfStack.SegmentType != SegmentType.ParentSegment)
                        {
                            newSegments.Pop();
                        }
                        else
                        {
                            newSegments.Push(clone);
                        }

                        isSelfSegmentsOnly = false;
                        break;
                    case SegmentType.EmptySegment:
                    case SegmentType.NullSegment:
                    case SegmentType.SelfSegment:
                        continue;
                    case SegmentType.DeviceSegment:
                        return new[] {clone};
                        // N + R
                    case SegmentType.GenericSegment:
                        // Q + R
                    case SegmentType.VolumeRelativeSegment:
                        // Q + A
                    case SegmentType.RootSegment:
                        // N + A
                    case SegmentType.VolumelessRootSegment:
                        // Q + A
                    case SegmentType.RemoteSegment:
                        isSelfSegmentsOnly = false;
                        newSegments.Push(clone);
                        break;
                }
            }

            if (newSegments.Count == 0 || isSelfSegmentsOnly)
            {
                if (pathType == PathType.Posix)
                {
                    return new[] {PosixSegment.SelfSegment};
                }
                else
                {
                    return new[] {WindowsSegment.SelfSegment};
                }
            }
            return newSegments.Reverse().ToArray();
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
            var segments = CloneSegments(this);
            var lastSegemnt = segments.Last();
            if (string.IsNullOrWhiteSpace(newExtension))
            {
                lastSegemnt.Name = lastSegemnt.NameWithoutExtension;
            }
            else
            {
                lastSegemnt.Name = string.Join(".", lastSegemnt.NameWithoutExtension, newExtension);
            }

            var tempPath = CreateFromPathAndSegments(this, segments.ToArray());
            return CreateFromSegments(this.PathType, tempPath.ToString(), segments.ToArray());
        }

        public IPath ConvertToRelativePath()
        {
            ParsedPath path = this;
            while (path.Anchor == PathAnchor.Absolute)
            {
                if (path.Segments.Length > 1)
                {
                    path = (ParsedPath) CreateFromPathAndSegments(path, path.Segments.Skip(1).Cast<Segment>().ToArray());
                    path = (ParsedPath) CreateFromSegments(path.PathType, path.ToString(), path.Segments.Cast<Segment>().ToArray());
                }
                else
                {
                    path = (ParsedPath) CreateFromSegments(path.PathType, ".", new[] {(Segment)Segment.GetSelfSegment(path.PathType)});
                }
            }
            return path;
        }

        public IPath Normalize()
        {
            return this.simplifiedForm;
            //IPath retval = ParsedPath.CreateFromPathAndSegments(this, NormalizeSegments());
            //return retval;
        }
    }
}
