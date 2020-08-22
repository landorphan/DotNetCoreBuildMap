namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    // TODO: Make this internal once we have enough build system to do InternalsVisibleTo
    public class WindowsSegment : Segment
    {
        public static readonly string[] DeviceNames = new []
        {
            "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8",
            "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
        };
        public static readonly WindowsSegment NullSegment = new WindowsSegment(SegmentType.NullSegment, null);
        public static readonly WindowsSegment EmptySegment = new WindowsSegment(SegmentType.EmptySegment, string.Empty);
        public static readonly WindowsSegment SelfSegment = new WindowsSegment(SegmentType.SelfSegment, ".");
        public static readonly WindowsSegment ParentSegment = new WindowsSegment(SegmentType.ParentSegment, "..");

        public override string ToString()
        {
            if (SegmentType == SegmentType.VolumeRelativeSegment ||
                SegmentType == SegmentType.RootSegment)
            {
                return $"{Name}:";
            }

            return Name;
        }

        public static WindowsSegment ParseFromString(string input)
        {
            var match = PathSegmentNotationSegmentRegex.Match(input);
            if (match.Success)
            {
                SegmentType segmentType = PathSegmentNotationComponents.StringToSegmentType[match.Groups[SegmentTypeGroupName].Value];
                string name = match.Groups[SegmentNameGroupName].Value;
                return new WindowsSegment(segmentType, name);
            }
            else
            {
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
                else if (IsDeviceSegment(input))
                {
                    return new WindowsSegment(SegmentType.DeviceSegment, input);
                }
                else
                {
                    return new WindowsSegment(SegmentType.GenericSegment, input);
                }
            }
        }

        public static bool IsDeviceSegment(string input)
        {
            return DeviceNames.Contains(input);
        }

        public WindowsSegment(SegmentType type, string name)
        {
            this.SegmentType = type;
            this.Name = name;
        }

        public override bool IsLegalForSegmentOffset(int offset)
        {
            if (offset > 0 && (this.SegmentType == SegmentType.VolumeRelativeSegment || this.SegmentType == SegmentType.RootSegment))
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
                (this.SegmentType != SegmentType.ParentSegment && this.SegmentType != SegmentType.SelfSegment))
            {
                return false;
            }

            return true;
        }

        public override bool IsDiscouraged()
        {
            if (this.SegmentType != SegmentType.DeviceSegment)
            {
                foreach (var deviceName in WindowsSegment.DeviceNames)
                {
                    if (this.Name.StartsWith(deviceName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            if (this.Name.StartsWith(WindowsRelevantPathCharacters.Space.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal))
            {
                return true;
            }

            return false;
        }

        public override ISegment Clone()
        {
            return new WindowsSegment(this.SegmentType, this.Name);
        }
    }
}
