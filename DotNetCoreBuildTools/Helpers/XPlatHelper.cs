namespace ProjectOrder.Helpers
{
    using System.IO;

    public static class XPlatHelper
    {
        public static string ConvertPath(string path)
        {
            return path.Replace("\\", "/");
        }

        public static string FullyNormalizePath(string root, string path)
        {
            return Path.GetFullPath(Path.Combine(root, ConvertPath(path)))
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }
    }
}
