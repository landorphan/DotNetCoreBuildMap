namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PosixSegmenter : ISegmenter
    {
        private const string UncIndicator = "UNC:";

        public IEnumerable<Segment> GetSegments(string[] tokens)
        {
            IList<PosixSegment> segments = new List<PosixSegment>();
            if (tokens == null || tokens.Length == 0)
            {
                segments.Add(PosixSegment.NullSegment);
                return segments;
            }

            for (var i = 0; i < tokens.Length; i++)
            {
                if (tokens.Length == 1)
                {
                    if (tokens[i] == null)
                    {
                        segments.Add(PosixSegment.NullSegment);
                        continue;
                    }

                    if (string.IsNullOrEmpty(tokens[i]))
                    {
                        segments.Add(PosixSegment.EmptySegment);
                        continue;
                    }
                }

                if (i == 0)
                {
                    if (tokens[i].StartsWith(UncIndicator, StringComparison.Ordinal))
                    {
                        segments.Add(new PosixSegment(SegmentType.RemoteSegment, tokens[i].Substring(UncIndicator.Length)));
                        continue;
                    }

                    if (string.IsNullOrEmpty(tokens[i]))
                    {
                        segments.Add(new PosixSegment(SegmentType.RootSegment, string.Empty));
                        continue;
                    }
                }

                segments.Add(PosixSegment.ParseFromString(tokens[i]));
            }

            // Special case for Root Segment to avoid a Root+Empty combination
#pragma warning disable S109 // Magic numbers should not be used
            if (segments.Count == 2 && segments[0].SegmentType == SegmentType.RootSegment && segments[1].SegmentType == SegmentType.EmptySegment)
#pragma warning restore S109 // Magic numbers should not be used
            {
                return segments.Take(1);
            }

            return segments;
        }
    }
}
