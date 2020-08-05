namespace Landorphan.BuildMap.Abstractions.VisualStudioSolutionFile
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Build.Construction;

    public interface IProjectInSoltuion
    {
        IEnumerable<Guid> GetProjectsThisProjectDependsOn();
        string AbsolutePath { get; }
        string ProjectName { get; }

        Guid SlnGuid { get; }
        
        SolutionProjectType ProjectType { get; }
        string RelativePath { get; }
    }
}