namespace dotnetmap.Commands
{
    using System.CommandLine;

    public abstract class BuildBase : Command
    {
        protected BuildBase(string name, string description) : base(name, description)
        {
        }
    }
}
