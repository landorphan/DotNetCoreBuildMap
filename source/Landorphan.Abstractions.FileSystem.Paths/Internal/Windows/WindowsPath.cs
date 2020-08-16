using System;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
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
                if ((this.LeadingSegment.SegmentType == SegmentType.RemoteSegment ||
                     this.LeadingSegment.SegmentType == SegmentType.RootSegment ||
                     this.LeadingSegment.SegmentType == SegmentType.VolumelessRootSegment) &&
                    NormalizationDepth >= 0)
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
        }

        protected override ISegment SelfSegment => WindowsSegment.SelfSegment;
        protected override ISegment NullSegment => WindowsSegment.NullSegment;
        protected override ISegment EmptySegment => WindowsSegment.EmptySegment;
        protected override ISegment ParentSegment => WindowsSegment.ParentSegment;

        protected override void SetStatus()
        {
            int loc = 0;
            bool isDiscouraged = false;

            foreach (var segment in Segments)
            {
                if (!segment.IsLegal())
                {
                    Status = PathStatus.Illegal;
                    return;
                }

                if (loc > 0)
                {
                    if (segment.Name != null)
                    {
                        foreach (char illegalAfterFirstSegment in WindowsRelevantPathCharacters.IllegalAfterFirstSegment)
                        {
                            if (segment.Name.ToCharArray().Contains(illegalAfterFirstSegment))
                            {
                                Status = PathStatus.Illegal;
                                return;
                            }
                        }
                    }
                }

                switch (segment.SegmentType)
                {
                    case SegmentType.NullSegment:
                        if (loc + 1 < Segments.Length || loc == 0)
                        {
                            Status = PathStatus.Illegal;
                        }

                        return;

                    case SegmentType.EmptySegment:
                        if (loc == 0)
                        {
                            Status = PathStatus.Illegal;
                            return;
                        }

                        break;

                    case SegmentType.RootSegment:
                    case SegmentType.RemoteSegment:
                    case SegmentType.VolumeRelativeSegment:
                    case SegmentType.VolumelessRootSegment:
                        if (loc != 0)
                        {
                            Status = PathStatus.Illegal;
                            return;
                        }

                        break;
                    //case SegmentType.VolumeRelativeSegment:
                    //case SegmentType.VolumelessRootSegment:
                    //    if (loc > 0)
                    //    {
                    //        Status = PathStatus.Illegal;
                    //        return;
                    //    }
                    //    break;

                    // TODO: Ensure proper handling of Device Segment when determining anchor.
                    case SegmentType.DeviceSegment:
                        break;
                }

                if (segment.SegmentType != SegmentType.DeviceSegment)
                {
                    foreach (var deviceName in WindowsSegment.DeviceNames)
                    {
                        if (segment.Name.StartsWith(deviceName, StringComparison.OrdinalIgnoreCase))
                        {
                            isDiscouraged = true;
                        }
                    }
                }

                if (segment.Name.StartsWith(WindowsRelevantPathCharacters.Space.ToString(), StringComparison.Ordinal))
                {
                    isDiscouraged = true;
                }

                loc++;
            }

            if (isDiscouraged)
            {
                Status = PathStatus.Discouraged;
                return;
            }

            Status = PathStatus.Legal;
        }

        public const string UncPrefix = "UNC";

        public static string ConvertToString(ParsedPath path)
        {
            var seperator = '\\';
            if (path.Segments.Length == 1 && path.LeadingSegment.SegmentType == SegmentType.VolumelessRootSegment)
            {
                return seperator.ToString();
            }
            StringBuilder builder = new StringBuilder();
            //if (style == ToStringStyle.Long)
            //{
            //    builder.Append(seperator);
            //    builder.Append(seperator);
            //    builder.Append(WindowsRelevantPathCharacters.QuestionMark);
            //    builder.Append(seperator);
            //    builder.Append(UncPrefix);
            //    builder.Append(seperator);
            //}
            
            var segments = path.Segments.ToArray();
            bool skipNextSeperator = false;
            for (int i = 0; i < segments.Length; i++)
            {
                var currentSegment = segments[i];
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
                        return currentSegment.Name;
                    case SegmentType.RemoteSegment:
                        builder.Append(seperator);
                        builder.Append(seperator);
                        builder.Append(currentSegment.Name);
                        break;
                    case SegmentType.VolumeRelativeSegment:
                        builder.Append(currentSegment.Name);
                        skipNextSeperator = true;
                        break;
                    case SegmentType.RootSegment:
                        builder.Append(currentSegment.Name);
                        builder.Append(seperator);
                        skipNextSeperator = true;
                        break;
                    //case SegmentType.NullSegment:
                    //case SegmentType.EmptySegment:
                    //case SegmentType.GenericSegment:
                    //case SegmentType.SelfSegment:
                    //case SegmentType.ParentSegment:
                    default:
                        builder.Append(currentSegment.Name);
                        break;
                }
            }

            return builder.ToString();
        }
    }
}
