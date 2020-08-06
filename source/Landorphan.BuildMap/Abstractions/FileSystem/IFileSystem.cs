namespace Landorphan.BuildMap.Abstractions.FileSystem
{
    public class FilePaths
    {
        public string Relative { get; set; }
        public string Absolute { get; set; }
        
        public string Real { get; set; }
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

        public string GetNameWithoutExtension(string path);

        public string GetName(string path);

        public bool FileExists(string path);

        public string GetAbsolutePath(string path);

        public string GetRealPath(string path);
    }
}