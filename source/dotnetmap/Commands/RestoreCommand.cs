namespace dotnetmap.Commands
{
    public class RestoreCommand : BuildBase
    {
        public RestoreCommand() : base("restore", 
            "Walks the project map and performs NuGet package restores on each project in order.")
        {
        }
    }
}