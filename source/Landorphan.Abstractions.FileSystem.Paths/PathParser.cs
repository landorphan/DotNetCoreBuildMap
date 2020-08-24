namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using Landorphan.Abstractions.FileSystem.Paths.Abstraction;
    using Landorphan.Abstractions.FileSystem.Paths.Internal;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;

    public class PathParser : IPathParser
    {
        public static readonly string PathSegmentNotationSegmentRegexPattern =
            @$"/?\{{([{string.Join(string.Empty, PathSegmentNotationComponents.SegmentTypeStrings)}])\}} ?([^/]*)";
        public const string PathTypeGroupName = "PathType";
        public const string SegmentGroupName = "Segment";
        public static readonly string PathSegmentNotationPathRegexPattern =
            $@"\w*\[{PathSegmentNotationQuickCheckToken}(?<{PathTypeGroupName}>{PathSegmentNotationComponents.WindowsPathType}|{PathSegmentNotationComponents.PosixPathType})\]" +
            $@"(?<{SegmentGroupName}>{PathSegmentNotationSegmentRegexPattern})*";
        public static readonly Regex PathSegmentNotationPathRegex = new Regex(PathSegmentNotationPathRegexPattern,
            RegexOptions.Compiled);

        public IPath Parse(string pathString)
        {
            bool windows = PathAbstractionManager.GetRuntimeInformation().IsOSPlatform(OSPlatform.Windows);
            if (windows)
            {
                return Parse(pathString, PathType.Windows);
            }

            return Parse(pathString, PathType.Posix);
        }

        public const string PathSegmentNotationQuickCheckToken = "PSN:";

        public IPath Parse(String pathString, PathType pathType)
        {
            pathString ??= string.Empty;
            // NOTE: This is designed to be a very fast but possibly inaccurate test to see if this path is in PSN form.
            // This saves the time of performing the full Regex match if there is no way the path is in PSN form.
            // HOWEVER: the path will only be considered to be in PSN form if the actual Regex matches.
            // otherwise it will fall through and use the non PSN form of parsing.
            if (pathString.IndexOf(PathSegmentNotationQuickCheckToken, StringComparison.Ordinal) >= 0)
            {
                var match = PathSegmentNotationPathRegex.Match(pathString);
                if (match.Success)
                {
                    var pathTypeMatchGroup = match.Groups[PathTypeGroupName];
                    switch (pathTypeMatchGroup.Value)
                    {
                        case PathSegmentNotationComponents.PosixPathType:
                            pathType = PathType.Posix;
                            break;
                        case PathSegmentNotationComponents.WindowsPathType:
                            pathType = PathType.Windows;
                            break;
                        default:
                            throw new ArgumentException("Unrecognized Path Type");
                    }
                    var segmentMatchGroup = match.Groups[SegmentGroupName];
                    List<Segment> parsedSegments = new List<Segment>();
                    foreach (Capture capture in segmentMatchGroup.Captures)
                    {
                        Segment parsedSegment;
                        if (pathType == PathType.Posix)
                        {
                            parsedSegment = PosixSegment.ParseFromString(capture.Value);
                        }
                        else
                        {
                            parsedSegment = WindowsSegment.ParseFromString(capture.Value);
                        }
                        parsedSegments.Add(parsedSegment);
                    }

                    return ParsedPath.CreateFromSegments(pathType, pathString, parsedSegments);
                }
            }
            var tokenizer = GetTokenizer(pathString, pathType);
            var tokens = tokenizer.GetTokens();
            var segmenter = this.GetSegmenter(pathType);
            var rawSegments = segmenter.GetSegments(tokens).ToArray();
            return ParsedPath.CreateFromSegments(pathType, pathString, rawSegments);
        }

        // TODO: Make this internal once we have enough of the build system to use InternalsVisibleTo
        public ISegmenter GetSegmenter(PathType pathType)
        {
            if (PathType.Windows == pathType)
            {
                return new WindowsSegmenter();
            }
            else
            {
                return new PosixSegmenter();
            }
        }

        private PathTokenizer GetTokenizer(string pathString, PathType pathType)
        {
            if (pathType == PathType.Windows)
            {
                return new WindowsPathTokenizer(pathString);
            }
            else
            {
                return new PosixPathTokenizer(pathString);
            }
        }
    }
}
