namespace ProjectOrder.Model
{
    using System.Collections.Generic;
    using System.IO;

    public class VisualStudioSolutionFile
    {
        public string Directory => Path.GetDirectoryName(FilePath);
        public string FilePath { get; set; }

        public string Name => Path.GetFileName(FilePath);

        public List<ProjectFileReference> ProjectFiles { get; set; } = new List<ProjectFileReference>();

        // This is held as a list of strings because it makes the file easier to parse
        // given the nature of SLN files. 
        public List<string> RawFileText { get; set; } = new List<string>();
    }
}
