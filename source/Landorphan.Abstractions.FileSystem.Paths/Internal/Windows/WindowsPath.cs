namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    internal class WindowsPath : ParsedPath
    {
        public override PathAnchor Anchor
        {
            get
            {
                if (this == SimplifiedForm)
                {
                    if (LeadingSegment.SegmentType == SegmentType.RemoteSegment ||
                        LeadingSegment.SegmentType == SegmentType.RootSegment ||
                        LeadingSegment.SegmentType == SegmentType.VolumelessRootSegment)
                    {
                        return PathAnchor.Absolute;
                    }

                    foreach (var segment in Segments)
                    {
                        if (segment.SegmentType == SegmentType.DeviceSegment)
                        {
                            return PathAnchor.Absolute;
                        }
                    }

                    return PathAnchor.Relative;
                }

                return SimplifiedForm.Anchor;
            }
        }

        public override PathType PathType => PathType.Windows;

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
            const char separator = WindowsRelevantPathCharacters.BackSlash;
            var segmentArray = segments.ToArray();
            if (segmentArray.Length == 1 && segmentArray[0].SegmentType == SegmentType.VolumelessRootSegment)
            {
                return separator.ToString(CultureInfo.InvariantCulture);
            }

            var builder = new StringBuilder();

            var skipNextSeparator = false;
            for (var i = 0; i < segmentArray.Length; i++)
            {
                var currentSegment = segmentArray[i];
                if (i > 0 && !skipNextSeparator)
                {
                    builder.Append(separator);
                }

                skipNextSeparator = false;
                switch (currentSegment.SegmentType)
                {
                    // As device segments throw out all but the device ...
                    // only the device is relevant in the path so all else is thrown away.
                    case SegmentType.DeviceSegment:
                        return currentSegment.ToString();
                    case SegmentType.RemoteSegment:
                        builder.Append(separator);
                        builder.Append(separator);
                        builder.Append(currentSegment);
                        break;
                    case SegmentType.VolumeRelativeSegment:
                        builder.Append(currentSegment);
                        skipNextSeparator = true;
                        break;
                    case SegmentType.RootSegment:
                        builder.Append(currentSegment);
                        builder.Append(separator);
                        skipNextSeparator = true;
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
