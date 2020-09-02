namespace dotnetmap.Commands
{
    using System;
    using System.CommandLine;
    using System.Linq;
    using Landorphan.Common;

    public class FilterPair
    {
        public FilterPair(string item, string pattern)
        {
            Item = item;
            Pattern = pattern;
        }

        public FilterPair(string filterPair)
        {
            filterPair.ArgumentNotNull(nameof(filterPair));
            var items = filterPair.Split("=");
            if (items.Length > 1)
            {
                Item = items[0];
                // Split if efferent and quick but over aggressive.  If an equal sign (=) is in the search pattern,
                // split will split the string based on that.  This reassembles the remaining splits back into one 
                // pattern.
                Pattern = string.Join("=", items.Skip(1));
            }
            else
            {
                throw new InvalidOperationException($"Unable to parse the supplied filter ({filterPair})");
            }
        }

        public string Item { get; set; }
        public string Pattern { get; set; }
    }

    public class SelectCommand : DisplayBase
    {
        private readonly Option<FilterPair[]> FilterPair;

        public SelectCommand() : base(
            "select",
            "Search the map for given value combinations.")
        {
            FilterPair = new Option<FilterPair[]>(
                new[] {"--filter", "-f"},
                "A set of filters to apply to the map before displaying.  The syntax fo a filter is: " +
                "Name=Pattern where 'Pattern' is a GLOB pattern to mach a property of the object map.  When " +
                "a map property supports multiple values (such as project type) then a mach will succeed if " +
                "any of the values mach the pattern.");
            FilterPair.IsRequired = true;
            AddOption(FilterPair);
        }
    }
}
