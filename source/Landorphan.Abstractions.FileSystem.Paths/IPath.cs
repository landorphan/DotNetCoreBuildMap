namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Collections.Generic;

    public enum PathStatus
    {
        Undetermined,
        Legal,
        Discouraged,
        Illegal
    }

    public enum PathType
    {
        Windows,
        Posix,
    }

    public enum PathAnchor
    {
        Absolute,
        Relative
    }

    public enum SimplificationLevel
    {
        NotNormalized,
        LeadingParentsOnly,
        SelfReferenceOnly,
        Fully
    }

    public interface IPath
    {
        string SuppliedPathString { get; }

        ISegment LeadingSegment { get; }

        ISegment TrailingSegment { get; }

        PathStatus Status { get; }

        bool IsDiscouraged { get; }

        PathType PathType { get; }

        PathAnchor Anchor { get; }

        IReadOnlyList<ISegment> Segments { get; }

        IPath SuppliedPath { get; }

        IPath GetParent();

        ISegment RootSegment { get; }

        SimplificationLevel SimplificationLevel { get; }

        string Name { get; }
        string NameWithoutExtension { get; }
        string Extension { get; }
        bool HasExtension { get; }
        bool IsFullyQualified { get; }

        IPath ChangeExtension(string newExtension);
        IPath ConvertToRelativePath();
        IPath Simplify();

        IPath ReplaceSegment(int offset, ISegment segment);

        ISegment CreateSegment(SegmentType segmentType, string name);
        IPath AppendSegment(ISegment segment);
    }
}
