namespace Landorphan.BuildMap.Abstractions
{
    public class FilePaths
    {
        public string Relative { get; set; }
        public string Absolute { get; set; }
    }

    public interface IFileSystem
    {
        string NormalizePath(string path);
        string GetWorkingDirectory();
        
        FilePaths[] GetFiles(string path);

        public string ReadFileContents(string path);

        public string CombinePaths(string rootPath, string relativePath);

        public string GetParentDirectory(string path);

        public string GetExtension(string path);

        public string GetFileNameWithoutExtension(string path);

        public string GetFileName(string path);

        public bool FileExists(string path);

        public string GetAbsolutePath(string path);
    }
}