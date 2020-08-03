using System;
using Landorphan.BuildMap.Model;
using Landorphan.BuildMap.Model.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Landorphan.BuildMap.Serialization.Converters
{
    public class VersionStringJsonConverter : JsonConverter<VersionString>
    {
        public override void WriteJson(JsonWriter writer, VersionString value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override VersionString ReadJson(JsonReader reader, Type objectType, VersionString existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var str = (string) reader.Value;
            VersionString retval = new VersionString(str);
            return retval;
        }
    }
}