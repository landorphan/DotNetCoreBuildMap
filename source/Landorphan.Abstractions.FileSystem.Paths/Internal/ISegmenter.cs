using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal
{
    public interface ISegmenter
    {
        IEnumerable<Segment> GetSegments(string[] tokens);
    }
}
