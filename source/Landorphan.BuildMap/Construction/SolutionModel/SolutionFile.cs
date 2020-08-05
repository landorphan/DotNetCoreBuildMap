using System;
using System.Collections.Generic;
using Landorphan.BuildMap.Model.Support;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
    using Onion.SolutionParser.Parser.Model;

    public class SolutionFile : SuppliedFile
    {
        public SolutionFile(SuppliedFile original) : base(original)
        {
        }

        public ISolution SolutionContents { get; set; }
        public Dictionary<Guid, Guid> SlnGuidToHashGuidLookup { get; private set; } = new Dictionary<Guid, Guid>(); 
        public List<ProjectFile> Projects { get; set; } = new List<ProjectFile>(); 
    }
}