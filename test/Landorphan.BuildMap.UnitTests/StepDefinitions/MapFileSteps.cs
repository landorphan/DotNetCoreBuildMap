using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Landorphan.BuildMap.UnitTests.StepDefinitions
{
    using System.IO;
    using System.Reflection;
    using FluentAssertions;
    using Landorphan.BuildMap.Construction;
    using Landorphan.BuildMap.Model;
    using Landorphan.BuildMap.UnitTests.TestAssets.TestHelpers;
    using Landorphan.BuildMap.UnitTests.TestHelpers;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public sealed class MapFileSteps
    {
        private ScenarioContext scenario;
        private TestProjectsAndSolutions projectsAndSolutions = new TestProjectsAndSolutions();
        private Dictionary<string, TestProject> allProjects = new Dictionary<string, TestProject>();
        private Dictionary<string, TestSolution> allSolutions = new Dictionary<string, TestSolution>();
        private MapManagement mapManagement = new MapManagement();
        private string testDirectory;
        private string testId = Guid.NewGuid().ToString();
        private string currentLocation;
        private string testName;
        private Map map;

        public MapFileSteps(ScenarioContext scenario)
        {
            this.scenario = scenario;
            testName = scenario.ScenarioInfo.Title.Replace(" ", string.Empty, StringComparison.Ordinal).TrimEnd(new char[] {'.', ' '});
            currentLocation = Path.GetDirectoryName(typeof(MapFileSteps).Assembly.Location);
            testDirectory = Path.Combine(currentLocation, "TestExecution", testName, testId);
        }
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario]
        public void BeforeScenario()
        {
            var root = Path.Combine(currentLocation, "TestExecution");
            if (Directory.Exists(root))
            {
                var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    File.Delete(file);
                }

                var directories = Directory.GetDirectories(root, "*.*", SearchOption.AllDirectories);
                foreach (var directory in directories)
                {
                    if (Directory.Exists(directory))
                    {
                        Directory.Delete(directory, true);
                    }
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }

        [Given(@"the projects and solutions are saved on disk")]
        public void GivenTheProjectsAndSolutionsAreSavedOnDisk()
        {
            foreach (var project in allProjects.Values)
            {
                var projectLocation = Path.Combine(testDirectory, project.RelativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(projectLocation));
                if (project.Status != FileStatus.Missing)
                {
                    using (var stream = File.Open(projectLocation, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                    using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        if (project.Status != FileStatus.Empty)
                        {
                            writer.Write(project.GetFileContent());
                            if (project.Status == FileStatus.Malformed)
                            {
                                // This adds an additional "root" node ... which makes the document malformed.
                                writer.Write("<Malformed />");
                            }
                        }
                        else
                        {
                            writer.WriteLine(string.Empty);
                        }
                    }
                }
            }
            foreach (var solution in allSolutions.Values)
            {
                var solutionLocation = Path.Combine(testDirectory, solution.FileName);
                using (var stream = File.OpenWrite(solutionLocation))
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.Write(solution.GetFileContent());
                }
            }

            var files = Directory.GetFiles(testDirectory, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                Console.WriteLine($"Created Entry: {file}");
            }
        }

        private Dictionary<int, Guid> idMap = new Dictionary<int, Guid>();

        [When(@"I create the map file with the following search patterns: (.*)")]
        public void WhenICreateTheMapFile(string searchPatterns)
        {
            map = mapManagement.Create(testDirectory, searchPatterns.Split(';'));
            var projects =
               (from p in map.Build.Projects
             orderby p.Name
              select p);
            // Intentionally 1 adjusted (counting like normal people not developers) so
            // the test case makes more sense to non developers.
            int i = 1;
            foreach (var project in projects)
            {
                idMap.Add(i++, project.Id);
            }
        }
        
        [Then(@"the map file should contain the following projects:")]
        public void ThenTheMapFileShouldContainTheFollowingProjects(Table table)
        {
            List<Project> expectedProjectLists = new List<Project>();
            var orderedExpected = table.CreateSet<TestMapProject>().OrderBy(p => p.Name);
            foreach (var testMapProject in orderedExpected)
            {
                var project = new Project();
                project.Id = idMap[testMapProject.Id];
                project.Group = testMapProject.Group;
                project.Item = testMapProject.Item;
                project.Types = testMapProject.Types.Split(new char[] {',', ';'});
                project.Language = testMapProject.Language;
                project.Name = testMapProject.Name;
                project.Status = testMapProject.Status;
                project.Solutions = (from n in testMapProject.Solutions.Split(new char[] {',', ';'})
                                   select $"{n}.sln").ToArray();
                project.RelativePath = testMapProject.RelativePath.Replace('`', '\\');
                if (!string.IsNullOrWhiteSpace(testMapProject.DependentOn))
                {
                    var strings = testMapProject.DependentOn.Split(new char[] {',', ';'});
                    foreach (var str in strings)
                    {
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            var id = int.Parse(str);
                            var guid = idMap[id];
                            project.DependentOn.Add(guid);
                        }
                    }
                }
                expectedProjectLists.Add(project);
            }

            Project[] actualProjects = map.Build.Projects.ToArray();
            Project[] expectedProjects = expectedProjectLists.ToArray();

            actualProjects.Length.Should().Be(expectedProjects.Length);
            for (int i = 0; i < expectedProjects.Length; i++)
            {
                var expectedProject = expectedProjects[i];
                var actualProject = actualProjects[i];

                actualProject.Id.Should().Be(expectedProject.Id);
                actualProject.Group.Should().Be(expectedProject.Group);
                actualProject.Item.Should().Be(expectedProject.Item);
//                actualProject.Types.Should().BeEquivalentTo(expectedProject.Types);
                actualProject.Language.Should().Be(expectedProject.Language);
                actualProject.Name.Should().Be(expectedProject.Name);
                actualProject.Status.Should().Be(expectedProject.Status);
                actualProject.Solutions.Should().BeEquivalentTo(expectedProject.Solutions);
                actualProject.RelativePath.Should().Be(expectedProject.RelativePath);
                actualProject.DependentOn.Should().BeEquivalentTo(expectedProject.DependentOn);
            }
        }


        [Given(@"the following solutions contain the following located projects:")]
        public void GivenTheFollowingSolutionsContainTheFollowingProjects(Table table)
        {
            foreach (var locatedProjectInSolutions in table.CreateSet<LocatedProjectsInSolutions>())
            {
                var solution = projectsAndSolutions.Solutions[locatedProjectInSolutions.Solution];
                var project = allProjects[locatedProjectInSolutions.Project];
                solution.Projects.Add(project);
            }
        }

        [Given(@"the following projects contain the following references:")]
        public void GivenTheFollowingProjectsContainTheFollowingReferences(Table table)
        {
            foreach (var projectReference in table.CreateSet<TestProjectReference>())
            {
                var project = allProjects[projectReference.Project];
                var reference = allProjects[projectReference.Reference];
                project.References.Add(reference);
            }
        }


        [Given(@"the following solutions define the following additional dependencies:")]
        public void GivenTheFollowingSolutionsDefineTheFollowingAdditionalDependencies(Table table)
        {
            foreach (var solutionDependency in table.CreateSet<TestSolutionDependency>())
            {
                var solution = allSolutions[solutionDependency.Solution];
                var baseProject = allProjects[solutionDependency.BaseProject];
                var dependentOnProject = allProjects[solutionDependency.DependentOn];
                solution.AddSolutionDependency(baseProject, dependentOnProject);
            }
        }

        private List<FileStatus> setableStatuses = new List<FileStatus>(){
                FileStatus.Empty,
                FileStatus.Malformed,
                FileStatus.Missing,
                FileStatus.Valid
            };

        [Given(@"I locate the following project files:")]
        public void GivenIHaveTheFollowingProjectFiles(Table table)
        {
            foreach (var project in table.CreateSet<TestProject>())
            {
                if (!setableStatuses.Contains(project.Status))
                {
                    throw new ArgumentException("Can not create projects of that status here.  Use a different test step.");
                }
                if (project.Status != FileStatus.Missing)
                {
                    projectsAndSolutions.Projects.Add(project.Name, project);
                }
                allProjects.Add(project.Name, project);
            }
        }

        [Given(@"I locate the following solution files:")]
        public void GivenIHaveTheFollowingSolutionFiles(Table table)
        {
            foreach (var solution in table.CreateSet<TestSolution>())
            {
                projectsAndSolutions.Solutions.Add(solution.Name, solution);
                allSolutions.Add(solution.Name, solution);
            }
        }

    }
}
