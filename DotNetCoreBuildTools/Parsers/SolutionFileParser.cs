using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using ProjectOrder.Helpers;
using ProjectOrder.Model;

namespace ProjectOrder.Parsers
{
   public class SolutionFileParser
   {

      public enum State
      {
         NotInProject,
         InSolutionFolder,
         InProject,
         InProjectDependencies,
      }

      public enum LineType
      {
         Ignore,
         StartProject,
         EndProject,
         StartDependencyList,
         EndDependencyList,
         DependencyLine
      }

      public VisualStudioSolutionFile SolutionFile { get; private set; }

      public SolutionFileParser(string solutionFileFilePath)
      {
         this.SolutionFile = new VisualStudioSolutionFile();
         this.SolutionFile.FilePath = solutionFileFilePath;
         using (var stream = File.OpenRead(solutionFileFilePath))
         using (var reader = new StreamReader(stream))
         {
            while (!reader.EndOfStream)
            {
               var line = reader.ReadLine();
               this.SolutionFile.RawFileText.Add(line);
            }
         }
      }

      private LineType DetermineLineTyeType(string line)
      {
         var retval = LineType.Ignore;
         return retval;
      }

      private State SolutionFileState = State.NotInProject;

      private LineType DeterminLineType(string line)
      {
         if (line.StartsWith(@"Project("""))
         {
            return LineType.StartProject;
         }
         else if (line.Contains("EndProjectSection"))
         {
            return LineType.EndDependencyList;
         }
         else if (line.StartsWith("EndProject"))
         {
            return LineType.EndProject;
         }
         else if (line.Contains("ProjectSection(ProjectDependencies)"))
         {
            return LineType.StartDependencyList;
         }
         else if (SolutionFileState == State.InProjectDependencies)
         {
            return LineType.DependencyLine;
         }

         return LineType.Ignore;
      }


      void ProcDependencyLine(string line)
      {
         var match = DependencyLineRegex.Match(line);
         if (match.Groups.Count == 3)
         {
            this.currentProject.DependentOnIds.Add(match.Groups[1].Value);
         }
      }
      
      private ProjectFileReference currentProject = null;
      private const string ProjectLinePattern = @".*Project\(\s*""\s*\{([a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12})\}\s*""\s*\)\s*=\s*""([^""]*)""\s*,\s*""([^""]*)""\s*,\s*""\s*\{([a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12})\}\s*"".*";
      private readonly Regex ProjectLineRegex = new Regex(ProjectLinePattern, RegexOptions.Compiled);
      
      private const string DependencyLinePattern = @"\s*\{([a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12})\}\s*=\s*\{([a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12})\}.*";
      private readonly Regex DependencyLineRegex = new Regex(DependencyLinePattern, RegexOptions.Compiled);


      private const string SolutionFolderId = @"2150E333-8FDC-42A3-9474-1A3956D46DE8";
      void ProcStartProject(string line)
      {
         var match = ProjectLineRegex.Match(line);
         if (match.Groups.Count == 5)
         {
            if (match.Groups[1].Value.ToUpperInvariant() == SolutionFolderId)
            {
               this.SolutionFileState = State.InSolutionFolder;
               return;
            }
            this.currentProject = new ProjectFileReference()
            {
               Id = match.Groups[4].Value.ToUpperInvariant(),
               Name = match.Groups[2].Value,
               FilePath = XPlatHelper.FullyNormalizePath(this.SolutionFile.Directory, match.Groups[3].Value)
            };
            this.SolutionFile.ProjectFiles.Add(this.currentProject);
         }

         this.SolutionFileState = State.InProject;
      }
      
      void ProcLine(string line)
      {
         var lineType = DeterminLineType(line);
         switch (lineType)
         {
            case LineType.StartProject:
               ProcStartProject(line);
               break;
            case LineType.EndProject:
               this.currentProject = null;
               this.SolutionFileState = State.NotInProject;
               break;
            case LineType.StartDependencyList:
               this.SolutionFileState = State.InProjectDependencies;
               break;
            case LineType.EndDependencyList:
               this.SolutionFileState = State.InProject;
               break;
            case LineType.DependencyLine:
               ProcDependencyLine(line);
               break;
         }
      }

      public VisualStudioSolutionFile Parse()
      {
         this.SolutionFileState = State.NotInProject;
         foreach(var line in SolutionFile.RawFileText)
         {
            ProcLine(line);
         }
         return SolutionFile;
      }
   }
}
