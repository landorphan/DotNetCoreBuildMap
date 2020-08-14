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

        PathStatus Status { get; }

        PathType PathType { get; }

        PathAnchor Anchor { get; }

        ISegment[] Segments { get; }

        IPath SuppliedPath { get; }

        long NormalizationDepth { get; }

        NormalizationLevel NormalizationLevel { get; }

        IPath Normalize();
    }
}
