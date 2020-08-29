using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    using System.Globalization;
    using System.Linq;

    class PosixPath : ParsedPath
    {
        public override PathType PathType => PathType.Posix;
        public override PathAnchor Anchor 
        { 
            get
            {
                if (this == this.simplifiedForm)
                {
                    if ((this.LeadingSegment.SegmentType == SegmentType.RemoteSegment ||
                         this.LeadingSegment.SegmentType == SegmentType.RootSegment))
                    {
                        return Paths.PathAnchor.Absolute;
                    }
                    return Paths.PathAnchor.Relative;
                }

                return simplifiedForm.Anchor;
            }
        }

        public override IPathComparerAndEquator DefaultComparerAndEquator => CaseSensitiveComparerAndEquator;

        public override ISegment CreateSegment(SegmentType segmentType, string name)
        {
            switch (segmentType)
            {
                case SegmentType.EmptySegment:
                    return PosixSegment.EmptySegment;
                case SegmentType.NullSegment:
                    return PosixSegment.NullSegment;
                case SegmentType.SelfSegment:
                    return PosixSegment.SelfSegment;
                case SegmentType.ParentSegment:
                    return PosixSegment.ParentSegment;
                default:
                    return new PosixSegment(segmentType, name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        protected override string ConvertToString(IEnumerable<ISegment> segments)
        {
            string separator = PosixRelevantPathChars.ForwardSlash.ToString(CultureInfo.InvariantCulture);
            var segmentArray = segments.ToArray();
            if (segmentArray.Length == 1 && segmentArray[0].SegmentType == SegmentType.RootSegment)
            {
                return separator;
            }
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < segmentArray.Length; i++)
            {
                var segment = segmentArray[i];
                if (i > 0)
                {
                    builder.Append(separator);
                }
                switch (segment.SegmentType)
                {
                    case SegmentType.RemoteSegment:
                        builder.Append(separator);
                        builder.Append(separator);
                        builder.Append(segment);
                        break;
                    // Comments here are just to show the cases covered by this 
                    // default clause.
                    //case SegmentType.NullSegment:
                    //case SegmentType.EmptySegment:
                    //case SegmentType.DeviceSegment:
                    //case SegmentType.VolumelessRootSegment:
                    //case SegmentType.VolumeRelativeSegment:
                    //case SegmentType.GenericSegment:
                    //case SegmentType.SelfSegment:
                    //case SegmentType.ParentSegment:
                    default:
                        builder.Append(segment);
                        break;
                }
            }

            return builder.ToString();
        }

    }
}
