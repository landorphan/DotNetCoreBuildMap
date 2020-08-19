using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    using System.Globalization;
    using System.Linq;

    public class PosixSegment : Segment
    {
        public static readonly PosixSegment NullSegment = new PosixSegment(SegmentType.NullSegment, null);
        public static readonly PosixSegment EmptySegment = new PosixSegment(SegmentType.EmptySegment, string.Empty);
        public static readonly PosixSegment SelfSegment = new PosixSegment(SegmentType.SelfSegment, ".");
        public static readonly PosixSegment ParentSegment = new PosixSegment(SegmentType.ParentSegment, "..");

        public override string ToString()
        {
            return this.Name;
        }

        public PosixSegment(SegmentType type, string name)
        {
            this.SegmentType = type;
            this.Name = name;
        }

        public static PosixSegment ParseFromString(string input)
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
            else
            {
                return new PosixSegment(SegmentType.GenericSegment, input);
            }
        }


        public override bool IsLegalForSegmentOffset(int offset)
        {
            if (this == ParentSegment || this == EmptySegment || this == SelfSegment || this == NullSegment)
            {
                return true;
            }

            if (Name != null)
            {
                foreach (var alwaysIllegalCharacter in PosixRelevantPathChars.AlwaysIllegalCharacters)
                {
                    if (Name.ToCharArray().Contains(alwaysIllegalCharacter))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override bool IsDiscouraged()
        {
            if (this.Name != null)
            {
                foreach (var segmentChar in Name)
                {
                    if (segmentChar < PosixRelevantPathChars.Space)
                    {
                        return true;
                    }
                }
                if ((this.Name.StartsWith(PosixRelevantPathChars.Space.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal) ||
                     this.Name.EndsWith(PosixRelevantPathChars.Space.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal) ||
                     ((this.SegmentType != SegmentType.SelfSegment &&
                       this.SegmentType != SegmentType.ParentSegment) && 
                      this.Name.EndsWith(PosixRelevantPathChars.Period.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal))))
                {
                    return true;
                }
            }

            return false;
        }

        public override ISegment Clone()
        {
            return new PosixSegment(this.SegmentType, this.Name);
        }

    }
}
