namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class PathSegmentNotationComponents
    {
        public const char CloseBrace = '}';
        public const char CloseBracket = ']';
        public const char Colon = ':';
        public const char ForwardSlash = '/';
        public const char OpenBrace = '{';
        public const char OpenBracket = '[';

        public const string PathSegmentNotationHeader = "PSN";
        public const char Percent = '%';
        public const string PosixPathType = "POS";
        public const char Space = ' ';
        public const string WindowsPathType = "WIN";
        public static readonly IReadOnlyDictionary<SegmentType, string> SegmentTypeToString = new ReadOnlyDictionary<SegmentType, string>(
            new Dictionary<SegmentType, string>
            {
                {SegmentType.ParentSegment, "P"},
                {SegmentType.RootSegment, "R"},
                {SegmentType.EmptySegment, "E"},
                {SegmentType.SelfSegment, "S"},
                {SegmentType.VolumeRelativeSegment, "V"},
                {SegmentType.DeviceSegment, "D"},
                {SegmentType.GenericSegment, "G"},
                {SegmentType.RemoteSegment, "X"},
                {SegmentType.VolumelessRootSegment, "$"},
                {SegmentType.NullSegment, "0"}
            });

        public static readonly IReadOnlyDictionary<string, SegmentType> StringToSegmentType = new ReadOnlyDictionary<string, SegmentType>(
            (
                from p in SegmentTypeToString
                select new KeyValuePair<string, SegmentType>(p.Value, p.Key)).ToDictionary(
                x => x.Key,
                x => x.Value)
        );

        public static IEnumerable<string> SegmentTypeStrings => StringToSegmentType.Keys;
    }
}
