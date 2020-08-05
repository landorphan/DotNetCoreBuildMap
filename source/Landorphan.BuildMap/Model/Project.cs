using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
using Landorphan.BuildMap.Model.Support;
using Landorphan.BuildMap.Serialization.Attributes;
using Newtonsoft.Json;

namespace Landorphan.BuildMap.Model
{
    [Serializable]
    [SuppressMessage("CodeSmell", "S109", 
        Justification = "The 'magic' values here are order instructions and it's beter to keep them as a number (tistocks - 2020-08-03)")]
    public class Project
    {
        [JsonProperty(Order = 0)]
        [TableDefaultDisplay]
        public int Group { get; set; }

        [JsonProperty(Order = 1)]
        [TableDefaultDisplay]
        [TextDefaultDisplay]
        public int Item { get; set; }
        
        [XmlArrayItem("Type")]
        [JsonProperty(Order = 2)]
        [TableDefaultDisplay]
        public StringList Types { get; set; } = new StringList();

        [JsonProperty(Order = 3)]
        [TableDefaultDisplay]
        public string Language { get; set; }

        [JsonProperty(Order = 4)]
        [TableDefaultDisplay]
        public string Name { get; set; }

        [JsonProperty(Order = 5)]
        [TableDefaultDisplay]
        public FileStatus Status { get; set; }

        [XmlArrayItem("Solution")]
        [JsonProperty(Order = 6)]
        [TableDefaultDisplay]
        public StringList Solutions { get; set; } = new StringList();

        [JsonProperty(Order = 7)]
        [TableDefaultDisplay]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty(Order = 8)]
        public string RelativePath { get; set; }

        [JsonProperty(Order = 9)]
        [TextDefaultDisplay]
        public string AbsolutePath { get; set; }

        [JsonProperty(Order = 10)]
        public string RealPath { get; set; }

        [XmlArrayItem("Id")]
        [JsonProperty(Order = 11)]
        [TableDefaultDisplay]
        public GuidList DependentOn { get; set; } = new GuidList();
    }
}