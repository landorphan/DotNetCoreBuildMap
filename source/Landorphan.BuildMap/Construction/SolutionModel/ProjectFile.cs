using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
    public class ProjectFile : SuppliedFile
    {
        public ProjectFile()
        {
        }

        public ProjectFile(SuppliedFile original) : base(original)
        {
        }
        
        public XDocument ProjectContents { get; set; }
        public List<SolutionFile> SolutionFiles { get; set; } = new List<SolutionFile>();
    }
}