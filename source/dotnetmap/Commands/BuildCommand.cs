namespace dotnetmap.Commands
{
    public class BuildCommand : BuildBase
    {
        public BuildCommand() : base("build", 
            "Walks the project map and performs a build on each project in order. ")
        {
        }
    }
}