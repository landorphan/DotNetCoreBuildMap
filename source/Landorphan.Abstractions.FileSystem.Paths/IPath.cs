namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Collections.Generic;
    using Landorphan.Common.Interfaces;

    public interface IPath : ICloneable<IPath>
    {
        PathAnchor Anchor { get; }

        string Extension { get; }

        bool HasExtension { get; }

        bool IsDiscouraged { get; }

        bool IsFullyQualified { get; }

        ISegment LeadingSegment { get; }

        string Name { get; }

        string NameWithoutExtension { get; }

        PathType PathType { get; }

        ISegment RootSegment { get; }

        IReadOnlyList<ISegment> Segments { get; }

        SerializationForm SerializationMethod { get; set; }

        SimplificationLevel SimplificationLevel { get; }

        PathStatus Status { get; }

        IPath SuppliedPath { get; }

        string SuppliedPathString { get; }

        ISegment TrailingSegment { get; }

        IPath AppendSegmentAfter(int offset, ISegment segment);

        IPath AppendSegmentAtEnd(ISegment segment);

        IPath ChangeExtension(string newExtension);

        IPath ConvertToRelativePath();

        ISegment CreateSegment(SegmentType segmentType, string name);

        IPath GetParent();

        IPath InsertSegmentAtBeginning(ISegment segment);

        IPath InsertSegmentBefore(int offset, ISegment segment);

        IPath Join(IPath other);

        IPath ReplaceSegment(int offset, ISegment segment);

        IPath Simplify();

        string ToPathSegmentNotation();
    }
}
