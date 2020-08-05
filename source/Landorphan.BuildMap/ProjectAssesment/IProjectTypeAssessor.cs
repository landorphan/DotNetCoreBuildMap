using Landorphan.BuildMap.Construction.SolutionModel;

namespace Landorphan.BuildMap.ProjectAssesment
{
    public interface IProjectTypeAssessor
    {
        string AssesProjectType(SuppliedProjectFile suppliedProjectFile);
    }
}