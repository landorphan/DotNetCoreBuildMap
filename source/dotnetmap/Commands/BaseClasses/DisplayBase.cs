using System.CommandLine;
using System.ComponentModel;
using Landorphan.BuildMap.Serialization;

namespace dotnetmap.Commands
{
    public abstract class DisplayBase : MapBase
    {
        protected Option<Format> OutputFormat;
        protected Option<string[]> ItemsToDisplay;

        protected override string DescriptionMapFile => "The path to the map file to display.";

        protected DisplayBase(string name, string description = null) : base(name, description)
        {
            OutputFormat = new Option<Format>("--format",
                "The format to use when outputting the map. " +
                "NOTE: The Table and Text format can not be used as an input to further dotnetmap invocations.");
            OutputFormat.Argument.SetDefaultValue("Table");
            this.AddOption(OutputFormat);
            
            ItemsToDisplay = new Option<string[]>(new[] { "--items", "-i" }, 
                "The items to display in the output.  This is only utilized for the Text or Table formats.");
            ItemsToDisplay.Argument.SetDefaultValue("All");
            this.AddOption(ItemsToDisplay);
        }
    }
}