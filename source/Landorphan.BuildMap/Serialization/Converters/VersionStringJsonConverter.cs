using System;
using Landorphan.BuildMap.Model;
using Landorphan.BuildMap.Model.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Landorphan.BuildMap.Serialization.Converters
{
    using Landorphan.Common;

    public class VersionStringJsonConverter : JsonConverter<VersionString>
    {
        public override void WriteJson(JsonWriter writer, VersionString value, JsonSerializer serializer)
        {
            value.ArgumentNotNull(nameof(value));
            writer.ArgumentNotNull(nameof(writer));
            writer.WriteValue(value.ToString());
        }

        public override VersionString ReadJson(JsonReader reader, Type objectType, VersionString existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            reader.ArgumentNotNull(nameof(reader));

            var str = (string)reader.Value;
            VersionString retval = new VersionString(str);
            return retval;
        }
    }
}