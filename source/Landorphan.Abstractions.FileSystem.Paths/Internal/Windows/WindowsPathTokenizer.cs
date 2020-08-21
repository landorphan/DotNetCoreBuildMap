namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
    using System;

    // TODO: Make this internal once we have enough of the build system working to use InternalsVisibleTo
    public class WindowsPathTokenizer : PathTokenizer
    {
        public const string LongFormUncIndicator = @"\\?\UNC\";
        public const string LongFormIndicator = @"\\?\";
        public const string DoubleBackSlash = @"\\";
        public const string DoubleForwardSlash = "//";

        public WindowsPathTokenizer(string path) : base(PreParsePath(path))
        {
        }

        // TODO: Make this internal once we have enough of the build system working to use InternalsVisibleTo
        public static string PreParsePath(string path)
        {
            if (path == null)
            {
                return null;
            }

            if (path.StartsWith(LongFormUncIndicator, StringComparison.Ordinal))
            {
                // Converts the (\\?\UNC\server\...) pattern into (UNC:server\...)
                path = "UNC:" + path.Substring(LongFormUncIndicator.Length);
            }
            else if (path.StartsWith(LongFormIndicator, StringComparison.Ordinal))
            {
                // Converts the (\\?\C:...) pattern into (C:...)
                path = path.Substring(LongFormIndicator.Length);
            }
            else if (path.StartsWith(DoubleBackSlash, StringComparison.Ordinal) || path.StartsWith(DoubleForwardSlash, StringComparison.Ordinal))
            {
                // Converts the (\\server\...) pattern into (UNC:server\...)
                path = "UNC:" + path.Substring(DoubleBackSlash.Length);
            }

            return path.Replace(WindowsRelevantPathCharacters.BackSlash, WindowsRelevantPathCharacters.ForwardSlash);
        }
    }
}
