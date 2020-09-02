namespace Landorphan.BuildMap.Construction.SolutionModel
{
    using System;
    using System.Collections.Generic;
    using Landorphan.BuildMap.Abstractions.VisualStudioSolutionFile;

    public class SuppliedSolutionFile : SuppliedFile
    {
        public SuppliedSolutionFile(SuppliedFile original) : base(original)
        {
        }

        public List<SuppliedProjectFile> Projects { get; set; } = new List<SuppliedProjectFile>();
        public Dictionary<Guid, Guid> SlnGuidToHashGuidLookup { get; private set; } = new Dictionary<Guid, Guid>();

        public ISolutionFile SolutionContents { get; set; }
    }
}
