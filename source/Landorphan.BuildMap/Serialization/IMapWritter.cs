using System.Collections.Generic;
using System.IO;
using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization
{
    public interface IMapWritter
    {
        void Write(Stream stream, Map map, WriteFormat writeFormat = WriteFormat.Map, 
            IEnumerable<string> items = null);
    }
}