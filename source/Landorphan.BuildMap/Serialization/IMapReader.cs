using System.IO;
using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization
{
    public interface IMapReader
    {
        Map Read(Stream stream, Format formatHint = Format.Map);
    }
}