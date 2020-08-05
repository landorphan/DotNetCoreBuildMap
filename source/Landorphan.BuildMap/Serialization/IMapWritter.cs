using System.Collections.Generic;
using System.IO;
using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization
{
    using YamlDotNet.Core.Events;

    public interface IMapWritter
    {
        void Write(Stream stream, Map map);

        public void Write(Stream stream, Map map, WriteFormat writeFormat);

        void Write(Stream stream, Map map, WriteFormat writeFormat, IEnumerable<string> items);
    }
}
