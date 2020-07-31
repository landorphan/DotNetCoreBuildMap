using System.CommandLine;

namespace dotnetmap.Commands
{
    public class DotNetMapCommand : RootCommand
    {
        public const string DotNetMapDescription =
            "Used to create and manage a map of multiple dotnet projects (or solutions), for the purposes of \n" +
            "building, testing, and managing those projects.  The map will organize all projects by dependency so \n" +
            "so that they can be built in order as a developer or build server build.  This allows complex project \n" +
            "structures to be built and managed.";

        public DotNetMapCommand() : base(DotNetMapDescription)
        {
            AddCommand(new CreateCommand());
            AddCommand(new ListCommand());
            AddCommand(new SelectCommand());
        }
    }
}