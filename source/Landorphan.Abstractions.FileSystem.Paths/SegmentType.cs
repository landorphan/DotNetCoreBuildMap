namespace Landorphan.Abstractions.FileSystem.Paths
{
    public enum SegmentType
    {
        // WHEN LEGAL: 
        //      Only as last segment
        // HOW  NORMALIZED:
        //      Always Removed
        //   LevelIncrease = 0
        NullSegment = 0,
        // WHEN LEGAL:
        //      If First segment: ILLEGAL
        //      otherwise: LEGAL
        // HOW  NORMALIZED:
        //      Always Removed
        //   LevelIncrease = 0
        EmptySegment = 1,
        // WHEN LEGAL:
        //      Only if First segment:
        // HOW  NORMALIZED:
        //      Always Kept
        //   LevelIncrease = 0
        RootSegment = 2,
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
        RemoteSegment = 3,
        // WHEN LEGAL: 
        //      Always LEGAL on Windows
        //      Always ILLEGAL on Posix
        // HOW  NORMALIZED:
        //      Results in Absolute *ALWAYS* regardless of location in path.
        //   LevelIncrease = +MIN(ABS(level), 0) (and returns)
        DeviceSegment = 4,
        // WHEN LEGAL: 
        //      If First segment: LEGAL on Windows
        //      Always ILLEGAL on Posix
        // HOW  NORMALIZED:
        //      Always Kept
        //   LevelIncrease = 0
        // EXAMPLE: On Windows 
        //      \foo
        VolumelessRootSegment = 6,
        // WHEN LEGAL: 
        //      If First segment: LEGAL on Windows
        //      Always ILLEGAL on Posix
        // HOW  NORMALIZED:
        //      Always Kept
        //   LevelIncrease = 1
        // EXAMPLE: On Windows
        //      C:foo.txt
        VolumeRelativeSegment = 7,
        // WHEN LEGAL: 
        //      Always LEGAL
        // HOW  NORMALIZED:
        //      Always Kept
        //   LevelIncrease = 1
        GenericSegment = 8,
        // WHEN LEGAL: 
        //      Always LEGAL
        // HOW  NORMALIZED:
        //      Always Removed
        //   LevelIncrease = 0
        SelfSegment = 9,
        // WHEN LEGAL: 
        //      Always LEGAL
        // HOW  NORMALIZED:
        //      Always Removed
        //   LevelIncrease = -1
        ParentSegment = 10,
    }
}
