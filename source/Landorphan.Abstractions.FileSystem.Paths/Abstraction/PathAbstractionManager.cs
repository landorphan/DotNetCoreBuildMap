namespace Landorphan.Abstractions.FileSystem.Paths.Abstraction
{
    using System;

    public static class PathAbstractionManager
    {
        public static Func<IRuntimeInformation> GetRuntimeInformation { get; set; } = InternalGetRuntimeInformation;

        public static IRuntimeInformation InternalGetRuntimeInformation()
        {
            return new RuntimeInformationAbstraction();
        }

        public static void Reset()
        {
            GetRuntimeInformation = InternalGetRuntimeInformation;
        }
    }
}
