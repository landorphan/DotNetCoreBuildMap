using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Abstraction
{
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;

    public class PathAbstractionManager
    {
        public static IRuntimeInformation InternalGetRuntimeInformation()
        {
            return new RuntimeInformationAbstraction();
        }

        public static Func<IRuntimeInformation> GetRuntimeInformation { get; set; } = InternalGetRuntimeInformation;
        public static void Reset()
        {
            GetRuntimeInformation = InternalGetRuntimeInformation;
        }
    }
}
