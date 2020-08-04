using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using dotnetmap.Commands;
using Landorphan.BuildMap.Abstractions;

namespace dotnetmap
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // IFileSystem fs = new FileSystemAbstraction();
            // var files = fs.GetFiles(fs.GetWorkingDirectory());
            // foreach (var file in files)
            // {
            //     Console.WriteLine(file);
            // }
            RootCommand root = new DotNetMapCommand();
            root.InvokeAsync(args);
        }
    }
}
