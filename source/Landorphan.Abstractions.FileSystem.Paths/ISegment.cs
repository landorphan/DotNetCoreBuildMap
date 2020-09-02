namespace Landorphan.Abstractions.FileSystem.Paths
{
    using Landorphan.Common.Interfaces;

    public interface ISegment : ICloneable<ISegment>
    {
        string Extension { get; }

        bool HasExtension { get; }

        bool IsRootSegment { get; }

        string Name { get; }

        string NameWithoutExtension { get; }

        SegmentType SegmentType { get; }

        bool IsDiscouraged();

        bool IsLegalForSegmentOffset(int offset);

        string ToPathSegmentNotation();
    }
}
