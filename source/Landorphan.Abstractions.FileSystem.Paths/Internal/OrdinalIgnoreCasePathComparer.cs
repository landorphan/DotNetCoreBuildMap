namespace Landorphan.Abstractions.FileSystem.Paths.Internal
{
    using System;

    internal sealed class OrdinalIgnoreCasePathComparer : PathComparer
    {
        /// <inheritdoc/>
        public override int Compare(IPath x, IPath y)
        {
            if (ReferenceEquals(null, x))
            {
                return ReferenceEquals(null, y) ? 0 : -1;
            }

            if (ReferenceEquals(null, y))
            {
                // not null (x) is greater than null (y)
                return 1;
            }

            var xs = x.ToString();
            var ys = y.ToString();
            var comp = StringComparer.OrdinalIgnoreCase;
            return comp.Compare(xs, ys);
        }

        /// <inheritdoc/>
        public override bool Equals(IPath x, IPath y)
        {
            return Compare(x, y) == 0;
        }

        /// <inheritdoc/>
        public override int GetHashCode(IPath obj)
        {
            var s = obj?.ToString();
            if (ReferenceEquals(s, null))
            {
                return 0;
            }

            var comp = StringComparer.OrdinalIgnoreCase;
            return comp.GetHashCode(s);
        }
    }
}
