using System.CommandLine;

namespace dotnetmap.Commands
{
    public abstract class BuildBase : Command
    {
        protected BuildBase(string name, string description) : base(name, description)
        {
        }
    }
}