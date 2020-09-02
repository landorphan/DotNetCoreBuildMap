namespace Landorphan.BuildMap.Serialization
{
    using System.Collections.Generic;
    using System.IO;
    using Landorphan.BuildMap.Model;

    public interface IMapWriter
    {
        void Write(Stream stream, Map map);

        public void Write(Stream stream, Map map, WriteFormat writeFormat);

        void Write(Stream stream, Map map, WriteFormat writeFormat, IEnumerable<string> items);
    }
}
