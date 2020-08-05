﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectOrder.Model
{
   public  class VisualStudioSolutionFile
   {
      public string FilePath { get; set; }

      public string Directory => Path.GetDirectoryName(this.FilePath);

      public string Name => Path.GetFileName(this.FilePath);

      // This is held as a list of strings because it makes the file easier to parse
      // given the nature of SLN files. 
      public List<string> RawFileText { get; set; } = new List<string>();

      public List<ProjectFileReference> ProjectFiles { get; set; } = new List<ProjectFileReference>();
   }
}
