using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization.Formatters.Interfaces
{
    public interface IFormatReader
    {
        Map Read(string text);
    }
}