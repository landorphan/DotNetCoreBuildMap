namespace Landorphan.BuildMap.Abstractions.VisualStudioSolutionFile
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Build.Construction;

    public interface IProjectInSolution
    {
        string AbsolutePath { get; }
        string ProjectName { get; }

        SolutionProjectType ProjectType { get; }
        string RelativePath { get; }

        Guid SlnGuid { get; }
        IEnumerable<Guid> GetProjectsThisProjectDependsOn();
    }
}
