namespace ProjectOrder.Parsers
{
    using System.IO;
    using System.Text.RegularExpressions;
    using ProjectOrder.Helpers;
    using ProjectOrder.Model;

    public class SolutionFileParser
    {
        public enum LineType
        {
            Ignore = 0,
            StartProject = 1,
            EndProject = 2,
            StartDependencyList = 3,
            EndDependencyList = 4,
            DependencyLine = 5,
        }

        public enum State
        {
            NotInProject = 0,
            InSolutionFolder = 1,
            InProject = 2,
            InProjectDependencies = 3,
        }

        private const string DependencyLinePattern =
            @"\s*\{([a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12})\}\s*=\s*\{([a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12})\}.*";
        private const string ProjectLinePattern =
            @".*Project\(\s*""\s*\{([a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12})\}\s*""\s*\)\s*=\s*""([^""]*)""\s*,\s*""([^""]*)""\s*,\s*""\s*\{([a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12})\}\s*"".*";

        private const string SolutionFolderId = @"2150E333-8FDC-42A3-9474-1A3956D46DE8";
        private readonly Regex DependencyLineRegex = new Regex(DependencyLinePattern, RegexOptions.Compiled);
        private readonly Regex ProjectLineRegex = new Regex(ProjectLinePattern, RegexOptions.Compiled);

        private ProjectFileReference currentProject;

        private State SolutionFileState = State.NotInProject;

        public SolutionFileParser(string solutionFileFilePath)
        {
            SolutionFile = new VisualStudioSolutionFile();
            SolutionFile.FilePath = solutionFileFilePath;
            using (var stream = File.OpenRead(solutionFileFilePath))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    SolutionFile.RawFileText.Add(line);
                }
            }
        }

        public VisualStudioSolutionFile SolutionFile { get; private set; }

        public VisualStudioSolutionFile Parse()
        {
            SolutionFileState = State.NotInProject;
            foreach (var line in SolutionFile.RawFileText)
            {
                ProcLine(line);
            }

            return SolutionFile;
        }

        private LineType DetermineLineTyeType(string line)
        {
            var retval = LineType.Ignore;
            return retval;
        }

        private LineType DetermineLineType(string line)
        {
            if (line.StartsWith(@"Project("""))
            {
                return LineType.StartProject;
            }

            if (line.Contains("EndProjectSection"))
            {
                return LineType.EndDependencyList;
            }

            if (line.StartsWith("EndProject"))
            {
                return LineType.EndProject;
            }

            if (line.Contains("ProjectSection(ProjectDependencies)"))
            {
                return LineType.StartDependencyList;
            }

            if (SolutionFileState == State.InProjectDependencies)
            {
                return LineType.DependencyLine;
            }

            return LineType.Ignore;
        }

        private void ProcDependencyLine(string line)
        {
            var match = DependencyLineRegex.Match(line);
            if (match.Groups.Count == 3)
            {
                currentProject.DependentOnIds.Add(match.Groups[1].Value);
            }
        }

        private void ProcLine(string line)
        {
            var lineType = DetermineLineType(line);
            switch (lineType)
            {
                case LineType.StartProject:
                    ProcStartProject(line);
                    break;
                case LineType.EndProject:
                    currentProject = null;
                    SolutionFileState = State.NotInProject;
                    break;
                case LineType.StartDependencyList:
                    SolutionFileState = State.InProjectDependencies;
                    break;
                case LineType.EndDependencyList:
                    SolutionFileState = State.InProject;
                    break;
                case LineType.DependencyLine:
                    ProcDependencyLine(line);
                    break;
            }
        }

        private void ProcStartProject(string line)
        {
            var match = ProjectLineRegex.Match(line);
            if (match.Groups.Count == 5)
            {
                if (match.Groups[1].Value.ToUpperInvariant() == SolutionFolderId)
                {
                    SolutionFileState = State.InSolutionFolder;
                    return;
                }

                currentProject = new ProjectFileReference
                {
                    Id = match.Groups[4].Value.ToUpperInvariant(),
                    Name = match.Groups[2].Value,
                    FilePath = XPlatHelper.FullyNormalizePath(SolutionFile.Directory, match.Groups[3].Value)
                };
                SolutionFile.ProjectFiles.Add(currentProject);
            }

            SolutionFileState = State.InProject;
        }
    }
}
