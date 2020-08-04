using System;
using System.Diagnostics.CodeAnalysis;

namespace Landorphan.BuildMap.Abstractions
{
    [SuppressMessage("CodeSmell", "S1104: Make this filed 'private' and encapsulate it in a 'public' property", 
        Justification = "This is by desogn so that this can be overriden by tests.  (tistocks - 2020-08-03)")]
    [SuppressMessage("Unknown", "CA2211: Non-constatn fields should not be visible", 
        Justification = "This is by desogn so that this can be overriden by tests.  (tistocks - 2020-08-03)")]
    [SuppressMessage("CodeSmell", "S2223: Change the visibility of ... or make it 'const' or 'readonly'",
        Justification = "This is by desogn so that this can be overriden by tests. (tistocks - 2020-08-03")]
    public static class AbstractionManager
    {
        public static IFileSystem InternalGetFileSystem()
        {
            return new FileSystemAbstraction();
        }

        public static void Reset()
        {
            GetFileSystem = InternalGetFileSystem;
        }

        public static Func<IFileSystem> GetFileSystem = InternalGetFileSystem;
    }
}