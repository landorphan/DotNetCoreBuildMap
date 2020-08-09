using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    using System.Linq;

    public class PosixSegment : Segment
    {
        public static readonly PosixSegment NullSegment = new PosixSegment(SegmentType.NullSegment, null);
        public static readonly PosixSegment EmptySegment = new PosixSegment(SegmentType.EmptySegment, string.Empty);
        public static readonly PosixSegment SelfSegment = new PosixSegment(SegmentType.SelfSegment, ".");
        public static readonly PosixSegment ParentSegment = new PosixSegment(SegmentType.ParentSegment, "..");

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


        public override bool IsLegal()
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
    }
}
