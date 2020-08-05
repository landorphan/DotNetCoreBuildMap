using System;
using System.Collections.Generic;
using System.Xml;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
    using System.Xml.Linq;

    public class ProjectFile : SuppliedFile
    {
        public ProjectFile(SuppliedFile original) : base(original)
        {
        }
        
        public XDocument ProjectContents { get; set; }
        public List<SolutionFile> SolutionFiles { get; set; } = new List<SolutionFile>();
        public Dictionary<Guid, ProjectFile> DependentOn { get; private set; } = new Dictionary<Guid, ProjectFile>();
    }
}