using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;
using ProjectOrder.Helpers;
using ProjectOrder.Model;
using ProjectOrder.Parsers;

namespace ProjectOrder
{
    public class CreateOrder // : IComparer<KeyValuePair<Guid, List<Guid>>>
    {
        //public int InternalCompare(KeyValuePair<Guid, List<Guid>> x, KeyValuePair<Guid, List<Guid>> y)
        //{
        //    if (y.Value.Contains(x.Key))
        //    {
        //        return 1;
        //    }
        //    if (x.Value.Contains(y.Key))
        //    {
        //        return -1;
        //    }
        //    return x.Key.CompareTo(y.Key);
        //}
        //public int Compare(KeyValuePair<Guid, List<Guid>> x, KeyValuePair<Guid, List<Guid>> y)
        //{
        //    var result = InternalCompare(x,y);
        //    if (result == 0)
        //    {
        //        Console.WriteLine($"{x.Key.ToString().ToUpperInvariant()}={y.Key.ToString().ToUpperInvariant()}");
        //    }
        //    else if (result < 0)
        //    {
        //        Console.WriteLine($"{x.Key.ToString().ToUpperInvariant()}<{y.Key.ToString().ToUpperInvariant()}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{x.Key.ToString().ToUpperInvariant()}>{y.Key.ToString().ToUpperInvariant()}");
        //    }
        //    return result;
        //}

        public static void Main(string[] args)
        {
            var me = new CreateOrder();
            var runParameters = new RunParameters();
            runParameters.PathList.Add(@"/Volumes/[C] tistocks-v0/temp/NewProject/NewProjectA/NewProjectA.sln");
            runParameters.PathList.Add(@"/Volumes/[C] tistocks-v0/temp/NewProject/NewProjectB/NewProjectB.sln");
            me.Run(runParameters);
        }

        private List<SolutionFileParser> SolutionFileParsers = new List<SolutionFileParser>();
        private List<ProjectFileReference> ProjectFiles = new List<ProjectFileReference>(); 
        private Dictionary<string, ProjectFile> MasterProjectList = new Dictionary<string, ProjectFile>();

        public void AssemblyProjectList(RunParameters runParameters)
        {
            foreach (var path in runParameters.PathList)
            {
                string workingPath = XPlatHelper.FullyNormalizePath(Directory.GetCurrentDirectory(), path);
                if (workingPath.EndsWith(".sln", StringComparison.OrdinalIgnoreCase))
                {
                    this.SolutionFileParsers.Add(new SolutionFileParser(workingPath));
                }
                else
                {
                    this.ProjectFiles.Add(new ProjectFileReference(workingPath));
                }
            }
        }

        public void AddIndependentProjectFiles()
        {
            foreach (var projectFileReference in this.ProjectFiles)
            {
                if (!this.MasterProjectList.ContainsKey(projectFileReference.FilePath))
                {
                    this.MasterProjectList.Add(projectFileReference.FilePath, new ProjectFile(projectFileReference));
                }
            }
        }

        public void ParseSolutionFiles()
        {
            foreach (var parser in SolutionFileParsers)
            {
                Dictionary<string, Tuple<ProjectFileReference, ProjectFile>> solutionFilesById = new Dictionary<string, Tuple<ProjectFileReference, ProjectFile>>();
                var projectFilesInSolution = parser.Parse();
                // TWO passes are needed here to ensure all dependencies are tracked.
                // The first pass simply find all projects in the solution by their ID.
                foreach (var projectFileReference in projectFilesInSolution.ProjectFiles)
                {
                    var projectFile = new ProjectFile(projectFileReference) { Solution = parser.SolutionFile.Name};
                    var tuple = new Tuple<ProjectFileReference, ProjectFile>(projectFileReference, projectFile);
                    solutionFilesById.Add(projectFileReference.Id, tuple);
                    this.MasterProjectList.Add(projectFileReference.FilePath, projectFile);
                }
                // The second pass creates the solution file object and associates any Solution dependencies
                foreach (var projectFileTuple in solutionFilesById)
                {
                    foreach (var projectId in projectFileTuple.Value.Item1.DependentOnIds)
                    {
                        projectFileTuple.Value.Item2.DependentOn.Add(solutionFilesById[projectId].Item2);
                    }
                }
            }
        }

        public void ResolveAllProjectReferences()
        {
            foreach (var projectItem in MasterProjectList)
            {
                projectItem.Value.ParseAndResolve(this.MasterProjectList);
            }
        }

        public void WriteProjectListOutput()
        {
            foreach (var projectFile in MasterProjectList.Values.OrderBy(x => x.BuildGroup))
            {
                int i = 0;
                Console.Write($"|{i}>{projectFile.BuildGroup}<{i++}");
                Console.Write($"|{i}>{projectFile.Id}<{i++}");
                Console.Write($"|{i}>{projectFile.ProjectType}<{i++}");
                Console.Write($"|{i}>{projectFile.Name}<{i++}");
                Console.Write($"|{i}>{projectFile.Solution}<{i++}");
                Console.Write($"|{i}>{projectFile.FilePath}<{i++}");
                Console.WriteLine("|");
            }
        }

        public void DetermineProjectBuildGroups()
        {
            foreach (var projectFile in MasterProjectList.Values.OrderBy(x => x))
            {
                if (projectFile.DependentOn.Any())
                {
                    projectFile.BuildGroup = projectFile.DependentOn.Max(p => p.BuildGroup) + 1;
                }
            }
        }

        public void Run(RunParameters runParameters)
        {
            AssemblyProjectList(runParameters);
            ParseSolutionFiles();
            AddIndependentProjectFiles();
            ResolveAllProjectReferences();
            DetermineProjectBuildGroups();
            WriteProjectListOutput();
            
            //Dictionary<Guid, List<Guid>> orderMap = new Dictionary<Guid, List<Guid>>();
            //using (var stream = File.OpenRead(args[0]))
            //using (var reader = new StreamReader(stream))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        var parts = line.Split("->");
            //        var project = Guid.Parse(parts[0]);
            //        var dependency = Guid.Parse(parts[1]);
            //        if (!orderMap.TryGetValue(project, out var dependencies))
            //        {
            //            dependencies = new List<Guid>();
            //            orderMap.Add(project, dependencies);
            //        }
            //        dependencies.Add(dependency);
            //    }
            //}
            //var ordered = orderMap.ToList();
            //ordered.Sort(this);
            //foreach (var pair in ordered)
            //{
            //    foreach(var item in pair.Value)
            //    {
            //        Console.WriteLine($"{pair.Key.ToString().ToUpperInvariant()}->{item.ToString().ToUpperInvariant()}");
            //    }
            //}
        }
    }
}