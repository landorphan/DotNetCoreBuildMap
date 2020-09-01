using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.BuildMap.UnitTests.TestAssets.TestHelpers
{
    public class TestProjectsAndSolutions
    {
        public string TestName { get; set; }
        public string TestId { get; set; }
        public Dictionary<string, TestProject> Projects { get; set; } = new Dictionary<string, TestProject>();
        public Dictionary<string, TestSolution> Solutions { get; set; } = new Dictionary<string, TestSolution>();
    }
}
