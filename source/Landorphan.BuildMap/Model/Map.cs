namespace Landorphan.BuildMap.Model
{
    using System;
    using Landorphan.BuildMap.Model.Support;

    [Serializable]
    public class Map
    {
        public Build Build { get; set; } = new Build();
        public VersionString SchemaVersion { get; set; } = new VersionString(1, 0);

        public VersionString ToolVersion { get; set; } = typeof(Map).Assembly.GetName().Version;
    }
}
