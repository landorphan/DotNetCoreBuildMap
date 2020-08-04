using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Landorphan.BuildMap.Model;
using Landorphan.BuildMap.Model.Support;
using Landorphan.BuildMap.Serialization.Attributes;
using Landorphan.BuildMap.Serialization.Formatters.Helpers;
using Landorphan.BuildMap.Serialization.Formatters.Interfaces;
using Newtonsoft.Json;

namespace Landorphan.BuildMap.Serialization.Formatters.Implementation
{
    using Landorphan.Common;
    public class TableFormatter : IFormatWriter
    {
        public IEnumerable<string> Items { get; private set; }

        public static IReadOnlyList<string> DefaultItems =>
            (from p in typeof(Project).GetProperties()
              let o = p.GetCustomAttribute<JsonPropertyAttribute>()
              let d = p.GetCustomAttribute<TableDefaultDisplayAttribute>()
            where o != null
               && d != null
          orderby o.Order
           select p.Name).ToArray();

        public static IReadOnlyList<string> AllItems => (
             from p in typeof(Project).GetProperties() 
              let o = p.GetCustomAttribute<JsonPropertyAttribute>()
            where o != null
          orderby o.Order
           select p.Name).ToArray(); 

        public TableFormatter(IEnumerable<string> itemHints)
        {
            this.Items = DefaultItems;
            if (itemHints != null &&
                itemHints.Any())
            {
                this.Items = itemHints;
            }
        }

        public IDictionary<string, ColumnInfo> ComputeColumns(Map map)
        {
            map.ArgumentNotNull(nameof(map));

            Build build = map.Build;

            var projectProperties = new Dictionary<string, ColumnInfo>(
                (from p in typeof(Project).GetProperties()
                 let o = p.GetCustomAttribute<JsonPropertyAttribute>()
                 let m = p.Name.Length < 8 ? 8 : p.Name.Length
                 where Items.Contains(p.Name)
                 select new KeyValuePair<string, ColumnInfo>(p.Name, new ColumnInfo
                 {
                     Property = p,
                     Order = o?.Order,
                     MinWidth = m,
                     MaxWidth = m,
                 })));

            var columnInfos = new Dictionary<string, ColumnInfo>(projectProperties);

            int item = 0;
            foreach (var project in build.Projects)
            {
                foreach (var column in columnInfos)
                {
                    column.Value.RowValues[item] = new List<string>();
                    object obj = column.Value.Property.GetValue(project);
                    SetColumnValueForItemRow(column.Value, item, obj);
                }

                item++;
            }

            return columnInfos;
        }

        public void SetColumnValueForMultiRow(ColumnInfo column, int item, object obj)
        {
            column.ArgumentNotNull(nameof(column));

            if (typeof(StringList).IsAssignableFrom(column.Property.PropertyType))
            {
                List<string> asStringList = new List<string>((StringList)obj);
                column.RowValues[item].AddRange(asStringList);
                asStringList = asStringList.ToList();
                asStringList.Add(column.Property.Name);

                int maxLength =
                    (from i in asStringList
                     select i.Length).Max();

                UpdateColumnWidth(column, maxLength);
            }
            else if (typeof(GuidList).IsAssignableFrom(column.Property.PropertyType))
            {
                GuidList asGuidList = (GuidList)obj;
                List<string> asStringList = new List<string>(
                    (from g in asGuidList
                     select g.ToString().Substring(0, 8)));
                column.RowValues[item].AddRange(asStringList);
                asStringList = asStringList.ToList();
                asStringList.Add(column.Property.Name);

                int maxLength =
                    (from i in asStringList
                     select i.Length).Max();

                UpdateColumnWidth(column, maxLength);
            }
        }

        public void SetColumnValueForItemRow(ColumnInfo column, int item, object obj)
        {
            column.ArgumentNotNull(nameof(column));
            obj.ArgumentNotNull(nameof(obj));

            if (typeof(VersionString).IsAssignableFrom(column.Property.PropertyType))
            {
                VersionString asVersionString = (VersionString)obj;
                string str = GetVersionStringText(column, asVersionString);
                column.RowValues[item].Add(str);
                UpdateColumnWidth(column, str.Length);
            }
            else if (typeof(Guid) == column.Property.PropertyType)
            {
                Guid guid = (Guid)obj;
                string str = GetGuidText(column, guid);
                column.RowValues[item].Add(str);
                UpdateColumnWidth(column, str.Length);
            }
            else if (typeof(int) == column.Property.PropertyType)
            {
                int integer = (int)obj;
                string str = integer.ToString(CultureInfo.InvariantCulture);
                column.RowValues[item].Add(str);
                UpdateColumnWidth(column, str.Length);
            }
            else if (typeof(StringList).IsAssignableFrom(column.Property.PropertyType) ||
                     typeof(GuidList).IsAssignableFrom(column.Property.PropertyType))
            {
                SetColumnValueForMultiRow(column, item, obj);
            }
            else if (column.Property.Name == nameof(Project.Name))
            {
                string str = ConvertPathToLinuxStyle((string)obj, false);
                column.RowValues[item].Add(str);
                UpdateColumnWidth(column, str.Length);
            }
            else
            {
                string str = ConvertPathToLinuxStyle(obj.ToString(), true);
                column.RowValues[item].Add(str);
                UpdateColumnWidth(column, str.Length);
            }
        }

