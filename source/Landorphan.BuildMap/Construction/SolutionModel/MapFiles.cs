using System;
using System.Collections.Generic;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using Landorphan.BuildMap.Abstractions;
    using Landorphan.BuildMap.Model;
    using Landorphan.BuildMap.Serialization;
    using Landorphan.Common;
    using Onion.SolutionParser.Parser;
    using Onion.SolutionParser.Parser.Model;

    public class MapFiles
    {
        public string WorkingDirectory { get; private set; }

        public MapFiles(string workingDirectory)
        {
            this.WorkingDirectory = workingDirectory;
        }

        private Dictionary<string, SuppliedFile> FilesBySafePath { get; } = new Dictionary<string, SuppliedFile>();

        private Dictionary<Guid, SuppliedFile> FilesByHashId { get; } = new Dictionary<Guid, SuppliedFile>();

        private Dictionary<Guid, ProjectFile> ProjectFiles { get; } = new Dictionary<Guid, ProjectFile>();
        private Dictionary<Guid, SolutionFile> SolutionFiles { get; } = new Dictionary<Guid, SolutionFile>();

        [SuppressMessage("CodeSmell", "S2070",
            Justification = "This is not being used for crypto purposes, MD5 is the correct algorithm to use for this case (tistocks - 2020-08-03)")]
        [SuppressMessage("Unknown", "CA5351", 
            Justification = "This is not being used for crypto purposes, MD5 is the correct algorithm to use for this case (tistocks - 2020-08-03)")]
        public Guid ComputeId(byte[] rawData)
        {
            byte[] hash = null;
            using (MD5 hasher = MD5.Create())
            {
                hash = hasher.ComputeHash(rawData);
            }
            Guid retval = new Guid(hash.Take(16).ToArray());
            return retval;
        }

        public SuppliedFile GetSuppliedFile(FilePaths locatedFile)
        {
            SuppliedFile retval;
            var fs = AbstractionManager.GetFileSystem();
            var absolutPath = fs.GetAbsolutePath(locatedFile.Absolute);
            var directory = fs.GetParentDirectory(locatedFile.Absolute);
            
            if (!FilesBySafePath.TryGetValue(absolutPath, out retval))
            {
                Encoding utf8 = new UTF8Encoding(false);
                SuppliedFile item = new SuppliedFile();
                if (!fs.FileExists(locatedFile.Absolute))
                {
                    item.Id = ComputeId(utf8.GetBytes(locatedFile.Absolute));
                    item.Directory = directory;
                    item.Path = locatedFile.Relative;
                    item.AbsolutePath = absolutPath;
                    item.Status = FileStatus.Missing;
                    item.RawText = string.Empty;
                }
                else
                {
                    var content = fs.ReadFileContents(locatedFile.Absolute);
                    byte[] buffer = utf8.GetBytes(content);
                    Guid id = ComputeId(buffer);
                    item.Id = id;
                    item.Path = locatedFile.Relative;
                    item.AbsolutePath = absolutPath;
                    item.RawText = content;
                    item.Directory = directory;
                    item.Status = FileStatus.Unknown;
                }

                return DeterminFileType(item);
            }
            return retval;
        }

        public const string SolutionFileHeader = "Microsoft Visual Studio Solution File";
        public const string SlnExtension = ".sln";
        public bool IsSolutionFile(SuppliedFile suppliedFile)
        {
            var fs = AbstractionManager.GetFileSystem();
            suppliedFile.ArgumentNotNull(nameof(suppliedFile));
            if (suppliedFile.Status == FileStatus.Missing)
            {
                return fs.GetExtension(suppliedFile.Path).Equals(SlnExtension, StringComparison.OrdinalIgnoreCase);
            }
            return suppliedFile.RawText.Contains(SolutionFileHeader, StringComparison.InvariantCultureIgnoreCase);
        }
        
        public SuppliedFile DeterminFileType(SuppliedFile suppliedFile)
        {
            if (IsSolutionFile(suppliedFile))
            {
                SolutionFile solutionFile = new SolutionFile(suppliedFile);
                TextReader reader = new StringReader(suppliedFile.RawText);
                SolutionParser slnParser = new SolutionParser(reader);
                // This parser doesn't seem to throw an exception for an invalid .sln file.
                // it simply interprets all the contents of the file as a header.
                try
                {
                    ISolution sln = slnParser.Parse();
                    solutionFile.SolutionContents = sln;
                    if (sln.Projects != null && sln.Projects.Count > 0)
                    {
                        solutionFile.Status = FileStatus.Valid;
                    }
                    else
                    {
                        solutionFile.Status = FileStatus.Empty;
                    }
                }
                catch (Exception ex)
                {
                    solutionFile.Status = FileStatus.Malformed;
                }

                SafeAddFile(solutionFile);
                return solutionFile;
            }
            else
            {
                ProjectFile projectFile = LoadProjectFileContents(suppliedFile);
                SafeAddFile(projectFile);
                return projectFile;
            }
        }
        
        public ProjectFile LoadProjectFileContents(SuppliedFile suppliedFile)
        {
            suppliedFile.ArgumentNotNull(nameof(suppliedFile));
            ProjectFile retval = new ProjectFile(suppliedFile);
            XDocument document;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(suppliedFile.RawText)))
            using (var reader = new XmlTextReader(stream))
            {
                try
                {
                    document = XDocument.Load(reader);
                    retval.ProjectContents = document;
                    retval.Status = FileStatus.Valid;
                }
                catch (XmlException ex) 
                {
                    retval.Status = FileStatus.Malformed;
                }
            }
            return retval;
        }

        public bool TryGetProjectFileByHashId(Guid id, out ProjectFile projectFile)
        {
            return ProjectFiles.TryGetValue(id, out projectFile);
        }

        public bool TryGetProjectFileBySafePath(string path, out ProjectFile projectFile)
        {
            var fs = AbstractionManager.GetFileSystem();
            SuppliedFile suppliedFile;
            FilePaths paths = new FilePaths()
            {
                Relative = path,
                Absolute = fs.GetAbsolutePath(path)
            };
            if (FilesBySafePath.TryGetValue(path, out suppliedFile))
            {
                return (projectFile = suppliedFile as ProjectFile) != null;
            }
            else
            {
                return (projectFile = GetSuppliedFile(paths) as ProjectFile) != null;
            }
        }

        public void PreprocessList(IEnumerable<FilePaths> locatedFiles)
        {
            locatedFiles.ArgumentNotNull(nameof(locatedFiles));
            foreach (var locatedFile in locatedFiles)
            {
                GetSuppliedFile(locatedFile);
            }
        }

        public List<SolutionFile> GetAllSolutionFiles()
        {
            return this.SolutionFiles.Values.ToList();
        }

        public List<ProjectFile> GetAllProjectFiles()
        {
            return this.ProjectFiles.Values.ToList();
        }

        public void SafeAddFile(ProjectFile projectFile)
        {
            projectFile.ArgumentNotNull(nameof(projectFile));
            if (!FilesBySafePath.TryGetValue(projectFile.Path, out _))
            {
                FilesBySafePath.Add(projectFile.Path, projectFile);
            }

            if (!FilesByHashId.TryGetValue(projectFile.Id, out _))
            {
                FilesByHashId.Add(projectFile.Id, projectFile);
            }

            if (!ProjectFiles.TryGetValue(projectFile.Id, out _))
            {
                ProjectFiles.Add(projectFile.Id, projectFile);
            }
        }

        public void SafeAddFile(SolutionFile solutionFile)
        {
            solutionFile.ArgumentNotNull(nameof(solutionFile));
            if (!FilesBySafePath.TryGetValue(solutionFile.Path, out _))
            {
                FilesBySafePath.Add(solutionFile.Path, solutionFile);
            }

            if (!FilesByHashId.TryGetValue(solutionFile.Id, out _))
            {
                FilesByHashId.Add(solutionFile.Id, solutionFile);
            }

            if (!SolutionFiles.TryGetValue(solutionFile.Id, out _))
            {
                SolutionFiles.Add(solutionFile.Id, solutionFile);
            }
        }
    }
}