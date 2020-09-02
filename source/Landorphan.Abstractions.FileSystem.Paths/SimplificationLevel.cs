namespace Landorphan.Abstractions.FileSystem.Paths
{
    public enum SimplificationLevel
    {
        NotNormalized = 0,
        LeadingParentsOnly = 1,
        SelfReferenceOnly = 2,
        Fully = 3
    }
}
