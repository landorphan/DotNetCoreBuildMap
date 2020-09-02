namespace Landorphan.BuildMap.Serialization.Formatters.Interfaces
{
    using Landorphan.BuildMap.Model;

    public interface IFormatWriter
    {
        string Write(Map map);
    }
}
