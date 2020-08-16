namespace Landorphan.Abstractions.FileSystem.Paths
{
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

    public enum NormalizationLevel
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

        PathType PathType { get; }

        PathAnchor Anchor { get; }

        ISegment[] Segments { get; }

        IPath SuppliedPath { get; }

        IPath Parent { get; }

        long NormalizationDepth { get; }

        ISegment RootSegment { get; }

        NormalizationLevel NormalizationLevel { get; }

        string Name { get; }
        string NameWithoutExtension { get; }
        string Extension { get; }
        bool HasExtension { get; }
        bool IsFullyQualified { get; }

        IPath ChangeExtension(string newExtension);
        IPath ConvertToRelativePath();
        IPath Normalize();
    }
}
