namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Landorphan.Abstractions.FileSystem.Paths.Internal;

    public abstract class PathComparer : IComparer<IPath>, IEqualityComparer<IPath>, IComparer, IEqualityComparer
    {
        static PathComparer()
        {
            Ordinal = new OrdinalPathComparer();
            OrdinalIgnoreCase = new OrdinalIgnoreCasePathComparer();
        }

        public static PathComparer Create(PathComparison comparison)
        {
            switch (comparison)
            {
                case PathComparison.Ordinal:
                    return new OrdinalPathComparer();

                case PathComparison.OrdinalIgnoreCase:
                    return new OrdinalIgnoreCasePathComparer();
                default:
                    throw new InvalidEnumArgumentException(nameof(comparison), (int)comparison, typeof(PathComparison));
            }
        }

        /// <summary>
        /// Gets a <see cref="PathComparer"></see> object that performs a case-sensitive ordinal path comparison.
        /// </summary>
        /// <returns>
        /// A <see cref="PathComparer"></see> object.
        /// </returns>
        public static PathComparer Ordinal { get; }

        /// <summary>
        /// Gets a <see cref="PathComparer"></see> object that performs a case-insensitive ordinal path comparison.</summary>
        /// <returns>
        /// A <see cref="PathComparer"></see> object.
        /// </returns>
        public static PathComparer OrdinalIgnoreCase { get; }

        /// <inheritdoc/>
        public int Compare(object x, object y)
        {
            return Compare(x as IPath, y as IPath);
        }

        /// <inheritdoc/>
        public abstract int Compare(IPath x, IPath y);

        /// <inheritdoc/>
        public new bool Equals(object x, object y)
        {
            return Equals(x as IPath, y as IPath);
        }

        /// <inheritdoc/>
        public int GetHashCode(object obj)
        {
            return !(obj is IPath asPath) ? 0 : GetHashCode(asPath);
        }

        /// <summary>
        /// Indicates whether two objects are equal.
        /// </summary>
        /// <param name="x">
        /// An object to compare to y.
        /// </param>
        /// <param name="y">
        /// An object to compare to x.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="x"/> and <paramref name="y"/> refer to the same object, or <paramref name="x"/> and <paramref name="y">y</paramref> are both the same type of object and
        /// those objects are equal, or both <paramref name="x">x</paramref> and <paramref name="y"/> are null; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool Equals(IPath x, IPath y);

        /// <summary>
        /// Gets the hash code for the specified object.
        /// </summary>
        /// <param name="obj">
        /// An <see cref="IPath"/>.
        /// </param>
        /// <returns>
        /// A 32-bit signed hash code calculated from the value of the <paramref name="obj">obj</paramref> parameter.
        /// </returns>
        public abstract int GetHashCode(IPath obj);
    }
}
