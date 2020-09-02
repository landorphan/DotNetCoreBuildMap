namespace dotnetmap.Commands
{
    using System.CommandLine;

    public class ActUponBase : MapBase
    {
        public ActUponBase(string name, string description) : base(name, description)
        {
            AddOption(MapFileArgument.ExistingOnly());
        }
    }
}
