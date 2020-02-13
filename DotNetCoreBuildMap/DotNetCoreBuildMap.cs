using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace ProjectOrder
{
    public class CreateOrder // : IComparer<KeyValuePair<Guid, List<Guid>>>
    {
        //public int InternalCompare(KeyValuePair<Guid, List<Guid>> x, KeyValuePair<Guid, List<Guid>> y)
        //{
        //    if (y.Value.Contains(x.Key))
        //    {
        //        return 1;
        //    }
        //    if (x.Value.Contains(y.Key))
        //    {
        //        return -1;
        //    }
        //    return x.Key.CompareTo(y.Key);
        //}
        //public int Compare(KeyValuePair<Guid, List<Guid>> x, KeyValuePair<Guid, List<Guid>> y)
        //{
        //    var result = InternalCompare(x,y);
        //    if (result == 0)
        //    {
        //        Console.WriteLine($"{x.Key.ToString().ToUpperInvariant()}={y.Key.ToString().ToUpperInvariant()}");
        //    }
        //    else if (result < 0)
        //    {
        //        Console.WriteLine($"{x.Key.ToString().ToUpperInvariant()}<{y.Key.ToString().ToUpperInvariant()}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{x.Key.ToString().ToUpperInvariant()}>{y.Key.ToString().ToUpperInvariant()}");
        //    }
        //    return result;
        //}

        public static void Main(string[] args)
        {
            var me = new CreateOrder();
            var runParameters = new RunParameters();
            runParameters.SolutionFilePathsList.Add(@"C:\temp\NewProject\NewProjectA\NewProjectA.sln");
            runParameters.SolutionFilePathsList.Add(@"C:\temp\NewProject\NewProjectB\NewProjectB.sln");
            me.Run(runParameters);
        }

//         public void ExtractMap(string path)
//         {
//             var lines = new List<string>();
//             using (var stream = File.OpenRead(path))
//             using (var reader = new StreamReader(stream))
//             {
//                 while (!reader.EndOfStream)
//                 {
//                     var line = reader.ReadLine();
//                     if (!string.IsNullOrWhiteSpace(line))
//                     {
//                         lines.Add(line);
//                     }
//                 }
//             }

//             foreach (var line in lines)
//             {
//                 var parts = line.Split('|');
//                 foreach (var part in parts)
//                 {
//                     if (!string.IsNullOrWhiteSpace(part))
//                     {
//                         var contents = part.Split('>','<');
// //                        Console.Write(contents[1]);
//                     }
//                 }
//             }
//         }
        public void Run(RunParameters runParameters)
        {
            //Dictionary<Guid, List<Guid>> orderMap = new Dictionary<Guid, List<Guid>>();
            //using (var stream = File.OpenRead(args[0]))
            //using (var reader = new StreamReader(stream))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        var parts = line.Split("->");
            //        var project = Guid.Parse(parts[0]);
            //        var dependency = Guid.Parse(parts[1]);
            //        if (!orderMap.TryGetValue(project, out var dependencies))
            //        {
            //            dependencies = new List<Guid>();
            //            orderMap.Add(project, dependencies);
            //        }
            //        dependencies.Add(dependency);
            //    }
            //}
            //var ordered = orderMap.ToList();
            //ordered.Sort(this);
            //foreach (var pair in ordered)
            //{
            //    foreach(var item in pair.Value)
            //    {
            //        Console.WriteLine($"{pair.Key.ToString().ToUpperInvariant()}->{item.ToString().ToUpperInvariant()}");
            //    }
            //}
        }
    }
}