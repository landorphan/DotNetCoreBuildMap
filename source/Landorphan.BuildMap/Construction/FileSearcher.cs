using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.BuildMap.Construction
{
    using System.Linq;
    using DotNet.Globbing;
    using Landorphan.BuildMap.Abstractions;
    using Landorphan.BuildMap.Abstractions.FileSystem;

    public class FileSearcher
    {
        public IEnumerable<FilePaths> LocateFiles(string workingDirectory, IEnumerable<string> globPatterns)
        {
            var fs = AbstractionManager.GetFileSystem();
            if (string.IsNullOrWhiteSpace(workingDirectory))
            {
                workingDirectory = fs.GetWorkingDirectory();
            }
            List<FilePaths> retval = new List<FilePaths>();
            var globs =
                (from g in globPatterns
                    select Glob.Parse(g));
            var files = fs.GetFiles(workingDirectory);
            foreach (var file in files)
            {
                foreach (var glob in globs)
                {
                    if (glob.IsMatch(file.Absolute))
                    {
                        retval.Add(file);
                        break;
                    }
                }
            }
            return retval;
        }
    }
}
