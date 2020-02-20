using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using ProjectOrder.Attributes;
using ProjectOrder.Helpers;

namespace ProjectOrder.Model
{
   public class ProjectFile : ProjectFileBase, IComparable<ProjectFile>, IComparer<ProjectFile>
   {
      [DisplayInMap]
      public int BuildGroup { get; set; }
      
      public enum TypeOfProject
      {
         Unknown,
         Library,
         Application,
         Web,
         Test
      }
      
      [DisplayInMap]
      public TypeOfProject ProjectType { get; set; }
      
      [DisplayInMap]
      public string Solution { get; set; } 
      
      public ProjectFile() : base()
      {
      }

      public ProjectFile(string path) : base(path)
      {
      }

      public ProjectFile(ProjectFileReference original) : base(original)
      {
      }
      
      public const string WebSdk = "Microsoft.NET.Sdk.Web";
      public const string TestSdk = "Microsoft.NET.Test.Sdk";
      public const string Exe = "Exe";
      
      public List<ProjectFile> DependentOn { get; set; } = new List<ProjectFile>();

      public void ParseAndResolve(Dictionary<string, ProjectFile> masterProjectList)
      {
         XDocument xDocument = XDocument.Load(this.FilePath);
         string sdk = ((xDocument.XPathEvaluate("/Project/@Sdk") as IEnumerable<object>)?.FirstOrDefault() as XAttribute)?.Value;
         string outputType = (xDocument.XPathSelectElement("/Project/PropertyGroup[not(@Condition) or @Condition='']/OutputType"))?.Value;
         var packageReferences = (xDocument.XPathEvaluate("/Project/ItemGroup/PackageReference/@Include") as IEnumerable<object>)?.Cast<XAttribute>();
         var projectReferences = (xDocument.XPathEvaluate("/Project/ItemGroup/ProjectReference/@Include") as IEnumerable<object>)?.Cast<XAttribute>();
	
         if (string.Equals(sdk, WebSdk, StringComparison.OrdinalIgnoreCase))
         {
            this.ProjectType = TypeOfProject.Web;
         }
         else if (packageReferences.Any(a => string.Equals(a.Value, TestSdk, StringComparison.OrdinalIgnoreCase)))
         {
            this.ProjectType = TypeOfProject.Test;
         }
         else if (string.Equals(outputType, Exe, StringComparison.OrdinalIgnoreCase))
         {
            this.ProjectType = TypeOfProject.Application;
         }
         else 
         {
            this.ProjectType = TypeOfProject.Library;
         }
	
         foreach (var include in projectReferences)
         {
            var includePath = XPlatHelper.FullyNormalizePath(this.Directory, include.Value);
            this.DependentOn.Add(masterProjectList[includePath]);
         }
      }

      public int Compare(ProjectFile x, ProjectFile y)
      {
         if (x.DependentOn.Any(d => d.FilePath == y.FilePath))
         {
            return 1;
         }

         if (y.DependentOn.Any(d => d.FilePath == y.FilePath))
         {
            return -1;
         }

         return string.Compare(x.Id, y.Id, StringComparison.OrdinalIgnoreCase);
      }

      public int CompareTo(ProjectFile other)
      {
         return Compare(this, other);
      }
   }
}