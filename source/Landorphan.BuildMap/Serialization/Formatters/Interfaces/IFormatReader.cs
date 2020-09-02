namespace Landorphan.BuildMap.Serialization.Formatters.Interfaces
{
    using Landorphan.BuildMap.Model;

    public interface IFormatReader
    {
        Map Read(string text);
        bool SniffValidFormat(string text);
    }
}
