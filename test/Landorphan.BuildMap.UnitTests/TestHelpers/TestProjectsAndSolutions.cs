namespace Landorphan.BuildMap.UnitTests.TestAssets.TestHelpers
{
    using System.Collections.Generic;

    public class TestProjectsAndSolutions
    {
        public Dictionary<string, TestProject> Projects { get; set; } = new Dictionary<string, TestProject>();
        public Dictionary<string, TestSolution> Solutions { get; set; } = new Dictionary<string, TestSolution>();
        public string TestId { get; set; }
        public string TestName { get; set; }
    }
}
