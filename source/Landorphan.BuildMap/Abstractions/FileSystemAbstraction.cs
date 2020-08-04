using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Landorphan.BuildMap.Abstractions
{
    public class FileSystemAbstraction : IFileSystem
    {
        public string NormalizePath(string path)
        {
            return path.Replace("\\", "/").TrimEnd(
                Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        public string GetWorkingDirectory()
        {
            return NormalizePath(Directory.GetCurrentDirectory());
        }

        public string[] GetFiles(string path)
        {
            var baseFilePaths =
                (from p in Directory.GetFiles(NormalizePath(path), "*.*", SearchOption.AllDirectories) 
               select NormalizePath(p));
            return baseFilePaths.ToArray();
        }

        public string ReadFileContents(string path)
        {
            string retval = null;
            if (File.Exists(path))
            {
                using (var stream = File.OpenRead(path))
                using (var reader = new StreamReader(stream))
                {
                    retval = reader.ReadToEnd();
                }
            }

            return retval;
        }
    }
}