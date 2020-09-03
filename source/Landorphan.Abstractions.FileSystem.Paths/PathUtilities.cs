namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Runtime.InteropServices;
    using Landorphan.Abstractions.FileSystem.Paths.Abstraction;

    public static class PathUtilities
    {
        public static IPathComparer CaseInsensitiveComparer { get; } = PathComparer.CaseInsensitive;

        public static IPathComparer CaseSensitiveComparer { get; } = PathComparer.CaseSensitive;

        public static IPathComparer DefaultComparer
        {
            get
            {
                // ri is not cached by design
                // TODO: how to override in a test scenario?
                IRuntimeInformation ri = new RuntimeInformationAbstraction();
                return ri.IsOSPlatform(OSPlatform.Windows) ? CaseInsensitiveComparer : CaseSensitiveComparer;
            }
        }

        public static SerializationForm DefaultSerializationMethod { get; set; }
    }
}
