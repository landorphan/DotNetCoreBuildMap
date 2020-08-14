using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
    using System.Linq;

    public class WindowsSegmenter : ISegmenter
    {
        public IEnumerable<Segment> GetSegments(string[] tokens)
        {
            IList<WindowsSegment> segments = new List<WindowsSegment>();

            if (tokens.Length == 0)
            {
                segments.Add(WindowsSegment.NullSegment);
                return segments.ToArray();
            }

            for (int i = 0; i < tokens.Length; i++)
            {
                if (i == 0)
                {
                    if (tokens[i].StartsWith("UNC:"))
                    {
                        segments.Add(new WindowsSegment(SegmentType.RemoteSegment, tokens[i].Substring(4)));
                        continue;
                    }
                    if (tokens[i].Contains(":"))
                    {
                        var parts = tokens[i].Split(':');
                        if (parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1]))
                        {
                            segments.Add(new WindowsSegment(SegmentType.VolumeRelativeSegment, parts[0] + ":"));
                            segments.Add(WindowsSegment.ParseFromString(parts[1]));
                        }
                        else
                        {
                            if (WindowsSegment.IsDeviceSegment(parts[0]))
                            {
                                segments.Add(new WindowsSegment(SegmentType.DeviceSegment, parts[0]));
                            }
                            else
                            {
                                segments.Add(new WindowsSegment(SegmentType.RootSegment, parts[0] + ":"));
                            }
                        }
                    }
                    else if (tokens.Length == 1)
                    {
                        if (tokens[i] == null)
                        {
                            segments.Add(WindowsSegment.NullSegment);
                        }

                        if (tokens[i] == string.Empty)
                        {
                            segments.Add(WindowsSegment.EmptySegment);
                        }
                        else
                        {
                            segments.Add(WindowsSegment.ParseFromString(tokens[i]));
                        }
                    }
                    else if (tokens[i] == string.Empty)
                    {
                        segments.Add(new WindowsSegment(SegmentType.VolumelessRootSegment, string.Empty));
                    }
                    else
                    {
                        segments.Add(WindowsSegment.ParseFromString(tokens[i]));
                    }
                }
                else if (i >= 1)
                {
                    segments.Add(WindowsSegment.ParseFromString(tokens[i]));
                }
            }

            // Special case for Root Segment to avoid a Root+Empty combination
            if (segments.Count == 2 && segments[0].SegmentType == SegmentType.VolumelessRootSegment && segments[1].SegmentType == SegmentType.EmptySegment)
            {
                return segments.Take(1);
            }

            return segments;
        }
    }
}
