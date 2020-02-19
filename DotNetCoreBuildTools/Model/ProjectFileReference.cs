using System.Collections.Generic;
using System.Text;

namespace ProjectOrder.Model
{
   public class ProjectFileReference : ProjectFileBase
   {
      public ProjectFileReference() : base()
      {
      }

      public ProjectFileReference(string path) : base(path)
      {
      }

      public ProjectFileReference(ProjectFileReference original) : base(original)
      {
         this.DependentOnIds.AddRange(original.DependentOnIds);
      }

      public List<string> DependentOnIds { get; set; } = new List<string>();
   }
}
