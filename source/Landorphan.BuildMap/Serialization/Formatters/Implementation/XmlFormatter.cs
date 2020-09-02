namespace Landorphan.BuildMap.Serialization.Formatters.Implementation
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Landorphan.BuildMap.Model;
    using Landorphan.BuildMap.Model.Support;
    using Landorphan.BuildMap.Serialization.Formatters.Interfaces;
    using Landorphan.Common;

    public class XmlFormatter : IFormatter
    {
        private readonly XmlSerializer serializer = new XmlSerializer(
            typeof(Map),
            new[] {typeof(Project), typeof(VersionString), typeof(Build)});

        public Map Read(string text)
        {
            Map result;
            var bytes = Encoding.UTF8.GetBytes(text);
            using (var stream = new MemoryStream(bytes))
            using (var reader = new XmlTextReader(stream))
            {
                result = (Map)serializer.Deserialize(reader);
            }

            return result;
        }

        public bool SniffValidFormat(string text)
        {
            text.ArgumentNotNull(nameof(text));
            return text.StartsWith("<", StringComparison.OrdinalIgnoreCase);
        }

        public string Write(Map map)
        {
            string result;
            using (var stream = new MemoryStream())
            using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, map);
                result = Encoding.UTF8.GetString(stream.ToArray());
            }

            return result;
        }
    }
}
