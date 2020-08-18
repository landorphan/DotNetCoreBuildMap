namespace Landorphan.Abstractions.FileSystem.Paths
{
    public enum SegmentType
    {
        // WHEN LEGAL: 
        //      Only as last segment
        // HOW  NORMALIZED:
        //      Always Removed
        //   LevelIncrease = 0
        NullSegment,
        // WHEN LEGAL:
        //      If First segment: ILLEGAL
        //      otherwise: LEGAL
        // HOW  NORMALIZED:
        //      Always Removed
        //   LevelIncrease = 0
        EmptySegment,
        // WHEN LEGAL:
        //      Only if First segment:
        // HOW  NORMALIZED:
        //      Always Kept
        //   LevelIncrease = 0
        RootSegment,
        // WHEN LEGAL:
        //      Only if First segment:
        // HOW  NORMALIZED:
        //      Always Kept
        //   LevelIncrease = 0
        // EXAMPLE:
        // On Windows
        //      C:\
        // On Posix
        //      /
        RemoteSegment,
        // WHEN LEGAL: 
        //      Always LEGAL on Windows
        //      Always ILLEGAL on Posix
        // HOW  NORMALIZED:
        //      Results in Absolute *ALWAYS* regardless of location in path.
        //   LevelIncrease = +MIN(ABS(level), 0) (and returns)
        DeviceSegment,
        // WHEN LEGAL: 
        //      If First segment: LEGAL on Windows
        //      Always ILLEGAL on Posix
        // HOW  NORMALIZED:
        //      Always Kept
        //   LevelIncrease = 0
        // EXAMPLE: On Windows 
        //      \foo
        VolumelessRootSegment,
        // WHEN LEGAL: 
        //      If First segment: LEGAL on Windows
        //      Always ILLEGAL on Posix
        // HOW  NORMALIZED:
        //      Always Kept
        //   LevelIncrease = 1
        // EXAMPLE: On Windows
        //      C:foo.txt
        VolumeRelativeSegment,
        // WHEN LEGAL: 
        //      Always LEGAL
        // HOW  NORMALIZED:
        //      Always Kept
        //   LevelIncrease = 1
        GenericSegment,
        // WHEN LEGAL: 
        //      Always LEGAL
        // HOW  NORMALIZED:
        //      Always Removed
        //   LevelIncrease = 0
        SelfSegment,
        // WHEN LEGAL: 
        //      Always LEGAL
        // HOW  NORMALIZED:
        //      Always Removed
        //   LevelIncrease = -1
        ParentSegment,
    }

    public interface ISegment
    {
        SegmentType SegmentType { get; }

        string Name { get; }

        //ISegment NextSegment { get; }

        //ISegment LastSegment { get; }

        bool IsRootSegment { get; }

        bool IsLegal();

        bool IsDiscouraged();
        
        string NameWithoutExtension { get; }

        string Extension { get; }

        bool HasExtension { get; }

        ISegment Clone();
    }
}
