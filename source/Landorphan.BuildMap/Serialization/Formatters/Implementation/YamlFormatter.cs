namespace Landorphan.BuildMap.Serialization.Formatters.Implementation
{
    using Landorphan.BuildMap.Model;
    using Landorphan.BuildMap.Serialization.Formatters.Interfaces;
    using YamlDotNet.Serialization;

    public class YamlFormatter : IFormatter
    {
        public Map Read(string text)
        {
            var deserializer = new Deserializer();
            return deserializer.Deserialize<Map>(text);
        }

        public bool SniffValidFormat(string text)
        {
            return true;
        }

        public string Write(Map map)
        {
            var serializer = new Serializer();
            return serializer.Serialize(map);
        }
    }
}
