using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Globalization;

    public interface IPathComparerAndEquator : IComparer<IPath>, IEqualityComparer<IPath>
    {
    }

    public class PathComparerAndEquator : IPathComparerAndEquator
    {
        private StringComparison comparisonMethod;
        public PathComparerAndEquator(StringComparison comparisonMethod)
        {
            this.comparisonMethod = comparisonMethod;
        }

        public int Compare(IPath x, IPath y)
        {
            return string.Compare(x.ToPathSegmentNotation(), y.ToPathSegmentNotation(), comparisonMethod);
        }

        public bool Equals(IPath x, IPath y)
        {
            return Compare(x, y) == 0;
        }

        public int GetHashCode(IPath obj)
        {
            switch (comparisonMethod)
            {
                case StringComparison.OrdinalIgnoreCase:
                case StringComparison.InvariantCultureIgnoreCase:
                    return obj.ToPathSegmentNotation().ToUpper(CultureInfo.InvariantCulture).GetHashCode();
                case StringComparison.CurrentCultureIgnoreCase:
                    return obj.ToPathSegmentNotation().ToUpper(CultureInfo.CurrentCulture).GetHashCode();
                case StringComparison.Ordinal:
                case StringComparison.InvariantCulture:
                case StringComparison.CurrentCulture:
                default:
                    return obj.ToPathSegmentNotation().GetHashCode();
            }
        }

        public static IPathComparerAndEquator CaseSensitive { get; } = new PathComparerAndEquator(StringComparison.Ordinal);
        public static IPathComparerAndEquator CaseInsensitive { get; } = new PathComparerAndEquator(StringComparison.OrdinalIgnoreCase);
    }
}
