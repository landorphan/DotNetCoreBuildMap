namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
    using System;
    using System.Linq;

    // TODO: Make this internal once we have enough build system to do InternalsVisibleTo
    public class WindowsSegment : Segment
    {
        public static readonly string[] DeviceNames = new String[]
        {
            "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8",
            "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
        };
        public static readonly WindowsSegment NullSegment = new WindowsSegment(SegmentType.NullSegment, null);
        public static readonly WindowsSegment EmptySegment = new WindowsSegment(SegmentType.EmptySegment, string.Empty);
        public static readonly WindowsSegment SelfSegment = new WindowsSegment(SegmentType.SelfSegment, ".");
        public static readonly WindowsSegment ParentSegment = new WindowsSegment(SegmentType.ParentSegment, "..");

        public static WindowsSegment ParseFromString(string input)
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

        public static bool IsDeviceSegment(string input)
        {
            return DeviceNames.Contains(input);
        }

        public WindowsSegment(SegmentType type, string name)
        {
            this.SegmentType = type;
            this.Name = name;
        }

        public override bool IsLegal()
        {
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

            if (Name.EndsWith(WindowsRelevantPathCharacters.Space.ToString(), StringComparison.Ordinal))
            {
                return false;
            }

            if (Name.EndsWith(WindowsRelevantPathCharacters.Period.ToString(), StringComparison.Ordinal) &&
                (this != ParentSegment || this != SelfSegment))
            {
                return false;
            }

            return true;
        }

        public override ISegment Clone()
        {
            return new WindowsSegment(this.SegmentType, this.Name);
        }
    }
}
