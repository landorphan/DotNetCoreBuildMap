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
    using Landorphan.BuildMap.Abstractions.FileSystem;
    using Landorphan.BuildMap.Abstractions.VisualStudioSolutionFile;
    using Landorphan.BuildMap.Model;
    using Landorphan.BuildMap.Serialization;
    using Landorphan.Common;
    using Microsoft.Build.Construction;
    using Microsoft.Build.Exceptions;

    public class MapFiles
    {
        public string WorkingDirectory { get; private set; }

        public MapFiles(string workingDirectory)
        {
            this.WorkingDirectory = workingDirectory;
        }

        private Dictionary<string, SuppliedFile> FilesBySafePath { get; } = new Dictionary<string, SuppliedFile>();

        private Dictionary<Guid, SuppliedFile> FilesByHashId { get; } = new Dictionary<Guid, SuppliedFile>();

        private Dictionary<Guid, SuppliedProjectFile> ProjectFiles { get; } = new Dictionary<Guid, SuppliedProjectFile>();
        private Dictionary<Guid, SuppliedSolutionFile> SolutionFiles { get; } = new Dictionary<Guid, SuppliedSolutionFile>();

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
                var fileNameHash = ComputeId(utf8.GetBytes(locatedFile.Absolute));
                if (!fs.FileExists(locatedFile.Absolute))
                {
                    item.Id = fileNameHash;
                    item.Directory = directory;
                    item.Paths = locatedFile;
                    item.Status = FileStatus.Missing;
                    item.RawText = string.Empty;
                }
                else
                {
                    List<byte> fullBuffer = new List<byte>();
                    var content = fs.ReadFileContents(locatedFile.Absolute);
                    byte[] contentBuffer = utf8.GetBytes(content);
                    fullBuffer.AddRange(fileNameHash.ToByteArray());
                    fullBuffer.AddRange(contentBuffer);
                    Guid id = ComputeId(fullBuffer.ToArray());
                    item.Id = id;
                    item.Paths = locatedFile;
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
                return fs.GetExtension(suppliedFile.Paths.Absolute).Equals(SlnExtension, StringComparison.OrdinalIgnoreCase);
            }
            return suppliedFile.RawText.Contains(SolutionFileHeader, StringComparison.InvariantCultureIgnoreCase);
        }
        
        public SuppliedFile DeterminFileType(SuppliedFile suppliedFile)
        {
            if (IsSolutionFile(suppliedFile))
            {
                SuppliedSolutionFile suppliedSolutionFile = new SuppliedSolutionFile(suppliedFile);
                TextReader reader = new StringReader(suppliedFile.RawText);
                // This parser doesn't seem to throw an exception for an invalid .sln file.
                // it simply interprets all the contents of the file as a header.
                try
                {
                    ISolutionFile sln = AbstractionManager.ParseSolutionFile(suppliedFile);
                    suppliedSolutionFile.SolutionContents = sln;
                    var projects = sln.GetAllProjects();
                    if (projects != null && projects.Any())
                    {
                        suppliedSolutionFile.Status = FileStatus.Valid;
                    }
                    else
                    {
                        suppliedSolutionFile.Status = FileStatus.Empty;
                    }
                }
                catch (InvalidProjectFileException ex)
                {
                    suppliedSolutionFile.Status = FileStatus.Malformed;
                }

                SafeAddFile(suppliedSolutionFile);
                return suppliedSolutionFile;
            }
            else
            {
                SuppliedProjectFile suppliedProjectFile = LoadProjectFileContents(suppliedFile);
                SafeAddFile(suppliedProjectFile);
                return suppliedProjectFile;
            }
        }
        
        public SuppliedProjectFile LoadProjectFileContents(SuppliedFile suppliedFile)
        {
            suppliedFile.ArgumentNotNull(nameof(suppliedFile));
            SuppliedProjectFile retval = new SuppliedProjectFile(suppliedFile);
            if (retval.Status != FileStatus.Missing)
            {
                if (!string.IsNullOrWhiteSpace(retval.RawText))
                {
                    XDocument document;
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(retval.RawText)))
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
                }
                else
                {
                    retval.Status = FileStatus.Empty;
                }
            }
            return retval;
        }

        public bool TryGetProjectFileByHashId(Guid id, out SuppliedProjectFile suppliedProjectFile)
        {
            return ProjectFiles.TryGetValue(id, out suppliedProjectFile);
        }

        public bool TryGetProjectFileBySafePath(string path, out SuppliedProjectFile suppliedProjectFile)
        {
            var fs = AbstractionManager.GetFileSystem();
            SuppliedFile suppliedFile;
            FilePaths paths = new FilePaths()
            {
                Relative = path,
                Absolute = fs.GetAbsolutePath(path),
                Real = fs.GetRealPath(path)
            };
            if (FilesBySafePath.TryGetValue(path, out suppliedFile))
            {
                return (suppliedProjectFile = suppliedFile as SuppliedProjectFile) != null;
            }
            else
            {
                return (suppliedProjectFile = GetSuppliedFile(paths) as SuppliedProjectFile) != null;
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

        public List<SuppliedSolutionFile> GetAllSolutionFiles()
        {
            return this.SolutionFiles.Values.ToList();
        }

        public List<SuppliedProjectFile> GetAllProjectFiles()
        {
            return this.ProjectFiles.Values.ToList();
        }

        public void SafeAddFile(SuppliedProjectFile suppliedProjectFile)
        {
            suppliedProjectFile.ArgumentNotNull(nameof(suppliedProjectFile));
            if (!FilesBySafePath.TryGetValue(suppliedProjectFile.Paths.Absolute, out _))
            {
                FilesBySafePath.Add(suppliedProjectFile.Paths.Absolute, suppliedProjectFile);
            }

            if (!FilesByHashId.TryGetValue(suppliedProjectFile.Id, out _))
            {
                FilesByHashId.Add(suppliedProjectFile.Id, suppliedProjectFile);
            }

            if (!ProjectFiles.TryGetValue(suppliedProjectFile.Id, out _))
            {
                ProjectFiles.Add(suppliedProjectFile.Id, suppliedProjectFile);
            }
        }

        public void SafeAddFile(SuppliedSolutionFile suppliedSolutionFile)
        {
            suppliedSolutionFile.ArgumentNotNull(nameof(suppliedSolutionFile));
            if (!FilesBySafePath.TryGetValue(suppliedSolutionFile.Paths.Absolute, out _))
            {
                FilesBySafePath.Add(suppliedSolutionFile.Paths.Absolute, suppliedSolutionFile);
            }

            if (!FilesByHashId.TryGetValue(suppliedSolutionFile.Id, out _))
            {
                FilesByHashId.Add(suppliedSolutionFile.Id, suppliedSolutionFile);
            }

            if (!SolutionFiles.TryGetValue(suppliedSolutionFile.Id, out _))
            {
                SolutionFiles.Add(suppliedSolutionFile.Id, suppliedSolutionFile);
            }
        }
    }
}