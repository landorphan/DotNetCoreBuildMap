namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    using System;

    public class PosixPathTokenizer : PathTokenizer
    {
        public const string DoubleForwardSlash = "//";

        public PosixPathTokenizer(string path) : base(PreParsePath(path))
        {
        }

        public static string PreParsePath(string path)
        {
            if (path == null)
            {
                return null;
            }

            if (path.StartsWith(DoubleForwardSlash, StringComparison.Ordinal))
            {
                // Converts the (\\server\...) pattern into (UNC:server\...)
                path = "UNC:" + path.Substring(DoubleForwardSlash.Length);
            }

            return path;
        }
    }
}
