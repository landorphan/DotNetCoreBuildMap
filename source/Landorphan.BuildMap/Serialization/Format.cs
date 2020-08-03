using System.ComponentModel;

namespace Landorphan.BuildMap.Serialization
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
        [Description("A Xml serialized form supported mostly for completeness of format options.")]
        Xml,
        [Description("A text based table display that is easier to quickly read.")]
        Table,
        [Description("Similar to the Table format, but with no headers.  This is useful to pass values from " +
                     "one command into another, such as if you only need the paths to the projects for another" +
                     "command line task.")]
        Text
    }
}