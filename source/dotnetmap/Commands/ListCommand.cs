using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using Landorphan.BuildMap.Model;
using Landorphan.BuildMap.Serialization;
using Landorphan.BuildMap.Serialization.Formatters.Implementation;

namespace dotnetmap.Commands
{
    using Landorphan.Common;
    public class ListCommand : DisplayBase
    {
        public ListCommand() : base("list", "Lists the projects in the map.")
        {
            Handler = CommandHandler.Create<FileInfo, WriteFormat, IEnumerable<string>, FileInfo>(ListMap);
        }

        public void ListMap(FileInfo map, WriteFormat format, IEnumerable<string> items, FileInfo output)
        {
            map.ArgumentNotNull(nameof(map));

            Console.Error.WriteLine("Listing projects...");
            IMapReader reader = new MapReader();
            IMapWritter writer = new MapWritter();
            Map mapObject = null;
            ReadFormat formatHint;

            Console.Error.WriteLine("Determining map structure...");
            switch (map.Extension)
            {
                case ".yml":
                case ".yaml":
                    formatHint = ReadFormat.Yaml;
                    break;
                case ".json":
                    formatHint = ReadFormat.Json;
                    break;
                case ".xml":
                    formatHint = ReadFormat.Xml;
                    break;
                default:
                    formatHint = ReadFormat.Map;
                    break;
            }
            Console.Error.WriteLine($"Map Structure is {formatHint}...");

            Console.Error.WriteLine($"Reading Input...");
            using (var stream = map.OpenRead())
            {
                mapObject = reader.Read(stream, formatHint);
            }

            if (mapObject != null)
            {
                Console.Error.WriteLine($"Map successfully read ... ");
                byte[] data;
                using (var memstream = new MemoryStream())
                {
                    Console.Error.WriteLine("Writing output");
                    writer.Write(memstream, mapObject, format, items);
                    data = memstream.GetBuffer();
                }
                Stream outputStream = null;
                try
                {
                    if (output != null)
                    {
                        outputStream = output.OpenWrite();
                    }
                    else
                    {
                        outputStream = Console.OpenStandardOutput();
                    }
                    outputStream.Write(data);
                }
                finally
                {
                    // Only close a supplied file...
                    // Do not close StandardOutput!
                    if (output != null)
                    {
                        outputStream.Close();
                    }
                }
            }

            // Console.WriteLine("Called with following arguments:");
            // Console.WriteLine($"     --map = {map}");
            // Console.WriteLine($"  --format = {format}");
            // Console.WriteLine($"   --items = {string.Join(" + ", items)}");
            // Console.WriteLine($"  --output = {output}");
        }
    }
}