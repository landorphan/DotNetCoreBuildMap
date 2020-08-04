using System;
using System.Collections.Generic;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
    public class MapFiles
    {
        public List<string> LocatedFiles { get; set; } = new List<string>();
        public List<SuppliedFile> SuppliedFiles { get; set; } = new List<SuppliedFile>();
        public Dictionary<Guid, ProjectFile> ProjectFiles { get; set; } = new Dictionary<Guid, ProjectFile>();
        public Dictionary<Guid, SolutionFile> SolutionFiles { get; set; } = new Dictionary<Guid, SolutionFile>();
    }
}