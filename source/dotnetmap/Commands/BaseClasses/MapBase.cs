namespace dotnetmap.Commands
{
    using System.CommandLine;
    using System.IO;

    public abstract class MapBase : Command
    {
        protected MapBase(string name, string description) : base(name, description)
        {
            MapFileArgument = new Option<FileInfo>(new[] {"--map", "-m"}, DescriptionMapFile)
            {
                IsRequired = true,
            };
        }

        protected virtual string DescriptionMapFile { get; private set; } = "The path to the map file to create.";
        protected Option<FileInfo> MapFileArgument { get; set; }
    }
}
