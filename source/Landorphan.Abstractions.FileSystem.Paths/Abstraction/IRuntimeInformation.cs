using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Abstraction
{
    using System.Runtime.InteropServices;

    public interface IRuntimeInformation
    {
        bool IsOSPlatform(OSPlatform platform);
    }
}
