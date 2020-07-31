using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;

namespace dotnetmap.Commands
{
    public class FilterPair 
    {
        public FilterPair(string item, string pattern)
        {
            this.Item = item;
            this.Pattern = pattern;
        }

        public FilterPair(string filterPair)
        {
            var items = filterPair.Split("=");
            if (items.Length > 1)
            {
                this.Item = items[0];
                // Split if efferent and quick but over aggressive.  If an equal sign (=) is in the search pattern,
                // split will split the string based on that.  This reassembles the remaining splits back into one 
                // pattern.
                this.Pattern = string.Join("=", items.Skip(1));
            }
            else
            {
                throw new InvalidOperationException($"Unable to parse the supplied filter ({filterPair})");
            }
        }
        
        public string Item { get; set; }
        public string Pattern { get; set; }
    }
    
    
    public class SelectCommand : DisplayCommand
    {
        private Option<FilterPair[]> FilterPair;
        
        public SelectCommand() : base("select", 
            "Search the map for given value combinations.")
        {
            FilterPair = new Option<FilterPair[]>(new[] { "--filter", "-f" }, 
                "A set of filters to apply to the map before displaying.");
        }
    }
}