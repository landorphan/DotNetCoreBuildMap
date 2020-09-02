namespace Landorphan.BuildMap.Abstractions.FileSystem
{
    public class FilePaths
    {
        public string Absolute { get; set; }

        public string Real { get; set; }
        public string Relative { get; set; }
    }

    public interface IFileSystem
    {
        public string CombinePaths(string rootPath, string relativePath);

        public bool FileExists(string path);

        public string GetAbsolutePath(string path);

        public string GetExtension(string path);

        FilePaths[] GetFiles(string path);

        public string GetName(string path);

        public string GetNameWithoutExtension(string path);

        public string GetParentDirectory(string path);

        public string GetRealPath(string path);
        string GetWorkingDirectory();
        string NormalizePath(string path);

        public string ReadFileContents(string path);
    }
}
