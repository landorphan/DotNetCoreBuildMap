using Landorphan.BuildMap.Model;
using Landorphan.BuildMap.Serialization.Formatters.Interfaces;
using YamlDotNet.Serialization;

namespace Landorphan.BuildMap.Serialization.Formatters.Implementation
{
    public class YamlFormatter : IFormatter
    {
        public string Write(Map map)
        {
            var serializer = new Serializer();
            return serializer.Serialize(map);
        }

        public Map Read(string text)
        {
            var deserializer = new Deserializer();
            return deserializer.Deserialize<Map>(text);
        }
    }
}