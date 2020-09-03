namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IPathComparer : IComparer<IPath>, IEqualityComparer<IPath>, IComparer, IEqualityComparer
    {
    }
}
