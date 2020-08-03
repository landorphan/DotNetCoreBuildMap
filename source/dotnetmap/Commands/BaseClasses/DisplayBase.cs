using System.CommandLine;
using System.ComponentModel;
using System.IO;
using Landorphan.BuildMap.Serialization;

namespace dotnetmap.Commands
{
    public abstract class DisplayBase : ActUponBase
    {
        protected Option<Format> OutputFormat;
        protected Option<string[]> ItemsToDisplay;
        protected Option<FileInfo> OutputFile; 

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
            
            OutputFile = new Option<FileInfo>(new[] { "--output", "-o", },
                "The output location (if not specified output will go to standard out.");
            this.AddOption(OutputFile);
        }
    }
}