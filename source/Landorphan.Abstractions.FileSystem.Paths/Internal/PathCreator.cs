using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal
{
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Posix;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;

    public interface IPathCreator
    {
        IPath CreatePath(PathType pathType);
        ISegment CreateSegment(PathType pathType, SegmentType type, string name);
    }

    public class PathCreator : IPathCreator
    {
        public IPath CreatePath(PathType pathType)
        {
            if (pathType == PathType.Windows)
            {
                return new WindowsPath();
            }
            return new PostixPath();
        }

        public ISegment CreateSegment(PathType pathType, SegmentType type, string name)
        {
            if (pathType == PathType.Windows)
            {
                return new WindowsSegment(type, name);
            }
            return new PosixSegment(type, name);
        }
    }
}
