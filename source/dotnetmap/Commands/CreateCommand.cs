using System.Collections.Generic;
using System.CommandLine;
using System.IO;

namespace dotnetmap.Commands
{
    public class CreateCommand : MapBase
    {
        protected Option<IEnumerable<FileInfo>> InputFiles;
        
        public CreateCommand() : base("create", "Creates a build map of a set of dotnet projects.")
        {
            InputFiles = new Option<IEnumerable<FileInfo>>(new [] { "--input", "-i" }, 
                "Project files or GLOB search patterns to use as input to create the build map.");
            InputFiles.IsRequired = true;
            this.AddOption(InputFiles);
        }
    }
}