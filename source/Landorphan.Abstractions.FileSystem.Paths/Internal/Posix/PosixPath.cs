namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    internal class PosixPath : ParsedPath
    {


        public override PathAnchor Anchor
        {
            get
            {
                if (this == simplifiedForm)
                {
                    if (LeadingSegment.SegmentType == SegmentType.RemoteSegment ||
                        LeadingSegment.SegmentType == SegmentType.RootSegment)
                    {
                        return PathAnchor.Absolute;
                    }

                    return PathAnchor.Relative;
                }

                return simplifiedForm.Anchor;
            }
        }

        public override PathType PathType => PathType.Posix;

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
            var separator = PosixRelevantPathChars.ForwardSlash.ToString(CultureInfo.InvariantCulture);
            var segmentArray = segments.ToArray();
            if (segmentArray.Length == 1 && segmentArray[0].SegmentType == SegmentType.RootSegment)
            {
                return separator;
            }

            var builder = new StringBuilder();

            for (var i = 0; i < segmentArray.Length; i++)
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
