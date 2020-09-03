namespace Landorphan.Abstractions.FileSystem.Paths
{
    public interface IPathParser
    {
        IPath Parse(string pathString);
        IPath Parse(string pathString, PathType pathType);
    }
}
