namespace ProjectOrder.Model
{
    using System.Collections.Generic;

    public class ProjectFileReference : ProjectFileBase
    {
        public ProjectFileReference()
        {
        }

        public ProjectFileReference(string path) : base(path)
        {
        }

        public ProjectFileReference(ProjectFileReference original) : base(original)
        {
            DependentOnIds.AddRange(original.DependentOnIds);
        }

        public List<string> DependentOnIds { get; set; } = new List<string>();
    }
}
