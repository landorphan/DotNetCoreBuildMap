using System;
using System.Collections.Generic;
using System.Xml;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
    using System.Xml.Linq;

    public class SuppliedProjectFile : SuppliedFile
    {
        public SuppliedProjectFile(SuppliedFile original) : base(original)
        {
        }
        
        public XDocument ProjectContents { get; set; }
        public List<SuppliedSolutionFile> SolutionFiles { get; set; } = new List<SuppliedSolutionFile>();
        public Dictionary<Guid, SuppliedProjectFile> DependentOn { get; private set; } = new Dictionary<Guid, SuppliedProjectFile>();
    }
}