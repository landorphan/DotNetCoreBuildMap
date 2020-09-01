using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    using System.Reflection;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Landorphan.BuildMap.Abstractions.FileSystem;
    using Landorphan.BuildMap.Abstractions.VisualStudioSolutionFile;
    using Landorphan.Common;
    using Microsoft.Build.Construction;
    using Newtonsoft.Json.Linq;
    using YamlDotNet.Core.Tokens;

    public class MapManagement
    {

        // public List<SuppliedFile> GetSuppliedFiles(IEnumerable<string> locatedFiles)
        // {
        //     List<SuppliedFile> retval = new List<SuppliedFile>();
        //     foreach (var file in locatedFiles)
        //     {
        //         var suppliedFile = GetSuppliedFile(file);
        //         retval.Add(suppliedFile);
        //     }
        //
        //     return retval;
        // }

//        private readonly Guid SolutionFolderGuid = new Guid("2150E333-8FDC-42A3-9474-1A3956D46DE8");
//        private readonly Guid SharedProjectGuid = new Guid("D954291E-2A0B-460D-934E-DC6B0785DB48");
        
        public void IncorporateSolutionFileProjects(MapFiles mapFiles)
        {
            var fs = AbstractionManager.GetFileSystem();
            var slnFiles = mapFiles.GetAllSolutionFiles().Where(sf => sf.Status == FileStatus.Valid);
            foreach (var solutionFile in slnFiles)
            {
                Dictionary<Guid, IProjectInSolution> solutionProjects = new Dictionary<Guid, IProjectInSolution>(
                (   from sp in solutionFile.SolutionContents.GetAllProjects()
                   where sp.ProjectType != SolutionProjectType.SolutionFolder 
                  select new KeyValuePair<Guid, IProjectInSolution>(sp.SlnGuid, sp)));
                // First pass through ... create the ProjectFile entries and map
                // sln guids to HashGuids.
                foreach (var projectReference in solutionProjects)
                {
                    var slnGuid = projectReference.Key;
                    var projectReferencePath = fs.NormalizePath(projectReference.Value.AbsolutePath);
                    if (mapFiles.TryGetProjectFileBySafePath(projectReference.Value.RelativePath, projectReferencePath, out var projectFile))
                    {
                        solutionFile.SlnGuidToHashGuidLookup.Add(slnGuid, projectFile.Id);
                        projectFile.SolutionFiles.Add(solutionFile);
                    }
                }
                // Second pass through ... map dependency projects.
                foreach (var projectReference in solutionProjects)
                {
                    var currentProjectHashGuid = solutionFile.SlnGuidToHashGuidLookup[projectReference.Value.SlnGuid];
                    if (mapFiles.TryGetProjectFileByHashId(currentProjectHashGuid, out var currentProjectFile))
                    {
                        foreach (var dependentOnSlnGuid in projectReference.Value.GetProjectsThisProjectDependsOn())
                        {
                            var dependentOnHashGuid = solutionFile.SlnGuidToHashGuidLookup[dependentOnSlnGuid];
                            if (mapFiles.TryGetProjectFileByHashId(dependentOnHashGuid, out var dependentOnProject))
                            {
                                if (!currentProjectFile.SolutionDependentOn.TryGetValue(dependentOnHashGuid, out var solutionProjectDependentOn))
                                {
                                    solutionProjectDependentOn = new Dictionary<Guid, SuppliedProjectFile>();
                                    currentProjectFile.SolutionDependentOn.Add(solutionFile.Id, solutionProjectDependentOn);
                                }
                                solutionProjectDependentOn.Add(dependentOnHashGuid, dependentOnProject);
                            }
                        }
                    }
                }
            }
        }

        public const string ProjectReferenceXPath = "/Project/ItemGroup/ProjectReference/@Include";

        public void MapProjectLevelDependenciesForProjectFile(MapFiles mapFiles, SuppliedProjectFile suppliedProjectFile)
        {
            var fs = AbstractionManager.GetFileSystem();
            var projectReferences = (suppliedProjectFile.ProjectContents.XPathEvaluate(ProjectReferenceXPath) 
                as IEnumerable<object>)?.Cast<XAttribute>();
            foreach (var include in projectReferences)
            {
                var baseRelativePath = fs.CombinePaths(suppliedProjectFile.Directory, include.Value);
                var includePath = fs.GetAbsolutePath(baseRelativePath);
                SuppliedProjectFile includedSuppliedProjectFile;
                // Guid includedProjectHashGuid;
                // First attempt to lookup the project based on the "safe path".
                if (mapFiles.TryGetProjectFileBySafePath(baseRelativePath, includePath, out includedSuppliedProjectFile) && 
                    !suppliedProjectFile.ProjectDependentOn.TryGetValue(includedSuppliedProjectFile.Id, out _))
                {
                    suppliedProjectFile.ProjectDependentOn.Add(includedSuppliedProjectFile.Id, includedSuppliedProjectFile);
                }
                // I think after refactor this is no longer necessary but keeping the code until we test ... just in case.
                // else
                // {
                //     // The Safe path did not work ... attempt to resolve via hashGuid.
                //     var suppliedFile = GetSuppliedFile(includePath);
                //     includedProjectHashGuid = suppliedFile.Id;
                //     if (!mapFiles.ProjectFiles.TryGetValue(includedProjectHashGuid, out includedProjectFile))
                //     {
                //         // Finally try to load the project file
                //         includedProjectFile = LoadProjectFileContents(suppliedFile);
                //         mapFiles.SafeAddFile(includedProjectFile);
                //     }
                // }
            }
        }

        public void MapProjectLevelDependencies(MapFiles mapFiles)
        {
            foreach (var projectFile in mapFiles.GetAllProjectFiles())
            {
                if (projectFile.Status == FileStatus.Valid)
                {
                    MapProjectLevelDependenciesForProjectFile(mapFiles, projectFile);
                }
            }
        }

        // TODO: This needs to be moved to the extension system once its in place.
        public string DetermineProjectLanguage(SuppliedProjectFile suppliedProjectFile)
        {
            var fs = AbstractionManager.GetFileSystem();
            var extension = fs.GetExtension(suppliedProjectFile.Paths.Relative);
            switch (extension)
            {
                case ".csproj":
                    return Language.CSharp;
                case ".fsproj":
                    return Language.FSharp;
                case ".vbproj":
                    return Language.VisualBasic;
                default:
                    return Language.Unknown;
            }
        }

        public Map ConvertMapFilesToMap(string workingDirectory, MapFiles mapFiles)
        {
            var fs = AbstractionManager.GetFileSystem();
            Map map = new Map();
            map.Build.RelativeRoot = workingDirectory;
            var projectFiles = mapFiles.GetAllProjectFiles();
            foreach (var projectFile in projectFiles)
            {
                var project = new Model.Project();
                project.Id = projectFile.Id;
                project.Language = DetermineProjectLanguage(projectFile);
                project.Name = fs.GetNameWithoutExtension(projectFile.Paths.Relative);
                var solutionNames =
                (from s in projectFile.SolutionFiles
                 select fs.GetName(s.Paths.Relative));
                project.Solutions.AddRange(solutionNames);
                project.RelativePath = projectFile.Paths.Relative;
                project.AbsolutePath = projectFile.Paths.Absolute;
                project.RealPath = projectFile.Paths.Real;
                project.Status = projectFile.Status;
                project.DependentOn.AddRange(projectFile.ProjectDependentOn.Keys);
                map.Build.Projects.Add(project);
            }

            return map;
        }

        public Map BaseOrderMap(Map map)
        {
            foreach (var project in map.Build.Projects)
            {
                if (project.Status != FileStatus.Valid)
                {
                    project.Group = -1;
                }
            }

            return map;
        }

        public Map Create(string workingDirectory, IEnumerable<string> globPatterns)
        {
            var fileSearcher = new FileSearcher();
            workingDirectory.ArgumentNotNullNorEmptyNorWhiteSpace(nameof(workingDirectory));

            var fs = AbstractionManager.GetFileSystem();
            workingDirectory = fs.NormalizePath(workingDirectory);
            IEnumerable<FilePaths> locatedFiles = fileSearcher.LocateFiles(workingDirectory, globPatterns);
            MapFiles mapFiles = new MapFiles(workingDirectory);
            mapFiles.PreprocessList(locatedFiles);

            IncorporateSolutionFileProjects(mapFiles);

            MapProjectLevelDependencies(mapFiles);

            foreach (var suppliedProjectFile in mapFiles.GetAllProjectFiles())
            {
                var circularReferenceChecker = new ProjectCircularReferenceChecker(suppliedProjectFile);
                if (circularReferenceChecker.ValidateCircularReferences())
                {
                    suppliedProjectFile.Status = FileStatus.Circular;
                }
            }
            var map = ConvertMapFilesToMap(workingDirectory, mapFiles);
            BaseOrderMap(map);
            return map;
        }
    }
}