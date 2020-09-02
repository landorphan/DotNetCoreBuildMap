namespace Landorphan.BuildMap.Model
{
    using System;
    using Landorphan.BuildMap.Model.Support;
    using Newtonsoft.Json;

    [Serializable]
    public class Build
    {
        [JsonProperty(Order = 0)]
        public VersionString BuildVersion { get; set; }

        public ProjectList Projects { get; set; } = new ProjectList();

        [JsonProperty(Order = 1)]
        public string RelativeRoot { get; set; }
    }
}
