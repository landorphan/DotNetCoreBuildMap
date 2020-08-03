using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization.Formatters
{
    public interface IFormatReader
    {
        Map Read(string text);
    }
}