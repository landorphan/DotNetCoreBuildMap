// ReSharper disable AssignmentInConditionalExpression
namespace Landorphan.BuildMap.Abstractions.VisualStudioSolutionFile
{
    using System;
    using System.Collections.Generic;

    public interface ISolutionFile
    {
        bool TryGetProjectBySlnGuid(Guid slnGuid, out IProjectInSoltuion projectInSolution);
        IEnumerable<IProjectInSoltuion> GetAllProjects();
    }
}
