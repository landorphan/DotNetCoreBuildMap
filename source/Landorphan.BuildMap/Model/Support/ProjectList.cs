using System;
using System.Collections.Generic;

namespace Landorphan.BuildMap.Model.Support
{
    [Serializable]
    public class ProjectList : List<Project>
    {
        public ProjectList()
        {
            
        }
        
        public ProjectList(ICollection<Project> projects) : base(projects)
        {
        }

        public static implicit operator ProjectList(Project[] projects)
        {
            return new ProjectList(projects);
        }
    }
}