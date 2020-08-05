using System;
using System.Collections.Generic;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
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
            this.Path = original.Path;
            this.Directory = original.Directory;
            this.AbsolutePath = original.AbsolutePath;
            this.Status = original.Status;
        }

        public Guid Id { get; set; }
        public string RawText { get; set; }
        public string Path { get; set; }
        public string AbsolutePath { get; set; }
        public string Directory { get; set; }

        public FileStatus Status { get; set; }
    }
}