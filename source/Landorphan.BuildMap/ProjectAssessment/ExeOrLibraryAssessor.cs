namespace Landorphan.BuildMap.ProjectAssessment
{
    using Landorphan.BuildMap.Construction.SolutionModel;

    public class ExeOrLibraryAssessor : IProjectTypeAssessor
    {
        public const string Executable = "Executable";
        public const string Library = "Library";

        public string AssesProjectType(SuppliedProjectFile suppliedProjectFile)
        {
            return Library;
        }
    }
}
