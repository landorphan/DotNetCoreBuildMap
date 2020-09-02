namespace Landorphan.BuildMap.Serialization.Converters
{
    using System;
    using Landorphan.BuildMap.Model.Support;
    using Landorphan.Common;
    using Newtonsoft.Json;

    public class VersionStringJsonConverter : JsonConverter<VersionString>
    {
        public override VersionString ReadJson(JsonReader reader, Type objectType, VersionString existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            reader.ArgumentNotNull(nameof(reader));

            var str = (string)reader.Value;
            var retval = new VersionString(str);
            return retval;
        }

        public override void WriteJson(JsonWriter writer, VersionString value, JsonSerializer serializer)
        {
            value.ArgumentNotNull(nameof(value));
            writer.ArgumentNotNull(nameof(writer));
            writer.WriteValue(value.ToString());
        }
    }
}
