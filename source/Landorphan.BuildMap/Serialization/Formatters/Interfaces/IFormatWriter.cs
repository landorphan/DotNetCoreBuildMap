using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization.Formatters.Interfaces
{
    public interface IFormatWriter
    {
        string Write(Map map);
    }
}