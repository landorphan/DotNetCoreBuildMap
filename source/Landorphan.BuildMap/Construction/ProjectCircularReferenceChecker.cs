namespace Landorphan.BuildMap.Construction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Landorphan.BuildMap.Construction.SolutionModel;

    public class ProjectCircularReferenceChecker
    {
        private List<SuppliedProjectFile> circularReferences = new List<SuppliedProjectFile>();
        private SuppliedProjectFile project;
        private List<Guid> visitedProjectFiles = new List<Guid>();

        public ProjectCircularReferenceChecker(SuppliedProjectFile project)
        {
            this.project = project;
        }

        public bool ValidateCircularReferences()
        {
            visitedProjectFiles.Clear();
            circularReferences.Clear();
            project.ProjectCircularReferences.Clear();
            foreach (var dependentOnProject in project.ProjectDependentOn)
            {
                ValidateCircularReferencesInternalLoop(dependentOnProject.Value);
            }

            if (circularReferences.Any())
            {
                project.ProjectCircularReferences.AddRange(circularReferences);
                return true;
            }

            return false;
        }

        public void ValidateCircularReferencesInternalLoop(SuppliedProjectFile projectFileToEvaluate)
        {
            if (!visitedProjectFiles.Contains(projectFileToEvaluate.Id))
            {
                visitedProjectFiles.Add(projectFileToEvaluate.Id);
                foreach (var dependentOnProject in projectFileToEvaluate.ProjectDependentOn)
                {
                    if (dependentOnProject.Key == project.Id)
                    {
                        circularReferences.Add(dependentOnProject.Value);
                    }
                    else
                    {
                        ValidateCircularReferencesInternalLoop(dependentOnProject.Value);
                    }
                }
            }
        }
    }
}
