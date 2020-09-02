// ReSharper disable AssignmentInConditionalExpression

namespace Landorphan.BuildMap.Abstractions.VisualStudioSolutionFile
{
    using System;
    using System.Collections.Generic;

    public interface ISolutionFile
    {
        IEnumerable<IProjectInSolution> GetAllProjects();
        bool TryGetProjectBySlnGuid(Guid slnGuid, out IProjectInSolution projectInSolution);
    }
}
