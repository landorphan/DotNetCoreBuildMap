namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
    using System;

    // TODO: Make this internal once we have enough of the build system working to use InternalsVisibleTo
    public class WindowsPathTokenizer : PathTokenizer
    {
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

            if (path.StartsWith(@"\\?\UNC\"))
            {
                // Converts the (\\?\UNC\server\...) pattern into (UNC:server\...)
                path = "UNC:" + path.Substring(8);
            }
            else if (path.StartsWith(@"\\?\", StringComparison.Ordinal))
            {
                // Converts the (\\?\C:...) pattern into (C:...)
                path = path.Substring(4);
            }
            else if (path.StartsWith(@"\\"))
            {
                // Converts the (\\server\...) pattern into (UNC:server\...)
                path = "UNC:" + path.Substring(2);
            }

            return path.Replace('\\', '/');
        }
    }
}
