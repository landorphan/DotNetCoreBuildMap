namespace Landorphan.BuildMap.Construction.SolutionModel
{
    using System;
    using Landorphan.BuildMap.Abstractions.FileSystem;
    using Landorphan.BuildMap.Model;
    using Landorphan.Common;

    public class SuppliedFile
    {
        public SuppliedFile()
        {
        }

        public SuppliedFile(SuppliedFile original)
        {
            original.ArgumentNotNull(nameof(original));
            Id = original.Id;
            RawText = original.RawText;
            Paths = original.Paths;
            Directory = original.Directory;
            Status = original.Status;
        }

        public string Directory { get; set; }

        public Guid Id { get; set; }

        public FilePaths Paths { get; set; }
        public string RawText { get; set; }

        public FileStatus Status { get; set; }
    }
}
