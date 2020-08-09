using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    class PostixPath : ParsedPath
    {
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

                switch (segment.SegmentType)
                {
                    case SegmentType.NullSegment:
                        if (loc + 1 < Segments.Length || loc == 0)
                        {
                            Status = PathStatus.Illegal;
                            return;
                        }

                        break;
                    case SegmentType.EmptySegment:
                        if (loc == 0)
                        {
                            Status = PathStatus.Illegal;
                            return;
                        }

                        break;
                    case SegmentType.RootSegment:
                    case SegmentType.RemoteSegment:
                        if (loc != 0)
                        {
                            Status = PathStatus.Illegal;
                            return;
                        }

                        break;
                    case SegmentType.DeviceSegment:
                    case SegmentType.VolumeRelativeSegment:
                    case SegmentType.VolumelessRootSegment:
                        Status = PathStatus.Illegal;
                        return;
                }

                if (segment.Name != null)
                {
                    foreach (var segmentChar in segment.Name.ToCharArray())
                    {
                        if (segmentChar < PosixRelevantPathChars.Space)
                        {
                            isDiscouraged = true;
                        }
                    }
                    if (!isDiscouraged &&
                        (segment.Name.StartsWith(PosixRelevantPathChars.Space.ToString(), StringComparison.Ordinal) ||
                         segment.Name.EndsWith(PosixRelevantPathChars.Space.ToString(), StringComparison.Ordinal)))
                    {
                        isDiscouraged = true;
                    }
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

        public override PathType PathType => PathType.Posix;
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
                return Paths.PathAnchor.Relative;
            }
        }
    }
}
