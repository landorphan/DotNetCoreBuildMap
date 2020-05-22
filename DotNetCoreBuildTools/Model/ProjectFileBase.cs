namespace ProjectOrder.Model
{
    using System;
    using System.IO;
    using ProjectOrder.Attributes;

    public class ProjectFileBase
    {
        public ProjectFileBase()
        {
        }

        public ProjectFileBase(string filePath)
        {
            FilePath = filePath;
            Name = Path.GetFileName(filePath);
        }

        public ProjectFileBase(ProjectFileBase original)
        {
            Id = original.Id;
            Name = original.Name;
            FilePath = original.FilePath;
        }

        public string Directory => Path.GetDirectoryName(FilePath);

        [DisplayInMap]
        public string FilePath { get; set; }

        [DisplayInMap]
        public string Id { get; set; } = Guid.NewGuid().ToString().ToUpperInvariant();

        [DisplayInMap]
        public string Name { get; set; }
    }
}
