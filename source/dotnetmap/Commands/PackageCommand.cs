namespace dotnetmap.Commands
{
    public class PackageCommand : BuildBase
    {
        public PackageCommand() : base(
            "pack",
            "Walks the project map and performs a NuGet package command on each project in the map " +
            "(that is identified as a NuGet project) in order.")
        {
        }
    }
}
