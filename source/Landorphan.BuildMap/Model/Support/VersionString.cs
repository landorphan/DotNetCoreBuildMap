namespace Landorphan.BuildMap.Model.Support
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using Landorphan.BuildMap.Serialization.Converters;
    using Landorphan.Common;
    using Newtonsoft.Json;
    using YamlDotNet.Core;
    using YamlDotNet.Core.Events;
    using YamlDotNet.Serialization;
    using Version = System.Version;

    [Serializable]
    [JsonConverter(typeof(VersionStringJsonConverter))]
    public class VersionString : IYamlConvertible, IXmlSerializable
    {
        public const string Colon = ":";
        public const string Dash = "-";
        public const string Dot = ".";
        public const string Release = "release";

        private const string regexPattern = @"(?<Major>\d+)(?:\.(?<Minor>\d+))?(?:\.(?<Build>\d+))?(?:\.(?<Revision>\d+))?(?:-(?<Moniker>[a-zA-Z0-9_-]+))?(?:\:(?<Hash>[0-9A-Fa-f]+))?";

        private readonly Regex parsePattern = new Regex(regexPattern, RegexOptions.Compiled);

        public VersionString()
        {
        }

        public VersionString(string version)
        {
            SetFromString(version);
        }

        public VersionString(int major) : this(major, 0, null, null, null, null)
        {
        }

        public VersionString(int major, int minor) : this(major, minor, null, null, null, null)
        {
        }

        public VersionString(int major, int minor, int build) : this(major, minor, build, null, null, null)
        {
        }

        public VersionString(int major, int minor, int build, int revision) : this(major, minor, build, revision, null, null)
        {
        }

        public VersionString(int major, int minor, int build, int revision, string moniker) : this(major, minor, build, revision, moniker, null)
        {
        }

        public VersionString(int major, int minor, int? build, int? revision, string moniker, string hash)
        {
            SetValues(major, minor, build, revision, moniker, hash);
        }

        public int? Build { get; set; }
        public string Hash { get; set; }

        public int Major { get; set; }
        public int Minor { get; set; }
        public string Moniker { get; set; }
        public int? Revision { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ArgumentNotNull(nameof(reader));
            SetFromString(reader.ReadString());
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.ArgumentNotNull(nameof(writer));
            writer.WriteString(ToString());
        }

        public void Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            var value = parser.Consume<Scalar>().Value;
            SetFromString(value);
        }

        public void Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            emitter.ArgumentNotNull(nameof(emitter));
            emitter.Emit(
                new Scalar(
                    null,
                    null,
                    ToString(),
                    ScalarStyle.Any,
                    true,
                    false));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
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

        [SuppressMessage(
            "CodeSmell",
            "S1541: Reduce Cyclomatic Complexity",
            Justification = "Cyclomatic Complexity for this methid is as low as posible.  (tistocks - 2020-08-05)")]
        private void SetFromString(string version)
        {
            var match = parsePattern.Match(version);
            foreach (Group group in match.Groups)
            {
                switch (group.Name)
                {
                    case nameof(Major):
                        Major = int.Parse(group.Value, CultureInfo.InvariantCulture);
                        break;
                    case nameof(Minor):
                        Minor = int.Parse(group.Value, CultureInfo.InvariantCulture);
                        break;
                    case nameof(Build):
                        if (group.Captures.Any())
                        {
                            Build = int.Parse(group.Value, CultureInfo.InvariantCulture);
                        }

                        break;
                    case nameof(Revision):
                        if (group.Captures.Any())
                        {
                            Revision = int.Parse(group.Value, CultureInfo.InvariantCulture);
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
                }
            }
        }

        private void SetValues(
            int major,
            int minor,
            int? build,
            int? revision,
            string moniker,
            string hash)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
            Moniker = moniker;
            Hash = hash;
        }

        public static implicit operator VersionString(Version version)
        {
            version.ArgumentNotNull(nameof(version));

            var retval = new VersionString();
            retval.Major = version.Major;
            retval.Minor = version.Minor;
            retval.Build = version.Build;
            retval.Revision = version.Revision;
            return retval;
        }

        public static implicit operator Version(VersionString versionString)
        {
            versionString.ArgumentNotNull(nameof(versionString));

            Version retval;
            if (versionString.Revision.HasValue && versionString.Build.HasValue)
            {
                retval = new Version(
                    versionString.Major,
                    versionString.Minor,
                    versionString.Build.Value,
                    versionString.Revision.Value);
            }
            else if (versionString.Build.HasValue)
            {
                retval = new Version(
                    versionString.Major,
                    versionString.Minor,
                    versionString.Build.Value);
            }
            else
            {
                retval = new Version(versionString.Major, versionString.Minor);
            }

            return retval;
        }
    }
}
