using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.BuildMap.UnitTests.TestHelpers
{
    class TestSolutionDependency
    {
        public string Solution { get; set; }
        public string BaseProject { get; set; }
        public string DependentOn { get; set; }
    }
}
