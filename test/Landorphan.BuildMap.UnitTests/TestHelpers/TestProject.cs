namespace Landorphan.BuildMap.UnitTests.TestAssets.TestHelpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using Landorphan.BuildMap.Model;

    public class TestProject
    {
        public const string CSharp = "CSharp";
        public const string FSharp = "FSharp";
        public const string Include = "Include";

        public const string ItemGroup = "ItemGroup";
        public const string MicrosoftNetSdk = "Microsoft.NET.Sdk";
        public const string NetStandard2 = "netstandard2.0";
        public const string Project = "Project";
        public const string ProjectReference = "ProjectReference";
        public const string PropertyGroup = "PropertyGroup";
        public const string Sdk = "Sdk";
        public const string TargetFramework = "TargetFramework";
        public const string VisualBasic = "VisualBasic";
        private const string csProjectGuid = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
        private const string dbProjectGuid = "{C8D11400-126E-41CD-887F-60BD40844F9E}";
        private const string fsProjectGuid = "{F2A71F9B-5D33-465A-A702-920D77279786}";
        private const string solutionFolderGuid = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";

        private const string vbProjectGuid = "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}";
        private const string vcProjectGuid = "{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}";

        public string Extension
        {
            get
            {
                switch (Language)
                {
                    case CSharp:
                        return ".csproj";
                    case VisualBasic:
                        return ".vbproj";
                    default:
                        return ".fsproj";
                }
            }
        }

        public string FileName => $"{Name}{Extension}";

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Language { get; set; }
        public string Name { get; set; }

        public Guid ProjectGuid
        {
            get
            {
                switch (Language)
                {
                    case CSharp:
                        return Guid.Parse(csProjectGuid);
                    case VisualBasic:
                        return Guid.Parse(vbProjectGuid);
                    default:
                        return Guid.Parse(fsProjectGuid);
                }
            }
        }

        public List<TestProject> References { get; set; } = new List<TestProject>();
        public string RelativePath => $@"{Name}\{FileName}";
        public FileStatus Status { get; set; } = FileStatus.Valid;

        public string GetFileContent()
        {
            var rootPropertyGroup = new XElement(
                PropertyGroup,
                new XElement(
                    TargetFramework,
                    new XText(NetStandard2)));

            var projectReferences = new XElement(ItemGroup);
            if (References.Any())
            {
                foreach (var testProject in References)
                {
                    var projectReference = new XElement(ProjectReference, new XAttribute(Include, $@"..\{testProject.RelativePath}"));
                    projectReferences.Add(projectReference);
                }
            }

            var document = new XDocument(
                new XElement(
                    Project,
                    new XAttribute(Sdk, MicrosoftNetSdk),
                    rootPropertyGroup,
                    projectReferences));

            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            byte[] buffer;
            long length = 0;
            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    document.WriteTo(writer);
                }

                length = stream.Length;
                buffer = stream.GetBuffer();
            }

            using (var stream = new MemoryStream(buffer.Take((int)length).ToArray()))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
