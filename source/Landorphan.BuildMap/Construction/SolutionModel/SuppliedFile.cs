using System;
using System.Collections.Generic;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
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
        }

        public Guid Id { get; set; }
        public string RawText { get; set; }
        public List<string> Paths { get; set; } = new List<string>();
    }
}