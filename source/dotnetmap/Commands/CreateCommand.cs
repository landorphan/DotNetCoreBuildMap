using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using Landorphan.BuildMap.Construction;

namespace dotnetmap.Commands
{
    public class CreateCommand : MapBase
    {
        protected Option<IEnumerable<string>> InputFiles;

        protected Option<string> WorkingDirectory;
        
        public CreateCommand() : base("create", "Creates a build map of a set of dotnet projects.")
        {
            this.AddOption(MapFileArgument);
            
            WorkingDirectory = new Option<string>(new [] { "--working-directory", "-wd" },
                "The directory to traverse for locating the files to include int he map.");
            this.AddOption(WorkingDirectory);
            
            InputFiles = new Option<IEnumerable<string>>(new [] { "--input", "-i" }, 
                "Project files or GLOB search patterns to use as input to create the build map.");
            InputFiles.IsRequired = true;
            this.AddOption(InputFiles);
            
            Handler = CommandHandler.Create<FileInfo, IEnumerable<string>, string>(CreateMap);
        }

        public void CreateMap(FileInfo map, IEnumerable<string> input, string workingDirectory)
        {
            MapManagement mapManager = new MapManagement();
            var files = mapManager.LocateFiles(workingDirectory, input);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }
    }
}