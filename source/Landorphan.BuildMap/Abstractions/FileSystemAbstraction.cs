using System;
using System.IO;
using System.Linq;

namespace Landorphan.BuildMap.Abstractions
{
    using Landorphan.Common;
    public class FileSystemAbstraction : IFileSystem
    {
        public string NormalizePath(string path)
        {
            path.ArgumentNotNull(nameof(path));
            return path.Replace("\\", "/", StringComparison.InvariantCulture).TrimEnd(
                Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        public string GetWorkingDirectory()
        {
            return NormalizePath(Directory.GetCurrentDirectory());
        }

        public FilePaths[] GetFiles(string path)
        {
            var baseFilePaths =
                (from p in Directory.GetFiles(NormalizePath(path), "*.*", SearchOption.AllDirectories) 
               select new FilePaths() {
                   Absolute = NormalizePath(p),
                   Relative = p.Length > path.Length ? p.Substring(path.Length + 1) : p,
               });
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

        public string CombinePaths(string rootPath, string relativePath)
        {
            return NormalizePath(Path.Combine(NormalizePath(rootPath), NormalizePath(relativePath)));
        }

        public string GetParentDirectory(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string GetAbsolutePath(string path)
        {
            return NormalizePath(Path.GetFullPath(path));
        }
    }
}