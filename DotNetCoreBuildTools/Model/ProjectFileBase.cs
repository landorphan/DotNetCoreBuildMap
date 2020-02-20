using System;
using System.IO;
using ProjectOrder.Attributes;

namespace ProjectOrder.Model
{
   public class ProjectFileBase
   {
      public ProjectFileBase() {}

      public ProjectFileBase(string filePath)
      {
         this.FilePath = filePath;
         this.Name = Path.GetFileName(filePath);
      }

      public ProjectFileBase(ProjectFileBase original)
      {
         this.Id = original.Id;
         this.Name = original.Name;
         this.FilePath = original.FilePath;
      }
      
      [DisplayInMap]
      public string Id { get; set; } = Guid.NewGuid().ToString().ToUpperInvariant();

      [DisplayInMap]
      public string Name { get; set; }

      [DisplayInMap]
      public string FilePath { get; set; }
      
      public string Directory => Path.GetDirectoryName(this.FilePath);
   }
}