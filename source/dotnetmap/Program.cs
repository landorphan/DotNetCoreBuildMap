using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using dotnetmap.Commands;

namespace dotnetmap
{
    class Program
    {
        static void Main(string[] args)
        {
            RootCommand root = new DotNetMapCommand();
            root.InvokeAsync(args);
        }
    }
}