        public string ConvertPathToLinuxStyle(string path, bool keepExtension)
        {
            Stack<string> pathStack = new Stack<string>();
            if (keepExtension)
            {
                pathStack.Push(Path.GetFileNameWithoutExtension(path));
            }
            else
            {
                pathStack.Push(Path.GetFileName(path));
            }

            path = Path.GetDirectoryName(path);
            while (!string.IsNullOrWhiteSpace(path))
            {
                pathStack.Push(Path.GetFileName(path));
                path = Path.GetDirectoryName(path);
            }

            return string.Join("/", pathStack.ToArray());
        }

        public void UpdateColumnWidth(ColumnInfo column, int length)
        {
            column.ArgumentNotNull(nameof(column));

            if (length > column.MaxWidth)
            {
                column.MaxWidth = length;
            }
            if (length > column.MinWidth)
            {
                column.MinWidth = length;
            }
        }

        public string GetGuidText(ColumnInfo column, Guid value)
        {
            var str = value.ToString().Substring(0, 8);

            var length = str.Length;
            UpdateColumnWidth(column, length);
            return str;
        }

        public string GetVersionStringText(ColumnInfo column, VersionString versionString)
        {
            Version asVersion = versionString;
            return asVersion.ToString();
        }

        public const int ColumnSpacingPadding = 2;

        public void WriteFooter(Map map, IDictionary<string, ColumnInfo> columns, StringBuilder builder)
        {
            columns.ArgumentNotNull(nameof(columns));
            builder.ArgumentNotNull(nameof(builder));

            builder.Append("=");
            foreach (var column in columns)
            {
                builder.Append(string.Empty.PadRight(column.Value.MaxWidth + ColumnSpacingPadding, '='));
                builder.Append("=");
            }

            builder.AppendLine();
        }

        public void WriteHeaders(Map map, IDictionary<string, ColumnInfo> columns, StringBuilder builder)
        {
            columns.ArgumentNotNull(nameof(columns));
            builder.ArgumentNotNull(nameof(builder));

            builder.Append("=");
            foreach (var column in columns)
            {
                builder.Append(string.Empty.PadRight(column.Value.MaxWidth + ColumnSpacingPadding, '='));
                builder.Append("=");
            }
            builder.AppendLine();
            builder.Append("|");
            foreach (var column in columns)
            {
                builder.Append(' ');
                var padded = column.Key.PadRight(column.Value.MaxWidth + 1, ' ');
                builder.Append(padded);
                builder.Append("|");
            }
            builder.AppendLine();
            builder.Append("=");
            foreach (var column in columns)
            {
                builder.Append(string.Empty.PadRight(column.Value.MaxWidth + ColumnSpacingPadding, '='));
                builder.Append("=");
            }
            builder.AppendLine();
        }

        public void WriteData(Map map, IDictionary<string, ColumnInfo> columns, StringBuilder builder)
        {
            map.ArgumentNotNull(nameof(map));
            columns.ArgumentNotNull(nameof(columns));
            builder.ArgumentNotNull(nameof(builder));

            int item = 0;
            foreach (var project in map.Build.Projects)
            {
                int maxRows =
                    (from c in columns
                     select c.Value.RowValues[item].Count).Max();
                for (int row = 0; row < maxRows; row++)
                {
                    builder.Append("|");
                    foreach (var column in columns)
                    {
                        if (row < column.Value.RowValues[item].Count)
                        {
                            builder.Append(' ');
                            var str = column.Value.RowValues[item].ToArray()[row];
                            builder.Append(str.PadRight(column.Value.MaxWidth, ' '));
                            builder.Append(' ');
                            builder.Append("|");
                        }
                        else
                        {
                            builder.Append(' ');
                            builder.Append(string.Empty.PadRight(column.Value.MaxWidth, ' '));
                            builder.Append(' ');
                            builder.Append("|");
                        }
                    }
                    builder.AppendLine();
                }

                item++;
            }
        }
        
        public string Write(Map map)
        {
            StringBuilder builder = new StringBuilder();
            var columns = ComputeColumns(map);
            WriteHeaders(map, columns, builder);
            WriteData(map, columns, builder);
            WriteFooter(map, columns, builder);
            return builder.ToString();
        }
    }
}