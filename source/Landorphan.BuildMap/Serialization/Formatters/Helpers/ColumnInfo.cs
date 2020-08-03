using System.Collections.Generic;
using System.Reflection;

namespace Landorphan.BuildMap.Serialization.Formatters.Helpers
{
    public class ColumnInfo
    {
        public PropertyInfo Property { get; set; }
        public int? Order { get; set; }
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }
        public Dictionary<int, List<string>> RowValues { get; set; } = new Dictionary<int, List<string>>();
    }
}