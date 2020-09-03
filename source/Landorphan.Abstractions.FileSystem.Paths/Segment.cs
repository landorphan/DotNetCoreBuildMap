namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using Landorphan.Abstractions.FileSystem.Paths.Internal;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;

    public abstract class Segment : ISegment
    {
        public const string SegmentNameGroupName = "Name";

        public const string SegmentTypeGroupName = "Type";

        public static readonly string PathSegmentNotationSegmentRegexPattern =
            @$"/?\{{(?<{SegmentTypeGroupName}>[{string.Join(string.Empty, PathSegmentNotationComponents.SegmentTypeStrings)}])\}} ?(?<{SegmentNameGroupName}>[^/]*)";
        public static readonly Regex PathSegmentNotationSegmentRegex = new Regex(
            PathSegmentNotationSegmentRegexPattern,
            RegexOptions.Compiled);

        public object Clone()
        {
            return DeepClone();
        }

        public abstract ISegment DeepClone();

        public string Extension
        {
            get
            {
                var retval = string.Empty;
                var loc = Name.LastIndexOf(WindowsRelevantPathCharacters.Period);
                if (loc >= 1 && loc < Name.Length - 1)
                {
                    retval = Name.Substring(loc);
                }

                return retval;
            }
        }

        public bool HasExtension => Extension?.Length > 0;

        public bool IsRootSegment => SegmentType == SegmentType.RootSegment ||
                                     SegmentType == SegmentType.RemoteSegment ||
                                     SegmentType == SegmentType.VolumelessRootSegment;

        public string Name { get; internal set; }

        public string NameWithoutExtension
        {
            get
            {
                var loc = Name.LastIndexOf(WindowsRelevantPathCharacters.Period);
                if (loc >= 1 && loc < Name.Length)
                {
                    return Name.Substring(0, loc);
                }

                return Name;
            }
        }

        public string PathSegmentNotationEncodedName
        {
            get
            {
                var builder = new StringBuilder(Name);

                // When encoding, '%' characters must be encoded first or the result will contain double '%' characters
                builder.Replace(
                    PathSegmentNotationComponents.Percent.ToString(CultureInfo.InvariantCulture),
                    $"%{(int)PathSegmentNotationComponents.Percent:X2}");

                for (int i = (char)0; i <= PathSegmentNotationComponents.Space; i++)
                {
                    builder.Replace(((char)i).ToString(CultureInfo.InvariantCulture), $"%{i:X2}");
                }

                builder.Replace(
                    PathSegmentNotationComponents.ForwardSlash.ToString(CultureInfo.InvariantCulture),
                    $"%{(int)PathSegmentNotationComponents.ForwardSlash:X2}");

                return builder.ToString();
            }
        }

        public SegmentType SegmentType { get; protected set; }

        public abstract bool IsDiscouraged();

        public abstract bool IsLegalForSegmentOffset(int offset);

        public string ToPathSegmentNotation()
        {
            var builder = new StringBuilder();
            builder.Append(PathSegmentNotationComponents.OpenBrace);
            builder.Append(PathSegmentNotationComponents.SegmentTypeToString[SegmentType]);
            builder.Append(PathSegmentNotationComponents.CloseBrace);
            builder.Append(PathSegmentNotationEncodedName);
            return builder.ToString();
        }

        public static ISegment GetEmptySegment(PathType pathType)
        {
            if (pathType == PathType.Posix)
            {
                return PosixSegment.EmptySegment;
            }

            return WindowsSegment.EmptySegment;
        }

        public static ISegment GetNullSegment(PathType pathType)
        {
            if (pathType == PathType.Posix)
            {
                return PosixSegment.NullSegment;
            }

            return WindowsSegment.NullSegment;
        }

        public static ISegment GetParentSegment(PathType pathType)
        {
            if (pathType == PathType.Posix)
            {
                return PosixSegment.ParentSegment;
            }

            return WindowsSegment.ParentSegment;
        }

        public static ISegment GetSelfSegment(PathType pathType)
        {
            if (pathType == PathType.Posix)
            {
                return PosixSegment.SelfSegment;
            }

            return WindowsSegment.SelfSegment;
        }

        public static string NameFromPathSegmentNotationEncodedName(string pathSegmentNotationName)
        {
            var builder = new StringBuilder(pathSegmentNotationName);
            for (var i = 0; i <= PathSegmentNotationComponents.Space; i++)
            {
                var search = $"%{i:X2}";
                builder.Replace(search, ((char)i).ToString(CultureInfo.InvariantCulture));
            }

            builder.Replace(
                $"%{(int)PathSegmentNotationComponents.ForwardSlash:X2}",
                PathSegmentNotationComponents.ForwardSlash.ToString(CultureInfo.InvariantCulture));

            // When Decoding an escaped '%' character must be decoded last, otherwise it will halt other decoding efforts.
            builder.Replace(
                $"%{(int)PathSegmentNotationComponents.Percent:X2}",
                PathSegmentNotationComponents.Percent.ToString(CultureInfo.InvariantCulture));

            return builder.ToString();
        }
    }
}
