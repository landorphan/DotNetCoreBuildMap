using System;
using System.Collections.Generic;
using Landorphan.BuildMap.Model.Support;

namespace Landorphan.BuildMap.Construction.SolutionModel
{
    public class SolutionFile : SuppliedFile
    {
        public SolutionFile()
        {
        }

        public SolutionFile(SuppliedFile original) : base(original)
        {
        }
        
        public List<ProjectFile> Projects { get; set; } = new List<ProjectFile>(); 
    }
}