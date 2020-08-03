using Landorphan.BuildMap.Model;

namespace Landorphan.BuildMap.Serialization.Formatters.Interfaces
{
    public interface IFormatReader
    {
        bool SniffValidFormat(string text);
        Map Read(string text);
    }
}