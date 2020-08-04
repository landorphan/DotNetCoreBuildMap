using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Landorphan.BuildMap.Model;
using Landorphan.BuildMap.Model.Support;
using Landorphan.BuildMap.Serialization.Formatters.Interfaces;
using Newtonsoft.Json;

namespace Landorphan.BuildMap.Serialization.Formatters.Implementation
{
    using Landorphan.Common;
    public class MapFormatter : IFormatter
    {
        public void WriteHeader(Map map, StringBuilder builder)
        {
            map.ArgumentNotNull(nameof(map));
            builder.ArgumentNotNull(nameof(builder));

            builder.AppendLine($"##### BUILDMAP {map.SchemaVersion} #####");
            builder.AppendLine($"# {nameof(Map.SchemaVersion)} = {map.SchemaVersion} #");
            builder.AppendLine($"# {nameof(Map.ToolVersion)} = {map.ToolVersion} #");
            builder.AppendLine($"##########");
        }

        public void WriteBuildInfo(Build build, StringBuilder builder)
        {
            build.ArgumentNotNull(nameof(build));
            builder.ArgumentNotNull(nameof(builder));

            builder.AppendLine($"+ {nameof(Build.BuildVersion)} = {build.BuildVersion} +");
            builder.AppendLine($"+ {nameof(Build.RelativeRoot)} = {build.RelativeRoot} +");
        }

        public List<KeyValuePair<int, PropertyInfo>> GetProjectProperties()
        {
            var projectType = typeof(Project);
            var properties = (from p in projectType.GetProperties()
                let a = p.GetCustomAttribute<JsonPropertyAttribute>()
                orderby a.Order
                select new KeyValuePair<int,PropertyInfo>(a.Order, p));
            return properties.ToList();
        }

        public void WriteList<T>(IEnumerable<T> list, StringBuilder builder)
        {
            list.ArgumentNotNull(nameof(list));
            builder.ArgumentNotNull(nameof(builder));
            int count = 0;
            foreach (var item in list)
            {
                if (count++ != 0)
                {
                    builder.Append(";");
                }
                builder.Append(item);
            }
        }

        public StringList GetStringList(string items)
        {
            items.ArgumentNotNull(nameof(items));
            StringList retval = new StringList(items.Split(";"));
            return retval;
        }

        public GuidList GetGuidList(string items)
        {
            var stringList = GetStringList(items);
            var guids =
                (from s in stringList
                where !string.IsNullOrWhiteSpace(s)      
               select new Guid(s)).ToArray();
            return new GuidList(guids);
        }
        
        void WriteProjectProperty(Project project, PropertyInfo property, StringBuilder builder)
        {
            if (typeof(StringList).IsAssignableFrom(property.PropertyType))
            {
                StringList list = (StringList) property.GetValue(project);
                WriteList(list, builder);
            }
            else if (typeof(GuidList).IsAssignableFrom(property.PropertyType))
            {
                GuidList list = (GuidList) property.GetValue(project);
                WriteList(list, builder);
            }
            else
            {
                builder.Append(property.GetValue(project));
            }
        }

        public void WriteProjectHeaders(StringBuilder builder)
        {
            builder.ArgumentNotNull(nameof(builder));
            var properties = GetProjectProperties();
            foreach (var property in properties)
            {
                builder.Append($":{property.Key}>");
                builder.Append(property.Value.Name);
                builder.Append($"<{property.Key}");
            }

            builder.AppendLine(":");
        }

        public void WriteProjectInfo(Project project, StringBuilder builder)
        {
            builder.ArgumentNotNull(nameof(builder));

            var properties = GetProjectProperties();
            foreach (var property in properties)
            {
                builder.Append($"|{property.Key}>");
                WriteProjectProperty(project, property.Value, builder);
                builder.Append($"<{property.Key}");
            }

            builder.AppendLine("|");
        }

        public void WriteProjectInfo(Build build, StringBuilder builder)
        {
            build.ArgumentNotNull(nameof(build));

            WriteProjectHeaders(builder);
            foreach (var project in build.Projects)
            {
                WriteProjectInfo(project, builder);
            }
        }

        public string Write(Map map)
        {
            map.ArgumentNotNull(nameof(map));

            StringBuilder builder = new StringBuilder();

            WriteHeader(map, builder);
            WriteBuildInfo(map.Build, builder);
            WriteProjectInfo(map.Build, builder);

            return builder.ToString();
        }

