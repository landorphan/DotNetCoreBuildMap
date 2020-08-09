namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Landorphan.Abstractions.FileSystem.Paths.Internal;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;

    public class PathParser : IPathParser
    {
        public IPath Parse(string pathString)
        {
            bool windows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (windows)
            {
                return Parse(pathString, PathType.Windows);
            }

            return Parse(pathString, PathType.Posix);
        }

        public IPath Parse(String pathString, PathType pathType)
        {
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
