using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization.Formatters
{
    public interface IFormatWriter
    {
        string Write(Map map);
    }
}