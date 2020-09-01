using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Converters
{
    using Newtonsoft.Json;

    public class ParsedPathConverter : JsonConverter<ParsedPath>
    {
        public override void WriteJson(JsonWriter writer, ParsedPath value, JsonSerializer serializer)
        {
            if (value.SerializationMethod == SerializationForm.Simple)
            {
                writer.WriteValue(value.ToString());
            }
            else
            {
                writer.WriteValue(value.ToPathSegmentNotation());
            }
        }

        public override ParsedPath ReadJson(JsonReader reader, Type objectType, ParsedPath existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var str = (string)reader.Value;
            PathParser parser = new PathParser();
            ParsedPath retval = (ParsedPath)parser.Parse(str);
            return retval;
        }
    }
}
