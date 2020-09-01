namespace Landorphan.BuildMap.Abstractions.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Emet.FileSystems;
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
            var foundFiles = Directory.GetFiles(NormalizePath(path), "*.*", SearchOption.AllDirectories);
            var baseFilePaths =
                (from p in foundFiles
               select new FilePaths {
                   Absolute = NormalizePath(p),
                   Relative = p.Length > path.Length ? p.Substring(path.Length + 1) : p,
                   Real = GetRealPath(NormalizePath(p))
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
            // return Path.GetDirectoryName(path);
            path = NormalizePath(path);
            if (path == "//" || path == "/")
            {
                return path;
            }

            int index = path.LastIndexOf('/');
            if (index > 0)
            {
                return path.Substring(0, index);
            }
            else
            {
                return "/";
            }

            return path;
        }

        public string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public string GetNameWithoutExtension(string path)
        {
            path.ArgumentNotNull(nameof(path));
            var name = GetName(path);
            int index = name.LastIndexOf('.');
            if (index > 1)
            {
                return name.Substring(0, index);
            }

            return name;
        }

        public string GetName(string path)
        {
            path.ArgumentNotNull(nameof(path));
            path = NormalizePath(path);
            int index = path.LastIndexOf('/');
            if (index > 1)
            {
                return path.Substring(index + 1);
            }

            return path;
        }
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string GetAbsolutePath(string path)
        {
            return NormalizePath(Path.GetFullPath(path));
        }

        public string GetRealPath(string path)
        {
            return path;
            Stack<string> fileParts = new Stack<string>(); 
            while (path.Length > 0)
            {
                var dirInfo = new DirectoryInfo(path);
                if (path == "//" || path == "/")
                {
                    break;
                }

                if (((int) dirInfo.Attributes != -1) &&
                    (dirInfo.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
                {
                    try
                    {
                        path = FileSystem.ReadLink(path);
                        if (!path.StartsWith('/'))
                        {
                            path = $"/{path}";
                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        break;
                    }
                    catch (DirectoryNotFoundException e)
                    {
                        break;
                    }
                }
                fileParts.Push(GetName(path));
                path = GetParentDirectory(path);
            }

            return string.Join('/', fileParts);
        }
    }
}