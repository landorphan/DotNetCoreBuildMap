using System;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    class WindowsPath : ParsedPath
    {
        public override PathType PathType => PathType.Windows;

        public override PathAnchor Anchor
        {
            get
            {
                if (this == simplifiedForm)
                {
                    if ((this.LeadingSegment.SegmentType == SegmentType.RemoteSegment ||
                         this.LeadingSegment.SegmentType == SegmentType.RootSegment ||
                         this.LeadingSegment.SegmentType == SegmentType.VolumelessRootSegment))
                    {
                        return PathAnchor.Absolute;
                    }
                    foreach (var segment in this.Segments)
                    {
                        if (segment.SegmentType == SegmentType.DeviceSegment)
                        {
                            return PathAnchor.Absolute;
                        }
                    }
                    return PathAnchor.Relative;
                }

                return simplifiedForm.Anchor;
            }
        }

        public override IPathComparerAndEquator DefaultComparerAndEquator => CaseInsensitiveComparerAndEquator;

        public override ISegment CreateSegment(SegmentType segmentType, string name)
        {
            switch (segmentType)
            {
                case SegmentType.EmptySegment:
                    return WindowsSegment.EmptySegment;
                case SegmentType.NullSegment:
                    return WindowsSegment.NullSegment;
                case SegmentType.SelfSegment:
                    return WindowsSegment.SelfSegment;
                case SegmentType.ParentSegment:
                    return WindowsSegment.ParentSegment;
                default:
                    return new WindowsSegment(segmentType, name);
            }
        }

        protected override string ConvertToString(IEnumerable<ISegment> segments)
        {
            const char seperator = WindowsRelevantPathCharacters.BackSlash;
            var segmentArray = segments.ToArray();
            if (segmentArray.Length == 1 && segmentArray[0].SegmentType == SegmentType.VolumelessRootSegment)
            {
                return seperator.ToString(CultureInfo.InvariantCulture);
            }
            StringBuilder builder = new StringBuilder();
            
            bool skipNextSeperator = false;
            for (int i = 0; i < segmentArray.Length; i++)
            {
                var currentSegment = segmentArray[i];
                if (i > 0 && !skipNextSeperator)
                {
                    builder.Append(seperator);
                }

                skipNextSeperator = false;
                switch (currentSegment.SegmentType)
                {
                    // As device segments throw out all but the device ...
                    // only the device is relevant in the path so all else is thrown away.
                    case SegmentType.DeviceSegment:
                        return currentSegment.ToString();
                    case SegmentType.RemoteSegment:
                        builder.Append(seperator);
                        builder.Append(seperator);
                        builder.Append(currentSegment);
                        break;
                    case SegmentType.VolumeRelativeSegment:
                        builder.Append(currentSegment);
                        skipNextSeperator = true;
                        break;
                    case SegmentType.RootSegment:
                        builder.Append(currentSegment);
                        builder.Append(seperator);
                        skipNextSeperator = true;
                        break;
                    // Comments here are just to show the cases covered by this 
                    // default clause.
                    //case SegmentType.NullSegment:
                    //case SegmentType.EmptySegment:
                    //case SegmentType.GenericSegment:
                    //case SegmentType.SelfSegment:
                    //case SegmentType.ParentSegment:
                    default:
                        builder.Append(currentSegment);
                        break;
                }
            }

            return builder.ToString();
        }
    }
}
