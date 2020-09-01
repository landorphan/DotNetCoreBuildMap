namespace Landorphan.BuildMap.Model
{
    public enum FileStatus
    {
        Unknown = 0,
        Missing = 1,
        Malformed = 2,
        Empty = 3,
        Circular = 4,
        Valid = 10,
    }
}
