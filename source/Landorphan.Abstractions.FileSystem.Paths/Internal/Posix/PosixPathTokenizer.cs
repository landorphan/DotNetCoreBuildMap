using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    public class PosixPathTokenizer : PathTokenizer
    {
        public const string DoubleForwardSlask = "//";
        public PosixPathTokenizer(string path) : base(PreParsePath(path))
        {
        }

        public static string PreParsePath(string path)
        {
            if (path == null)
            {
                return null;
            }

            if (path.StartsWith(DoubleForwardSlask, StringComparison.Ordinal))
            {
                // Converts the (\\server\...) pattern into (UNC:server\...)
                path = "UNC:" + path.Substring(DoubleForwardSlask.Length);
            }

            return path;
        }

    }
}
