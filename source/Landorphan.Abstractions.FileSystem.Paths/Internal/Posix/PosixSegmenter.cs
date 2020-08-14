using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    using System.Linq;

    public class PosixSegmenter : ISegmenter
    {
        private const string DoubleForwardSlash = "//";

        public IEnumerable<Segment> GetSegments(string[] tokens)
        {
            IList<PosixSegment> segments = new List<PosixSegment>();
            if (tokens.Length == 0)
            {
                segments.Add(PosixSegment.NullSegment);
                return segments;
            }

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens.Length == 1)
                {
                    if (tokens[i] == null)
                    {
                        segments.Add(PosixSegment.NullSegment);
                        continue;
                    }

                    if (tokens[i] == string.Empty)
                    {
                        segments.Add(PosixSegment.EmptySegment);
                        continue;
                    }
                    //else
                    //{
                    //    segments.Add(new PosixSegment(SegmentType.GenericSegment, tokens[i]));
                    //    continue;
                    //}
                }
                if (i == 0)
                {
                    if (tokens[i].StartsWith("UNC:"))
                    {
                        segments.Add(new PosixSegment(SegmentType.RemoteSegment, tokens[i].Substring(4)));
                        continue;
                    }
                    //if (tokens[i].Length == 0)
                    //{
                    //    segments.Add(new PosixSegment(SegmentType.RemoteSegment, tokens[i].Substring(2)));
                    //    continue;
                    //}

                    //if (tokens[i].StartsWith(PosixRelevantPathChars.ForwardSlash.ToString(), StringComparison.Ordinal))
                    //{
                    //    segments.Add(new PosixSegment(SegmentType.RootSegment, tokens[i].Substring(1)));
                    //    continue;
                    //}

                    if (tokens[i] == string.Empty)
                    {
                        segments.Add(new PosixSegment(SegmentType.RootSegment, string.Empty));
                        continue;
                    }

                }
                segments.Add(PosixSegment.ParseFromString(tokens[i]));
            }

            // Special case for Root Segment to avoid a Root+Empty combination
            if (segments.Count == 2 && segments[0].SegmentType == SegmentType.RootSegment && segments[1].SegmentType == SegmentType.EmptySegment)
            {
                return segments.Take(1);
            }

            return segments;
        }

    }
}
