using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.BuildMap.UnitTests.TestHelpers
{
    using Landorphan.BuildMap.Model;

    public class TestMapProject
    {
        public int Group { get; set; }
        public int Item { get; set; }
        public string Types { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public FileStatus Status { get; set; }
        public string Solutions { get; set; }
        public int Id { get; set; }
        public string RelativePath { get; set; }
        public string DependentOn { get; set; }
    }
}
