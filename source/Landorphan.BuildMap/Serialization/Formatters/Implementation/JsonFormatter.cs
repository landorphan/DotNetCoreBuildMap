namespace Landorphan.BuildMap.Serialization.Formatters.Implementation
{
    using System;
    using Landorphan.BuildMap.Model;
    using Landorphan.BuildMap.Serialization.Converters;
    using Landorphan.BuildMap.Serialization.Formatters.Interfaces;
    using Landorphan.Common;
    using Newtonsoft.Json;

    public class JsonFormatter : IFormatter
    {
        public Map Read(string text)
        {
            return JsonConvert.DeserializeObject<Map>(text);
        }

        public bool SniffValidFormat(string text)
        {
            text.ArgumentNotNull(nameof(text));
            return text.StartsWith("{", StringComparison.OrdinalIgnoreCase);
        }

        public string Write(Map map)
        {
            var settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.Converters.Add(new VersionStringJsonConverter());
            return JsonConvert.SerializeObject(map, settings);
        }
    }
}
