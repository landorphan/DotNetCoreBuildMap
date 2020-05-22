namespace ProjectOrder.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using ProjectOrder.Attributes;
    using ProjectOrder.Helpers;

    public class ProjectFile : ProjectFileBase, IComparable<ProjectFile>, IComparer<ProjectFile>
    {
        public enum PackageType
        {
            None,
            PackageOnBuild,
            PackageStepNeeded
        }

        public enum TypeOfProject
        {
            Unknown,
            Library,
            Application,
            Web,
            Test
        }

        public const string Exe = "Exe";
        public const string TestSdk = "Microsoft.NET.Test.Sdk";

        public const string WebSdk = "Microsoft.NET.Sdk.Web";

        public ProjectFile()
        {
        }

        public ProjectFile(string path) : base(path)
        {
        }

        public ProjectFile(ProjectFileReference original) : base(original)
        {
        }

        [DisplayInMap]
        public int BuildGroup { get; set; }

        public List<ProjectFile> DependentOn { get; set; } = new List<ProjectFile>();

        [DisplayInMap]
        public PackageType Package { get; set; }

        [DisplayInMap]
        public TypeOfProject ProjectType { get; set; }

        [DisplayInMap]
        public string Solution { get; set; }

        public int CompareTo(ProjectFile other)
        {
            return Compare(this, other);
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

        public void ParseAndResolve(Dictionary<string, ProjectFile> masterProjectList)
        {
            var xDocument = XDocument.Load(FilePath);
            var sdk = ((xDocument.XPathEvaluate("/Project/@Sdk") as IEnumerable<object>)?.FirstOrDefault() as XAttribute)?.Value;
            var outputType = xDocument.XPathSelectElement("/Project/PropertyGroup[not(@Condition) or @Condition='']/OutputType")?.Value;
            var packageReferences = (xDocument.XPathEvaluate("/Project/ItemGroup/PackageReference/@Include") as IEnumerable<object>)?.Cast<XAttribute>();
            var projectReferences = (xDocument.XPathEvaluate("/Project/ItemGroup/ProjectReference/@Include") as IEnumerable<object>)?.Cast<XAttribute>();
            var canPackageReference = xDocument.XPathSelectElement("/Project/PropertyGroup[not(@Condition) or @Condition='']/IsPackable")?.Value;
            var packageIdReference = xDocument.XPathSelectElement("/Project/PropertyGroup[not(@Condition) or @Condition='']/PackageId")?.Value;
            var packageOnBuildReference = xDocument.XPathSelectElement("/Project/PropertyGroup[not(@Condition) or @Condition='']/GeneratePackageOnBuild")?.Value;

            if (string.Equals(sdk, WebSdk, StringComparison.OrdinalIgnoreCase))
            {
                ProjectType = TypeOfProject.Web;
            }
            else if (packageReferences.Any(a => string.Equals(a.Value, TestSdk, StringComparison.OrdinalIgnoreCase)))
            {
                ProjectType = TypeOfProject.Test;
            }
            else if (string.Equals(outputType, Exe, StringComparison.OrdinalIgnoreCase))
            {
                ProjectType = TypeOfProject.Application;
            }
            else
            {
                ProjectType = TypeOfProject.Library;
            }

            foreach (var include in projectReferences)
            {
                var includePath = XPlatHelper.FullyNormalizePath(Directory, include.Value);
                DependentOn.Add(masterProjectList[includePath]);
            }

            if (bool.TryParse(canPackageReference, out var canPackage) &&
                canPackage &&
                !string.IsNullOrWhiteSpace(packageIdReference))
            {
                if (bool.TryParse(packageOnBuildReference, out var packageOnBuild) && packageOnBuild)
                {
                    Package = PackageType.PackageOnBuild;
                }
                else
                {
                    Package = PackageType.PackageStepNeeded;
                }
            }
        }
    }
}
