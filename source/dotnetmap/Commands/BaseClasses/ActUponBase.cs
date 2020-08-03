using System.CommandLine;

namespace dotnetmap.Commands
{
    public class ActUponBase : MapBase
    {
        public ActUponBase(string name, string description = null) : base(name, description)
        {
            this.AddOption(MapFileArgument.ExistingOnly());
        }
    }
}