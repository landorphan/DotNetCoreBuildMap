namespace Landorphan.Abstractions.FileSystem.Paths.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;

    public abstract class ParsedPath : IPath
    {
        internal static IPath CreateFromSegments(PathType type, string suppliedPath, Segment[] segments)
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

            ParsedPath retval = null;
            if (Paths.PathType.Windows == type)
            {
                retval = new WindowsPath();
            }
            else
            {
                retval = new PostixPath();
            }

            retval.Segments = segments;
            retval.LeadingSegment = segments[0];
            retval.Status = PathStatus.Undetermined;
            retval.SetStatus();

            //if (PathAnchor.Absolute == retval.Anchor)
            //{
            //    if (retval.NormalizationDepth < 0)
            //    {
            //        retval.Status = PathStatus.Illegal;
            //    }
            //}
            return retval;
        }

        protected abstract void SetStatus();

        public String SuppliedPathString { get; private set; }
        public ISegment LeadingSegment { get; private set; }
        public PathStatus Status { get; internal set; }
        public abstract PathType PathType { get; }
        public ISegment[] Segments { get; private set; }
        public abstract PathAnchor Anchor { get; }
        public IPath SuppliedPath => this;

        public long NormalizationDepth
        {
            get
            {
                int normalizationDepth = 0;
                foreach (var segment in Segments)
                {
                    switch (segment.SegmentType)
                    {
                        case SegmentType.DeviceSegment:
                            return 0;
                        case SegmentType.ParentSegment:
                            normalizationDepth--;
                            break;
//                        case SegmentType.VolumeRelativeSegment:
                        case SegmentType.GenericSegment:
                            if (normalizationDepth >= 0)
                            {
                                normalizationDepth++;
                            }
                            break;
                        default:
                            // DO NOTHING TO normalization level 
                            break;
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

        //public bool NormalizationScope
        //{
        //    get
        //    {
        //        var nonNormalSegments = (
        //            from s in Segments
        //            where nonNormalSegmentTypes.Contains(s.SegmentType)
        //            select s
        //        ).Any();
        //        return NormalizationDepth >= 0 && !nonNormalSegments;
        //    }
        //}
    }
}
