namespace Landorphan.BuildMap.Abstractions.VisualStudioSolutionFile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Build.Construction;

    public class SolutionFileAbstraction : ISolutionFile
    {
        private SolutionFile slnFile;

        public SolutionFileAbstraction(SolutionFile slnFile)
        {
            this.slnFile = slnFile;
        }

        public IEnumerable<IProjectInSolution> GetAllProjects()
        {
            var retval = new List<IProjectInSolution>(
                from p in slnFile.ProjectsInOrder
                select new ProjectInSolutionAbstraction(p));
            return retval;
        }

        public bool TryGetProjectBySlnGuid(Guid slnGuid, out IProjectInSolution projectInSolution)
        {
            projectInSolution = null;
            ProjectInSolution projectSolution;
            bool retval;
            if (retval = slnFile.ProjectsByGuid.TryGetValue(slnGuid.ToString(), out projectSolution))
            {
                projectInSolution = new ProjectInSolutionAbstraction(projectSolution);
            }

            return retval;
        }
    }
}
