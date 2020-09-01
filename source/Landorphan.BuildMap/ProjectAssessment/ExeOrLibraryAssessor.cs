using Landorphan.BuildMap.Construction.SolutionModel;

namespace Landorphan.BuildMap.ProjectAssessment
{
    public class ExeOrLibraryAssessor : IProjectTypeAssessor
    {
        public const string Library = "Library";
        public const string Executable = "Executable";
        
        public string AssesProjectType(SuppliedProjectFile suppliedProjectFile)
        {
            return Library;
        }
    }
}