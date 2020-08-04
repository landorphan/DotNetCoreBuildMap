using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Landorphan.BuildMap.Model;
using Landorphan.BuildMap.Serialization.Formatters.Implementation;
using Landorphan.BuildMap.Serialization.Formatters.Interfaces;

namespace Landorphan.BuildMap.Serialization
{
    public class MapWritter : IMapWritter
    {
        public void Write(Stream stream, Map map, WriteFormat writeFormat = WriteFormat.Map,
            List<string> items = null)
        {
            IFormatWriter writer = null;
            switch (writeFormat)
            {
                case WriteFormat.Table:
                    writer = new TableFormatter(items);
                    break;
                case WriteFormat.Text:
                    writer = new TextFormatter(items);
                    break;
                case WriteFormat.Yaml:
                    writer = new YamlFormatter();
                    break;
                case WriteFormat.Json:
                    writer = new JsonFormatter();
                    break;
                case WriteFormat.Xml: 
                    writer = new XmlFormatter();
                    break;
                default:
                    writer = new MapFormatter();
                    break;
            }

            string content = writer.Write(map);
            // Create a writer which will not close the stream.
            // The stream is owned by the caller.
            var encoding = new UTF8Encoding(false);
            using (StreamWriter streamWriter = new StreamWriter(stream, encoding, -1, true))
            {
                streamWriter.WriteLine(content);
            }
        }
    }
}