        private readonly Regex headerParse = new Regex($@"#\s+(?<{nameof(PropertyInfo.Name)}>[^ =]+)\s*=\s*(?<{nameof(PropertyInfo.SetValue)}>[^#]+)", 
            RegexOptions.Compiled);
        public void SetFromHeaderLines(string[] lines, Map map)
        {
            var propertyEntries =
                 (from p in typeof(Map).GetProperties()
                select new KeyValuePair<string,PropertyInfo>(p.Name, p));
            var properties = new Dictionary<string, PropertyInfo>(propertyEntries);
            var workingSet = 
                (from l in lines 
                where l.StartsWith("#", StringComparison.OrdinalIgnoreCase) &&
                     !l.StartsWith("##", StringComparison.OrdinalIgnoreCase)
               select l);
            foreach (var line in workingSet)
            {
                var match = headerParse.Match(line);
                if (match.Success)
                {
                    var property = properties[match.Groups[nameof(PropertyInfo.Name)].Value];
                    SetPropertyValue(map, property, match.Groups[nameof(PropertyInfo.SetValue)].Value.TrimEnd());
                }
            }
        }

        private readonly Regex buildParse = new Regex($@"\+\s+(?<{nameof(PropertyInfo.Name)}>[^ =]+)\s*=\s*(?<{nameof(PropertyInfo.SetValue)}>[^+]+)");
        public void SetFromBuildLines(string[] lines, Map map)
        {
            map.ArgumentNotNull(nameof(map));

            var propertyEntries =
                (from p in typeof(Build).GetProperties()
                 select new KeyValuePair<string, PropertyInfo>(p.Name, p));
            var properties = new Dictionary<string, PropertyInfo>(propertyEntries);
            var workingSet =
                (from l in lines
                 where l.StartsWith("+", StringComparison.OrdinalIgnoreCase)
                 select l);
            foreach (var line in workingSet)
            {
                var match = buildParse.Match(line);
                if (match.Success)
                {
                    var property = properties[match.Groups[nameof(PropertyInfo.Name)].Value];
                    SetPropertyValue(map.Build, property, match.Groups[nameof(PropertyInfo.SetValue)].Value.TrimEnd());
                }
            }
        }

        private const string ColumnId = "Id";
        private const string ColumnValue = "Value";
        private readonly Regex columnParse = new Regex($@"(?<{ColumnId}>\d+)>(?<{ColumnValue}>.*)<\d+");
        public void SetFromProjectLines(string[] lines, Map map)
        {
            map.ArgumentNotNull(nameof(map));

            var properties = GetProjectProperties();
            var workingSet =
                (from l in lines
                 where l.StartsWith("|", StringComparison.OrdinalIgnoreCase)
                 select l);
            foreach (var line in workingSet)
            {
                Project project = new Project();
                var columns =
                    (from c in line.Split("|")
                     where !string.IsNullOrWhiteSpace(c)
                     select c);
                foreach (var column in columns)
                {
                    var match = columnParse.Match(column);
                    if (match.Success)
                    {
                        var property = properties[int.Parse(match.Groups[ColumnId].Value,
                            CultureInfo.InvariantCulture)];
                        var rawValue = match.Groups[ColumnValue].Value;
                        SetPropertyValue(project, property.Value, rawValue);
                    }
                }
                map.Build.Projects.Add(project);
            }
        }

        public void SetPropertyValue(object obj, PropertyInfo property, string rawValue)
        {
            property.ArgumentNotNull(nameof(property));

            if (property.PropertyType == typeof(int))
            {
                property.SetValue(obj, int.Parse(rawValue, CultureInfo.InvariantCulture));
            }
            else if (property.PropertyType == typeof(VersionString))
            {
                property.SetValue(obj, new VersionString(rawValue));
            }
            else if (property.PropertyType == typeof(Guid))
            {
                property.SetValue(obj, new Guid(rawValue));
            }
            else if (property.PropertyType == typeof(Language))
            {
                property.SetValue(obj, Enum.Parse<Language>(rawValue));
            }
            else if (property.PropertyType == typeof(StringList))
            {
                property.SetValue(obj, GetStringList(rawValue));
            }
            else if (property.PropertyType == typeof(GuidList))
            {
                property.SetValue(obj, GetGuidList(rawValue));
            }
            else
            {
                property.SetValue(obj, rawValue);
            }
        }

        public bool SniffValidFormat(string text)
        {
            text.ArgumentNotNull(nameof(text));
            return text.StartsWith("##### BUILDMAP", StringComparison.Ordinal);
        }

        public Map Read(string text)
        {
            text.ArgumentNotNull(nameof(text));

            var lines = text.Replace("\r", "",
                StringComparison.OrdinalIgnoreCase).Split("\n");
            Map map = new Map();
            SetFromHeaderLines(lines, map);
            SetFromBuildLines(lines, map);
            SetFromProjectLines(lines, map);

            return map;
        }
    }
}