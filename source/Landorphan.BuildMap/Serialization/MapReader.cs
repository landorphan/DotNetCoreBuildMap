using System.IO;
using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization
{
    public class MapReader : IMapReader
    {
        public Map Read(Stream stream, Format formatHint = Format.Map)
        {
            throw new System.NotImplementedException();
        }
    }
}