namespace Landorphan.BuildMap.Serialization
{
    using System.IO;
    using Landorphan.BuildMap.Model;

    public interface IMapReader
    {
        Map Read(Stream stream);

        Map Read(Stream stream, ReadFormat formatHint);
    }
}
