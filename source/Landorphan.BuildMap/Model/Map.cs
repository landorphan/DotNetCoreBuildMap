using System;
using System.Reflection;
using Landorphan.BuildMap.Serialization.Attributes;

namespace Landorphan.BuildMap.Model
{
    [Serializable]
    public class Map
    {
        public VersionString SchemaVersion { get; set; } = new VersionString(1,0);

        public VersionString ToolVersion { get; set; } = typeof(Map).Assembly.GetName().Version;
        
        public Build Build { get; set; } = new Build();
    }
}