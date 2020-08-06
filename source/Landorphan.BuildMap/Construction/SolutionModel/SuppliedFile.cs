using System;
using System.Collections.Generic;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
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
            this.Id = original.Id;
            this.RawText = original.RawText;
            this.Paths = original.Paths;
            this.Directory = original.Directory;
            this.Status = original.Status;
        }

        public Guid Id { get; set; }
        public string RawText { get; set; }

        public FilePaths Paths {  get; set; }
        public string Directory { get; set; }

        public FileStatus Status { get; set; }
    }
}