using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using Landorphan.BuildMap.Model;
using Landorphan.BuildMap.Serialization.Formatters.Implementation;
using Landorphan.BuildMap.Serialization.Formatters.Interfaces;
using Newtonsoft.Json;

namespace Landorphan.BuildMap.Serialization
{
    public class MapReader : IMapReader
    {
        private Queue<IFormatReader> orderedReaders = new Queue<IFormatReader>();
        private Dictionary<ReadFormat, IFormatReader> organizedReaders = new Dictionary<ReadFormat, IFormatReader>();
        public MapReader()
        {
            IFormatReader reader = new MapFormatter();
            orderedReaders.Enqueue(reader);
            organizedReaders.Add(ReadFormat.Map, reader);
            reader = new XmlFormatter();
            orderedReaders.Enqueue(reader);
            organizedReaders.Add(ReadFormat.Xml, reader);
            reader = new JsonFormatter();
            orderedReaders.Enqueue(reader);
            organizedReaders.Add(ReadFormat.Json, reader);
            reader = new YamlFormatter();
            orderedReaders.Enqueue(reader);
            organizedReaders.Add(ReadFormat.Yaml, reader);
        }
        
        public Map Read(Stream stream, ReadFormat formatHint = ReadFormat.Map)
        {
            Map map = null;
            string contents;
            // Create a reader which will not close a stream ... 
            // The stream is owned by the caller and not this code.
            using (var reader = new StreamReader(stream, Encoding.UTF8, 
                false, -1, true))
            {
                contents = reader.ReadToEnd();
            }
            
            Console.Error.WriteLine($"Read contents Hint = {formatHint}.. ");
            // First try the suggested format.
            if (!TryRead(contents, organizedReaders[formatHint], out map))
            {
                // Next go through the list and try each reader.
                Console.Error.WriteLine($"Unable to read iterating over readers..");
                foreach (var reader in orderedReaders)
                {
                    if (TryRead(contents, reader, out map))
                    {
                        break;
                    }
                }
            }

            return map;
        }

        private bool TryRead(string text, IFormatReader reader, out Map map)
        {
            map = null;
            try
            {
                if (reader.SniffValidFormat(text))
                {
                    map = reader.Read(text);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
            }
            return false;
        }
    }
}