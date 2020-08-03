using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using Landorphan.BuildMap.Model;
using Landorphan.BuildMap.Serialization;

namespace dotnetmap.Commands
{
    public class ListCommand : DisplayBase
    {
        public ListCommand() : base("list", "Lists the projects in the map.")
        {
            Handler = CommandHandler.Create<FileInfo, Format, string[], FileInfo>((FileInfo map, 
                Format format, string[] items, FileInfo output) =>
            {
                Console.WriteLine("Called with following arguments:");
                Console.WriteLine($"     --map = {map}");
                Console.WriteLine($"  --format = {format}");
                Console.WriteLine($"   --items = {string.Join(" + ", items)}");
                Console.WriteLine($"  --output = {output}");
            });
        }
    }
}