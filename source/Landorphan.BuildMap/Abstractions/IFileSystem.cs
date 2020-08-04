namespace Landorphan.BuildMap.Abstractions
{
    public interface IFileSystem
    {
        string NormalizePath(string path);
        string GetWorkingDirectory();
        
        string[] GetFiles(string path);

        public string ReadFileContents(string path);
    }
}