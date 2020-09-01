using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.BuildMap.Construction
{
    using System.Linq;
    using Landorphan.BuildMap.Construction.SolutionModel;
    using Landorphan.BuildMap.Model;

    public class ProjectCircularReferenceChecker
    {
        private SuppliedProjectFile project;
        private List<Guid> visitedProjectFiles = new List<Guid>();
        private List<SuppliedProjectFile> circularReferences = new List<SuppliedProjectFile>();
        public ProjectCircularReferenceChecker(SuppliedProjectFile project)
        {
            this.project = project;
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
                        continue;
                    }
                    else
                    {
                        ValidateCircularReferencesInternalLoop(dependentOnProject.Value);
                    }
                }
            }
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
    }
}
