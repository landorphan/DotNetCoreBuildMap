using System.CommandLine;
using System.ComponentModel;

namespace dotnetmap.Commands
{
    public enum Format
    {
        [Description("Internal Map format designed to be easy to manipulate with command line tools " +
                     "such as grep, sed, awk.")]
        Map,
        [Description("A Json serialized form that provides easier programmatic manipulation.")]
        Json,
        [Description("A Yaml serialized form that is easier to manually edit.")]
        Yaml,
        [Description("A text based table display that is easier to quickly read.")]
        Table,
        [Description("Similar to the Table format, but with no headers.  This is useful to pass values from " +
                     "one command into another, such as if you only need the paths to the projects for another" +
                     "command line task.")]
        Text
    }
    
    public abstract class DisplayCommand : MapCommand
    {
        protected Option<Format> OutputFormat;
        protected Option<string[]> ItemsToDisplay;

        protected override string DescriptionMapFile => "The path to the map file to display.";

        protected DisplayCommand(string name, string description = null) : base(name, description)
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