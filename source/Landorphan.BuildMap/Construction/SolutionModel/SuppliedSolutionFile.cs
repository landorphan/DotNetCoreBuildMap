using System;
using System.Collections.Generic;
using Landorphan.BuildMap.Model.Support;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
    using Landorphan.BuildMap.Abstractions.VisualStudioSolutionFile;
    using Microsoft.Build.Construction;

    public class SuppliedSolutionFile : SuppliedFile
    {
        public SuppliedSolutionFile(SuppliedFile original) : base(original)
        {
        }

        public ISolutionFile SolutionContents { get; set; }
        public Dictionary<Guid, Guid> SlnGuidToHashGuidLookup { get; private set; } = new Dictionary<Guid, Guid>(); 
        public List<SuppliedProjectFile> Projects { get; set; } = new List<SuppliedProjectFile>(); 
    }
}