namespace Landorphan.BuildMap.Serialization.Formatters.Helpers
{
    using System.Collections.Generic;
    using System.Reflection;

    public class ColumnInfo
    {
        public int MaxWidth { get; set; }
        public int MinWidth { get; set; }
        public int? Order { get; set; }
        public PropertyInfo Property { get; set; }
        public Dictionary<int, List<string>> RowValues { get; set; } = new Dictionary<int, List<string>>();
    }
}
