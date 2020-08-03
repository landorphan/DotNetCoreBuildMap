using System.IO;
using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization
{
    public class MapWritter : IMapWritter
    {
        public void Write(Stream stream, Map map, Format format = Format.Map)
        {
            throw new System.NotImplementedException();
        }
    }
}