using System;
using System.Collections.ObjectModel;
using System.Linq;
using Landorphan.BuildMap.Serialization.Attributes;
using Newtonsoft.Json;

namespace Landorphan.BuildMap.Model
{
    [Serializable]
    public class Build
    {
        [JsonProperty(Order = 0)]
        public VersionString BuildVersion { get; set; }
        
        [JsonProperty(Order = 1)]
        public string RelativeRoot { get; set; }
        
        public ProjectList Projects { get; set; } = new ProjectList();
    }
}