using System.IO;
using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization
{
    public interface IMapWritter
    {
        void Write(Stream stream, Map map, Format format = Format.Map);
    }
}