namespace Landorphan.Abstractions.FileSystem.Paths.Internal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;

    public abstract class ParsedPath : IPath
    {
        internal static IPath CreateFromPathAndSegments(PathType type, IPath suppliedPath, Segment[] segments)
        {
            TraverseSegmentChain(segments);
            ParsedPath retval = null;
            if (Paths.PathType.Windows == type)
            {
                retval = new WindowsPath();
            }
            else
            {
                retval = new PosixPath();
            }
            retval.Segments = segments;
            retval.LeadingSegment = segments[0];
            retval.Status = PathStatus.Undetermined;
            retval.SetStatus();
            retval.SuppliedPathString = suppliedPath.SuppliedPathString;
            retval.SuppliedPath = suppliedPath;
            return retval;
        }

        internal static IPath CreateFromSegments(PathType type, string suppliedPath, Segment[] segments)
        {
            TraverseSegmentChain(segments);

            ParsedPath retval = null;
            if (Paths.PathType.Windows == type)
            {
                retval = new WindowsPath();
            }
            else
            {
                retval = new PosixPath();
            }

            retval.Segments = segments;
            retval.LeadingSegment = segments[0];
            retval.Status = PathStatus.Undetermined;
            retval.SetStatus();
            retval.SuppliedPathString = suppliedPath;
            retval.SuppliedPath = retval;
            return retval;
        }

        private static void TraverseSegmentChain(Segment[] segments)
        {
            Segment lastSegment = null;
            foreach (var segment in segments)
            {
                if (lastSegment != null)
                {
                    lastSegment.NextSegment = segment;
                }

                lastSegment = segment;
            }
        }

        protected abstract void SetStatus();

        public String SuppliedPathString { get; private set; }
        public ISegment LeadingSegment { get; private set; }
        public PathStatus Status { get; internal set; }
        public abstract PathType PathType { get; }
        public ISegment[] Segments { get; private set; }
        public abstract PathAnchor Anchor { get; }
        public IPath SuppliedPath { get; private set; }

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


        private List<SegmentType> nonNormalSegmentTypes = new List<SegmentType>()
        {
            SegmentType.EmptySegment,
            SegmentType.NullSegment,
            SegmentType.ParentSegment,
            SegmentType.SelfSegment
        };

        public NormalizationLevel NormalizationLevel
        {
            get
            {
                NormalizationLevel retval = NormalizationLevel.Fully;

                // This is a special case where the leading self reference can't be removed because 
                // it would result in an empty path ... so in this case it's as normalized as possible 
                if (this.Segments.Length == 1 && this.Segments[0].SegmentType == SegmentType.SelfSegment)
                {
                    return NormalizationLevel.SelfReferenceOnly;
                }

                bool leadingParentsConsumed = false;
                foreach (var segment in this.Segments)
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
        }

        public override string ToString()
        {
            if (PathType == PathType.Posix)
            {
                return PosixPath.ConvertToString(this);
            }

            return WindowsPath.ConvertToString(this);
        }

        internal Segment[] NormalizeSegments()
        {
            Stack<Segment> newSegments = new Stack<Segment>();
            bool isSelfSegmentsOnly = true;
            foreach (ISegment originalSegment in this.Segments)
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
                        if (topOfStack != null && topOfStack.SegmentType != SegmentType.ParentSegment)
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
                    case SegmentType.GenericSegment:
                    case SegmentType.RootSegment:
                    case SegmentType.VolumeRelativeSegment:
                    case SegmentType.VolumelessRootSegment:
                    case SegmentType.RemoteSegment:
                        isSelfSegmentsOnly = false;
                        newSegments.Push(clone);
                        break;
                }
            }

            if (newSegments.Count == 0 && isSelfSegmentsOnly)
            {
                if (this.PathType == PathType.Posix)
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

        public IPath Normalize()
        {
            IPath retval = ParsedPath.CreateFromPathAndSegments(this.PathType, this, NormalizeSegments());
            return retval;
        }
    }
}
