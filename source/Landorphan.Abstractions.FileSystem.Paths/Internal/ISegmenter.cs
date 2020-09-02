namespace Landorphan.Abstractions.FileSystem.Paths.Internal
{
    using System.Collections.Generic;

    public interface ISegmenter
    {
        IEnumerable<Segment> GetSegments(string[] tokens);
    }
}
