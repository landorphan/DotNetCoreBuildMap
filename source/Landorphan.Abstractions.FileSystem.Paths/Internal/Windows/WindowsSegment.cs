namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
    using System;
    using System.Globalization;
    using System.Linq;

    // TODO: Make this internal once we have enough build system to do InternalsVisibleTo
    public class WindowsSegment : Segment
    {
        public static readonly string[] DeviceNames =
        {
            "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8",
            "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
        };
        public static readonly WindowsSegment EmptySegment = new WindowsSegment(SegmentType.EmptySegment, string.Empty);
        public static readonly WindowsSegment NullSegment = new WindowsSegment(SegmentType.NullSegment, null);
        public static readonly WindowsSegment ParentSegment = new WindowsSegment(SegmentType.ParentSegment, "..");
        public static readonly WindowsSegment SelfSegment = new WindowsSegment(SegmentType.SelfSegment, ".");

        public WindowsSegment(SegmentType type, string name)
        {
            SegmentType = type;
            switch (type)
            {
                case SegmentType.DeviceSegment:
                case SegmentType.GenericSegment:
                case SegmentType.RemoteSegment:
                case SegmentType.RootSegment:
                case SegmentType.VolumeRelativeSegment:
                case SegmentType.ParentSegment:
                case SegmentType.SelfSegment:
                    Name = name;
                    break;
                case SegmentType.VolumelessRootSegment:
                case SegmentType.EmptySegment:
                case SegmentType.NullSegment:
                    Name = string.Empty;
                    break;
#pragma warning disable S3532 // Empty "default" clauses should be removed
                default:
                    break;
#pragma warning restore S3532 // Empty "default" clauses should be removed
            }
        }

        public override ISegment DeepClone()
        {
            return new WindowsSegment(SegmentType, Name);
        }

        public static bool IsDeviceSegment(string input)
        {
            return DeviceNames.Contains(input);
        }

        public static WindowsSegment ParseFromString(string input)
        {
            var match = PathSegmentNotationSegmentRegex.Match(input);
            if (match.Success)
            {
                var segmentType = PathSegmentNotationComponents.StringToSegmentType[match.Groups[SegmentTypeGroupName].Value];
                var name = match.Groups[SegmentNameGroupName].Value;
                return new WindowsSegment(segmentType, NameFromPathSegmentNotationEncodedName(name));
            }

            if (input == ".")
            {
                return SelfSegment;
            }

            if (input == "..")
            {
                return ParentSegment;
            }

            if (input == null)
            {
                return NullSegment;
            }

            if (input.Length == 0)
            {
                return EmptySegment;
            }

            if (IsDeviceSegment(input))
            {
                return new WindowsSegment(SegmentType.DeviceSegment, input);
            }

            return new WindowsSegment(SegmentType.GenericSegment, input);
        }

        public override bool IsDiscouraged()
        {
            if (SegmentType != SegmentType.DeviceSegment)
            {
                foreach (var deviceName in DeviceNames)
                {
                    if (Name.StartsWith(deviceName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            if (Name.StartsWith(WindowsRelevantPathCharacters.Space.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal))
            {
                return true;
            }

            return false;
        }

        public override bool IsLegalForSegmentOffset(int offset)
        {
            if (offset > 0 && (SegmentType == SegmentType.VolumeRelativeSegment || SegmentType == SegmentType.RootSegment))
            {
                return false;
            }

            if (this == ParentSegment || this == EmptySegment || this == SelfSegment || this == NullSegment)
            {
                return true;
            }

            if (Name != null)
            {
                foreach (var alwaysIllegalCharacter in WindowsRelevantPathCharacters.AlwaysIllegalCharacters)
                {
                    if (Name.ToCharArray().Contains(alwaysIllegalCharacter))
                    {
                        return false;
                    }
                }
            }

            if (Name.EndsWith(WindowsRelevantPathCharacters.Space.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal))
            {
                return false;
            }

            if (Name.EndsWith(WindowsRelevantPathCharacters.Period.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal) &&
                SegmentType != SegmentType.ParentSegment &&
                SegmentType != SegmentType.SelfSegment)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            if (SegmentType == SegmentType.VolumeRelativeSegment ||
                SegmentType == SegmentType.RootSegment)
            {
                return $"{Name}:";
            }

            return Name;
        }
    }
}
