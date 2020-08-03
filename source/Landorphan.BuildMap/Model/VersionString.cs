using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Landorphan.BuildMap.Serialization.Converters;
using Newtonsoft.Json;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using Version = System.Version;

namespace Landorphan.BuildMap.Model
{
    [Serializable]
    [JsonConverter(typeof(VersionStringJsonConverter))]
    public class VersionString : IYamlConvertible, IXmlSerializable
    {
        public const string Dot = ".";
        public const string Dash = "-";
        public const string Colon = ":";
        public const string Release = "release";

        private const string regexPattern = @"(?<Major>\d+)(?:\.(?<Minor>\d+))?(?:\.(?<Build>\d+))?(?:\.(?<Revision>\d+))?(?:-(?<Moniker>[a-zA-Z0-9_-]+))?(?:\:(?<Hash>[0-9A-Fa-f]+))?";

        protected readonly Regex parsePattern = new Regex(regexPattern, RegexOptions.Compiled);

        public int Major { get; set; } = 0;
        public int Minor { get; set; } = 0;
        public int? Build { get; set; }
        public int? Revision { get; set; }
        public string Moniker { get; set; }
        public string Hash { get; set; }

        public static implicit operator VersionString(Version version)
        {
            VersionString retval = new VersionString();
            retval.Major = version.Major;
            retval.Minor = version.Minor;
            retval.Build = version.Build;
            retval.Revision = version.Revision;
            return retval;
        }

        public static implicit operator Version(VersionString versionString)
        {
            Version retval;
            if (versionString.Revision.HasValue && versionString.Build.HasValue)
            {
                retval = new Version(versionString.Major, versionString.Minor, 
                                     versionString.Build.Value, versionString.Revision.Value);
            }
            else if (versionString.Build.HasValue)
            {
                retval = new Version(versionString.Major, versionString.Minor, 
                                     versionString.Build.Value);
            }
            else
            {
                retval = new Version(versionString.Major, versionString.Minor);
            }

            return retval;
        }

        public VersionString()
        {
        }

        public VersionString(string version)
        {
            this.SetFromString(version);            
        }
        
        private void SetFromString(string version)
        {
            var match = parsePattern.Match(version);
            foreach (Group group in match.Groups)
            {
                switch (group.Name)
                {
                    case nameof(Major):
                        Major = int.Parse(group.Value);
                        break;
                    case nameof(Minor):
                        Minor = int.Parse(group.Value);
                        break;
                    case nameof(Build):
                        if (group.Captures.Any())
                        {
                            Build = int.Parse(group.Value);
                        }
                        break;
                    case nameof(Revision):
                        if (group.Captures.Any())
                        {
                            Revision = int.Parse(group.Value);
                        }
                        break;
                    case nameof(Moniker):
                        if (group.Captures.Any())
                        {
                            Moniker = group.Value;
                        }
                        break;
                    case nameof(Hash):
                        if (group.Captures.Any())
                        {
                            Hash = group.Value;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetValues(int major, int minor = 0, int? build = null,
                               int? revision = null, string moniker = null, string hash = null)
        {
            this.Major = major;
            this.Minor = minor;
            this.Build = build;
            this.Revision = revision;
            this.Moniker = moniker;
            this.Hash = hash;
        }
        
        public VersionString(int major, int minor = 0, int? build = null, 
                             int? revision = null, string moniker = null, string hash = null)
        {
            this.SetValues(major, minor, build, revision, moniker, hash);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Major);
            builder.Append(Dot);
            builder.Append(Minor);
            if (Build.HasValue)
            {
                builder.Append(Dot);
                builder.Append(Build.Value);
            }
            if (Revision.HasValue)
            {
                builder.Append(Dot);
                builder.Append(Revision.Value);
            }

            if (!string.IsNullOrWhiteSpace(Moniker) &&
                !Release.Equals(Moniker, StringComparison.OrdinalIgnoreCase))
            {
                builder.Append(Dash);
                builder.Append(Moniker);
            }

            if (!string.IsNullOrWhiteSpace(Hash))
            {
                builder.Append(Colon);
                builder.Append(Hash);
            }

            return builder.ToString();
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var value = parser.Consume<Scalar>().Value;
            this.SetFromString(value);
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            emitter.Emit(new YamlDotNet.Core.Events.Scalar(null, null, this.ToString(), 
                         ScalarStyle.Any, true, false));
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            this.SetFromString(reader.ReadString());
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(this.ToString());
        }
    }
}