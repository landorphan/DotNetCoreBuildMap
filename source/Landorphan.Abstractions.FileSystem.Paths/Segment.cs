using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Globalization;
    using System.Security.Cryptography.X509Certificates;
    using System.Text.RegularExpressions;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;

    public abstract class Segment : ISegment
    {
        public static readonly string PathSegmentNotationSegmentRegexPattern =
            @$"/?\{{(?<{SegmentTypeGroupName}>[{string.Join(string.Empty, PathSegmentNotationComponents.SegmentTypeStrings)}])\}} ?(?<{SegmentNameGroupName}>[^/]*)";
        public static readonly Regex PathSegmentNotationSegmentRegex = new Regex(PathSegmentNotationSegmentRegexPattern,
            RegexOptions.Compiled);
        public const string SegmentTypeGroupName = "Type";
        public const string SegmentNameGroupName = "Name";

        public SegmentType SegmentType { get; protected set; }
        public String Name { get; internal set; }

        public static ISegment GetSelfSegment(PathType pathType)
        {
            if (pathType == PathType.Posix)
            {
                return PosixSegment.SelfSegment;
            }
            else
            {
                return WindowsSegment.SelfSegment;
            }
        }

        public static ISegment GetParentSegment(PathType pathType)
        {
            if (pathType == PathType.Posix)
            {
                return PosixSegment.ParentSegment;
            }
            else
            {
                return WindowsSegment.ParentSegment;
            }
        }

        public static ISegment GetEmptySegment(PathType pathType)
        {
            if (pathType == PathType.Posix)
            {
                return PosixSegment.EmptySegment;
            }
            else
            {
                return WindowsSegment.EmptySegment;
            }
        }

        public bool IsRootSegment => SegmentType == SegmentType.RootSegment ||
                                     SegmentType == SegmentType.RemoteSegment ||
                                     SegmentType == SegmentType.VolumelessRootSegment;

        public abstract bool IsLegalForSegmentOffset(int offset);
        public abstract bool IsDiscouraged();

        public string NameWithoutExtension {
            get
            {
                int loc = Name.LastIndexOf(WindowsRelevantPathCharacters.Period);
                if (loc >= 1 && loc < Name.Length)
                {
                    return Name.Substring(0, loc);
                }

                return Name;
            }
        }

        public string Extension
        {
            get
            {
                string retval = string.Empty;
                int loc = Name.LastIndexOf(WindowsRelevantPathCharacters.Period);
                if (loc >= 1 && loc < Name.Length-1)
                {
                    retval = Name.Substring(loc);
                }

                return retval;
            }
        }

        public bool HasExtension => Extension?.Length > 0;

        public abstract ISegment Clone();

        public string ToPathSegmentNotation()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(PathSegmentNotationComponents.OpenBrace);
            builder.Append(PathSegmentNotationComponents.SegmentTypeToString[this.SegmentType]);
            builder.Append(PathSegmentNotationComponents.CloseBrace);
            builder.Append(this.PathSegmentNotationEncodedName);
            return builder.ToString();
        }

        public static string NameFromPathSegmentNotationEncodedName(string pathSegmentNotationName)
        {
            StringBuilder builder = new StringBuilder(pathSegmentNotationName);
            for (int i = 0; i <= PathSegmentNotationComponents.Space; i++)
            {
                var search = $"%{i:X2}";
                builder.Replace(search, ((char)i).ToString(CultureInfo.InvariantCulture));
            }

            builder.Replace($"%{(int)PathSegmentNotationComponents.ForwardSlash:X2}",
                PathSegmentNotationComponents.ForwardSlash.ToString(CultureInfo.InvariantCulture));

            // When Decoding an escaped '%' character must be decoded last, otherwise it will halt other decoding efforts.
            builder.Replace($"%{(int)PathSegmentNotationComponents.Percent:X2}",
                PathSegmentNotationComponents.Percent.ToString(CultureInfo.InvariantCulture));

            return builder.ToString();
        }

        public string PathSegmentNotationEncodedName { get
            {
                StringBuilder builder = new StringBuilder(Name);

                // When encoding, '%' characters must be encoded first or the result will contain double '%' characters
                builder.Replace(PathSegmentNotationComponents.Percent.ToString(CultureInfo.InvariantCulture),
                    $"%{(int)PathSegmentNotationComponents.Percent:X2}");

                for (int i = (char) 0; i <= PathSegmentNotationComponents.Space; i++)
                {
                    builder.Replace(((char)i).ToString(CultureInfo.InvariantCulture), $"%{i:X2}");
                }

                builder.Replace(PathSegmentNotationComponents.ForwardSlash.ToString(CultureInfo.InvariantCulture),
                    $"%{(int)PathSegmentNotationComponents.ForwardSlash:X2}");

                return builder.ToString();
            }
        }
    }
}
