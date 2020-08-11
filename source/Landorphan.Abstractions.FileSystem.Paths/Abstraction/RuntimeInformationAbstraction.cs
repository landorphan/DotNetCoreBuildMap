using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Abstraction
{
    using System.Runtime.InteropServices;

    public class RuntimeInformationAbstraction : IRuntimeInformation
    {
        public bool IsOSPlatform(OSPlatform platform)
        {
            return RuntimeInformation.IsOSPlatform(platform);
        }
    }
}
