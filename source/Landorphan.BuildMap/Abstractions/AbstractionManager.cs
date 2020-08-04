using System;
using System.Runtime.CompilerServices;

namespace Landorphan.BuildMap.Abstractions
{
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