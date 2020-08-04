using System;
using System.Collections.Generic;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
    public class MapFiles
    {
        public List<string> LocatedFiles { get; set; } = new List<string>();
        public List<SuppliedFile> SuppliedFiles { get; set; } = new List<SuppliedFile>();
        public Dictionary<Guid, ProjectFile> ProjectFiles { get; private set; } = new Dictionary<Guid, ProjectFile>();
        public Dictionary<Guid, SolutionFile> SolutionFiles { get; private set; } = new Dictionary<Guid, SolutionFile>();

        public void SafeAddFile(ProjectFile projectFile)
        {
            if (!ProjectFiles.TryGetValue(projectFile.Id, out _))
            {
                ProjectFiles.Add(projectFile.Id, projectFile);
            }
        }

        public void SafeAddFile(SolutionFile solutionFile)
        {
            if (!SolutionFiles.TryGetValue(solutionFile.Id, out _))
            {
                SolutionFiles.Add(solutionFile.Id, solutionFile);
            }
        }
    }
}