namespace dotnetmap.Commands
{
    using System.Collections.Generic;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.IO;

    public class CreateCommand : MapBase
    {
        public CreateCommand() : base("create", "Creates a build map of a set of dotnet projects.")
        {
            AddOption(MapFileArgument);

            WorkingDirectory = new Option<string>(
                new[] {"--working-directory", "-wd"},
                "The directory to traverse for locating the files to include int he map.");
            AddOption(WorkingDirectory);

            InputFiles = new Option<IEnumerable<string>>(
                new[] {"--input", "-i"},
                "Project files or GLOB search patterns to use as input to create the build map.");
            InputFiles.IsRequired = true;
            AddOption(InputFiles);

            Handler = CommandHandler.Create<FileInfo, IEnumerable<string>, string>(CreateMap);
        }

        protected Option<IEnumerable<string>> InputFiles { get; set; }

        protected Option<string> WorkingDirectory { get; set; }

        public void CreateMap(FileInfo map, IEnumerable<string> input, string workingDirectory)
        {
            //MapManagement mapManager = new MapManagement();
            //var files = mapManager.LocateFiles(workingDirectory, input);
            //foreach (var file in files)
            //{
            //    Console.WriteLine(file);
            //}
        }
    }
}
