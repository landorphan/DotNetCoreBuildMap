using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using DotNet.Globbing;
using Landorphan.BuildMap.Abstractions;
using Landorphan.BuildMap.Construction.SolutionModel;
using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Construction
{
    public class MapManagement
    {
        public IEnumerable<string> LocateFiles(string workingDirectory, IEnumerable<string> globPatterns)
        {
            var fs = AbstractionManager.GetFileSystem();
            if (string.IsNullOrWhiteSpace(workingDirectory))
            {
                workingDirectory = fs.GetWorkingDirectory();
            }
            List<string> retval = new List<string>();
            var globs =
                (from g in globPatterns
               select Glob.Parse(g));
            var files = fs.GetFiles(workingDirectory);
            foreach (var file in files)
            {
                foreach (var glob in globs)
                {
                    if (glob.IsMatch(file))
                    {
                        retval.Add(file);
                        continue;
                    }
                }
            }
            return retval;
        }

        public Guid ComputeId(byte[] rawData)
        {
            MD5 hasher = MD5.Create();
            byte[] hash = hasher.ComputeHash(rawData);
            Guid retval = new Guid(hash.Take(16).ToArray());
            return retval;
        }

        public List<SuppliedFile> GetSuppliedFiles(IEnumerable<string> locatedFiles)
        {
            var fs = AbstractionManager.GetFileSystem();
            List<SuppliedFile> retval = new List<SuppliedFile>();
            Encoding utf8 = new UTF8Encoding(false);
            foreach (var file in locatedFiles)
            {
                var content = fs.ReadFileContents(file);
                byte[] buffer = utf8.GetBytes(content);
                Guid id = ComputeId(buffer);
                SuppliedFile item = new SuppliedFile();
                item.Id = id;
                item.Paths.Add(file);
                item.RawText = content;
                retval.Add(item);
            }

            return retval;
        }

        public ProjectFile LoadProjectFileContents(SuppliedFile suppliedFile)
        {
            ProjectFile retval = new ProjectFile(suppliedFile);
            XmlDocument document = new XmlDocument();
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(suppliedFile.RawText)))
            using (var reader = new XmlTextReader(stream))
            {
                document.Load(reader);
            }

            retval.ProjectContents = document;
            return retval;
        }

        public const string SolutionFileHeader = "Microsoft Visual Studio Solution File";
        public bool IsSolutionFile(SuppliedFile suppliedFile)
        {
            return suppliedFile.RawText.Contains(SolutionFileHeader);
        }

        public MapFiles PreprocessList(IEnumerable<string> locatedFiles)
        {
            MapFiles mapFiles = new MapFiles();
            mapFiles.LocatedFiles = locatedFiles.ToList();
            mapFiles.SuppliedFiles = GetSuppliedFiles(locatedFiles);
            foreach (var suppliedFile in mapFiles.SuppliedFiles)
            {
                if (IsSolutionFile(suppliedFile))
                {
                    
                }
                else
                {
                    ProjectFile projectFile = LoadProjectFileContents(suppliedFile);
                    mapFiles.SafeAddFile(projectFile);
                }
            }
            
            return mapFiles;
        }
        
        public Map Create(string workingDirectory, IEnumerable<string> globPatterns)
        {
            IEnumerable<string> locatedFiles = LocateFiles(workingDirectory, globPatterns);
            MapFiles mapFiles = PreprocessList(locatedFiles);
            return null;
        }
    }
}