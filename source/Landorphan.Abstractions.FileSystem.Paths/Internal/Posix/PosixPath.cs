using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    using System.Linq;

    class PosixPath : ParsedPath
    {
//        private void SetStatusInternal2()
//        {

//        }

//        protected override void SetStatus()
//        {
////            SetStatusInternal();
//        }
//        private void SetStatusInternal()
//        {
//            int loc = 0;
//            bool isDiscouraged = false;

//            foreach (var segment in Segments)
//            {
//                if (!segment.IsLegalForSegmentOffset(loc))
//                {
//                    status = PathStatus.Illegal;
//                    return;
//                }

//                switch (segment.SegmentType)
//                {
//                    case SegmentType.NullSegment:
//                        if (loc + 1 < Segments.Length || loc == 0)
//                        {
//                            status = PathStatus.Illegal;
//                            return;
//                        }

//                        break;
//                    case SegmentType.EmptySegment:
//                        if (loc == 0)
//                        {
//                            status = PathStatus.Illegal;
//                            return;
//                        }

//                        break;
//                    case SegmentType.RootSegment:
//                    case SegmentType.RemoteSegment:
//                        if (loc != 0)
//                        {
//                            status = PathStatus.Illegal;
//                            return;
//                        }

//                        break;
//                    case SegmentType.DeviceSegment:
//                    case SegmentType.VolumeRelativeSegment:
//                    case SegmentType.VolumelessRootSegment:
//                        status = PathStatus.Illegal;
//                        return;
//                }

//                if (segment.Name != null)
//                {
//                    foreach (var segmentChar in segment.Name.ToCharArray())
//                    {
//                        if (segmentChar < PosixRelevantPathChars.Space)
//                        {
//                            isDiscouraged = true;
//                        }
//                    }
//                    if (!isDiscouraged &&
//                        (segment.Name.StartsWith(PosixRelevantPathChars.Space.ToString(), StringComparison.Ordinal) ||
//                         segment.Name.EndsWith(PosixRelevantPathChars.Space.ToString(), StringComparison.Ordinal) ||
//                         ((segment.SegmentType != SegmentType.SelfSegment &&
//                           segment.SegmentType != SegmentType.ParentSegment) && 
//                          segment.Name.EndsWith(PosixRelevantPathChars.Period.ToString(), StringComparison.Ordinal))))
//                    {
//                        isDiscouraged = true;
//                    }
//                }


//                loc++;
//            }

//            if (isDiscouraged)
//            {
//                status = PathStatus.Discouraged;
//                return;
//            }

//            status = PathStatus.Legal;
//        }

        public override PathType PathType => PathType.Posix;
        public override PathAnchor Anchor 
        { 
            get
            {
                if (this == this.simplifiedForm)
                {
                    if ((this.LeadingSegment.SegmentType == SegmentType.RemoteSegment ||
                         this.LeadingSegment.SegmentType == SegmentType.RootSegment) 
//                    && NormalizationDepth >= 0
                    )
                    {
                        return Paths.PathAnchor.Absolute;
                    }
                    return Paths.PathAnchor.Relative;
                }

                return simplifiedForm.Anchor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static string ConvertToString(ParsedPath path)
        {
            var seperator = '/';
            if (path.Segments.Length == 1 && path.LeadingSegment.SegmentType == SegmentType.RootSegment)
            {
                return seperator.ToString();
            }
            StringBuilder builder = new StringBuilder();
            var segments = path.Segments.ToArray();

            for (int i = 0; i < segments.Length; i++)
            {
                var segment = segments[i];
                if (i > 0)
                {
                    builder.Append(seperator);
                }
                switch (segment.SegmentType)
                {
                    case SegmentType.RemoteSegment:
                        builder.Append(seperator);
                        builder.Append(seperator);
                        builder.Append(segment.Name);
                        break;
                    //case SegmentType.NullSegment:
                    //case SegmentType.EmptySegment:
                    //case SegmentType.DeviceSegment:
                    //case SegmentType.VolumelessRootSegment:
                    //case SegmentType.VolumeRelativeSegment:
                    //case SegmentType.GenericSegment:
                    //case SegmentType.SelfSegment:
                    //case SegmentType.ParentSegment:
                    default:
                        builder.Append(segment.Name);
                        break;
                }
            }

            return builder.ToString();
        }

    }
}
