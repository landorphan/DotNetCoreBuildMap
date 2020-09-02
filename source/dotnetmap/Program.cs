namespace dotnetmap
{
    using System.CommandLine;
    using dotnetmap.Commands;

    internal static class Program
    {
        private static void Main(string[] args)
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
