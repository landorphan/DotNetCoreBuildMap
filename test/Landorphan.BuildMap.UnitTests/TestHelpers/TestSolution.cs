namespace Landorphan.BuildMap.UnitTests.TestHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TestSolution
    {
        public List<string> BuildConfigurations = new List<string>
        {
            "Debug|Any CPU"
        };
        private const string EndGlobalLine = "EndGlobal";
        private const string EndGlobalSection = "    EndGlobalSection";

        private const string ProjectLineEnd = "EndProject";
        private const string SolutionDependenciesEnd = "    EndProjectSection";
        private const string SolutionDependenciesStart = "    ProjectSection(ProjectDependencies) = postProject";

        private const string StartBuildConfigurationPlatforms = "    GlobalSection(ProjectConfigurationPlatforms) = postSolution";

        private const string StartConfigurationPlatforms = "    GlobalSection(SolutionConfigurationPlatforms) = preSolution";

        private const string StartGlobalLine = "Global";

        private const string StartSolutionExtGlobals = "    GlobalSection(ExtensibilityGlobals) = postSolution";

        private const string StartSolutionProperties = "    GlobalSection(SolutionProperties) = preSolution";

        private const string VisualStudioHeader = "Microsoft Visual Studio Solution File, Format Version 12.00";
        private const string VisualStudioMinimumVersion = "MinimumVisualStudioVersion = 15.0.26124.0";
        private const string VisualStudioVersionComment = "# Visual Studio Version 16";
        private const string VisualStudioVersionLine = "VisualStudioVersion = 16.0.30225.117";
        public string FileName => $"{Name}.sln";
        public string Name { get; set; }

        public List<TestProject> Projects { get; set; } = new List<TestProject>();
        public Dictionary<Guid, List<Guid>> SolutionDependencies { get; set; } = new Dictionary<Guid, List<Guid>>();
        public Guid SolutionId { get; set; } = Guid.NewGuid();

        public void AddSolutionDependency(TestProject project, TestProject dependentOnProject)
        {
            if (!SolutionDependencies.TryGetValue(project.Id, out var dependentOnList))
            {
                dependentOnList = new List<Guid>();
                SolutionDependencies.Add(project.Id, dependentOnList);
            }

            if (!dependentOnList.Contains(dependentOnProject.Id))
            {
                dependentOnList.Add(dependentOnProject.Id);
            }
        }

        public string GetFileContent()
        {
            var builder = new StringBuilder();
            builder.AppendLine(VisualStudioHeader);
            builder.AppendLine(VisualStudioVersionComment);
            builder.AppendLine(VisualStudioVersionLine);
            builder.AppendLine(VisualStudioMinimumVersion);

            foreach (var project in Projects)
            {
                builder.AppendLine(GenerateProjectLineStart(project));
                if (SolutionDependencies.TryGetValue(project.Id, out var dependentOnProjects))
                {
                    builder.AppendLine(SolutionDependenciesStart);
                    foreach (var dependentOnProject in dependentOnProjects)
                    {
                        builder.AppendLine(GenerateSolutionDependencyLine(dependentOnProject));
                    }

                    builder.AppendLine(SolutionDependenciesEnd);
                }

                builder.AppendLine(ProjectLineEnd);
            }

            builder.AppendLine(StartGlobalLine);
            builder.AppendLine(StartConfigurationPlatforms);
            foreach (var buildConfiguration in BuildConfigurations)
            {
                builder.AppendLine(GenerateConfigurationLine(buildConfiguration));
            }

            builder.AppendLine(EndGlobalSection);
            builder.AppendLine(StartBuildConfigurationPlatforms);
            foreach (var project in Projects)
            {
                foreach (var buildConfig in BuildConfigurations)
                {
                    builder.AppendLine(GenerateBuildConfigurationPlatformLine(project, buildConfig));
                }
            }

            builder.AppendLine(EndGlobalSection);
            builder.AppendLine(StartSolutionProperties);
            builder.AppendLine("        HideSolutionNode = FALSE");
            builder.AppendLine(EndGlobalSection);

            builder.AppendLine(StartSolutionExtGlobals);
            builder.AppendLine($"        SolutionGuid = {{{SolutionId}}}");
            builder.AppendLine(EndGlobalSection);
            builder.AppendLine(EndGlobalLine);

            return builder.ToString();
        }

        private string GenerateBuildConfigurationPlatformLine(TestProject project, string buildConfig)
        {
            return $"        {{{project.Id}}}.{buildConfig}.Build.0 = {buildConfig}";
        }

        private string GenerateConfigurationLine(string buildConfig)
        {
            return $"        {{{buildConfig}}} = {{{buildConfig}}}";
        }

        private string GenerateProjectLineStart(TestProject project)
        {
            return $"Project(\"{{{project.ProjectGuid}}}\") = \"{project.Name}\", \"{project.RelativePath}\", \"{{{project.Id}}}\"";
        }

        private string GenerateSolutionDependencyLine(Guid projectId)
        {
            return $"        {{{projectId}}} = {{{projectId}}}";
        }
    }
}
