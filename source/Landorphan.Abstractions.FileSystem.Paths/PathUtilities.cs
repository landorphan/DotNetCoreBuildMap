namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Runtime.InteropServices;
    using Landorphan.Abstractions.FileSystem.Paths.Abstraction;

    public static class PathUtilities
    {
        public static IPathComparerAndEquator CaseInsensitiveComparerAndEquator { get; } = PathComparerAndEquator.CaseInsensitive;
        public static IPathComparerAndEquator CaseSensitiveComparerAndEquator { get; } = PathComparerAndEquator.CaseSensitive;

        public static IPathComparerAndEquator DefaultComparerAndEquator
        {
            get
            {
                // ri is not cached by design
                // TODO: how to override in a test scenario?
                IRuntimeInformation ri = new RuntimeInformationAbstraction();
                return ri.IsOSPlatform(OSPlatform.Windows) ? CaseInsensitiveComparerAndEquator : CaseSensitiveComparerAndEquator;
            }
        }
    }
}
