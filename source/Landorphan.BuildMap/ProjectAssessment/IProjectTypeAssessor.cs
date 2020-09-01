using Landorphan.BuildMap.Construction.SolutionModel;

namespace Landorphan.BuildMap.ProjectAssessment
{
    public interface IProjectTypeAssessor
    {
        string AssesProjectType(SuppliedProjectFile suppliedProjectFile);
    }
}