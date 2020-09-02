namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Converters
{
    using System;
    using Landorphan.Common;
    using Newtonsoft.Json;

    public class ParsedPathConverter : JsonConverter<ParsedPath>
    {
        public override ParsedPath ReadJson(JsonReader reader, Type objectType, ParsedPath existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            reader.ArgumentNotNull(nameof(reader));
            var str = (string)reader.Value;
            var parser = new PathParser();
            var retval = (ParsedPath)parser.Parse(str);
            return retval;
        }

        public override void WriteJson(JsonWriter writer, ParsedPath value, JsonSerializer serializer)
        {
            writer.ArgumentNotNull(nameof(writer));
            value.ArgumentNotNull(nameof(value));
            if (value.SerializationMethod == SerializationForm.Simple)
            {
                writer.WriteValue(value.ToString());
            }
            else
            {
                writer.WriteValue(value.ToPathSegmentNotation());
            }
        }
    }
}
