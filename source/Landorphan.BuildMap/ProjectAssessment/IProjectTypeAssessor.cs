namespace Landorphan.BuildMap.ProjectAssessment
{
    using Landorphan.BuildMap.Construction.SolutionModel;

    public interface IProjectTypeAssessor
    {
        string AssesProjectType(SuppliedProjectFile suppliedProjectFile);
    }
}
