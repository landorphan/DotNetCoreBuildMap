namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    public interface IPathComparerAndEquator : IComparer<IPath>, IEqualityComparer<IPath>, IComparer, IEqualityComparer
    {}

    public sealed class PathComparerAndEquator : IPathComparerAndEquator
    {
        private readonly StringComparison stringComparison;

        internal PathComparerAndEquator(StringComparison comparison)
        {
            if (!Enum.IsDefined(typeof(StringComparison), comparison))
            {
                throw new InvalidEnumArgumentException(nameof(comparison), (int)comparison, typeof(StringComparison));
            }

            stringComparison = comparison;
        }

        public static IPathComparerAndEquator CaseInsensitive { get; } = new PathComparerAndEquator(StringComparison.OrdinalIgnoreCase);

        public static IPathComparerAndEquator CaseSensitive { get; } = new PathComparerAndEquator(StringComparison.Ordinal);

        public int Compare(object x, object y)
        {
            return Compare(x as IPath, y as IPath);
        }

        public int Compare(IPath x, IPath y)
        {
            if (ReferenceEquals(null, x))
            {
                // null (x) is less than not null (y)
                return ReferenceEquals(null, y) ? 0 : -1;
            }

            if (ReferenceEquals(null, y))
            {
                // not null (x) is greater than null (y)
                return 1;
            }

            return string.Compare(x.ToPathSegmentNotation(), y.ToPathSegmentNotation(), stringComparison);
        }

        public new bool Equals(object x, object y)
        {
            return Equals(x as IPath, y as IPath);
        }

        public bool Equals(IPath x, IPath y)
        {
            return Compare(x, y) == 0;
        }

        public int GetHashCode(object obj)
        {
            if (!(obj is IPath asIPath))
            {
                return 0;
            }

            return GetHashCode(asIPath);
        }

        public int GetHashCode(IPath obj)
        {
            // ReSharper disable HeuristicUnreachableCode
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (ReferenceEquals(null, obj))
            {
                return 0;
            }

            // ReSharper restore HeuristicUnreachableCode

            switch (stringComparison)
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
    }
}
