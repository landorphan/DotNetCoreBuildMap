using System;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
    using System.Linq;

    class WindowsPath : ParsedPath
    {
        public override PathType PathType => PathType.Windows;

        public override PathAnchor Anchor
        {
            get
            {
                if (this.LeadingSegment.SegmentType == SegmentType.RemoteSegment ||
                    this.LeadingSegment.SegmentType == SegmentType.RootSegment ||
                    this.LeadingSegment.SegmentType == SegmentType.EmptySegment ||
                    this.LeadingSegment.SegmentType == SegmentType.NullSegment)
                {
                    return Paths.PathAnchor.Absolute;
                }
                foreach (var segment in this.Segments)
                {
                    if (segment.SegmentType == SegmentType.DeviceSegment)
                    {
                        return PathAnchor.Absolute;
                    }
                }
                return Paths.PathAnchor.Relative;
            }
        }

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
    }
}
