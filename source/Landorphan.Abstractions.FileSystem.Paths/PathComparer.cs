namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System;
    using System.Globalization;
    using Landorphan.Common;

    public sealed class PathComparer : IPathComparer
    {
        private readonly StringComparison stringComparison;

        internal PathComparer(StringComparison comparison)
        {
            comparison.ArgumentMustBeValidEnumValue(nameof(comparison));

            stringComparison = comparison;
        }

        public static IPathComparer CaseInsensitive { get; } = new PathComparer(StringComparison.OrdinalIgnoreCase);

        public static IPathComparer CaseSensitive { get; } = new PathComparer(StringComparison.Ordinal);

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
