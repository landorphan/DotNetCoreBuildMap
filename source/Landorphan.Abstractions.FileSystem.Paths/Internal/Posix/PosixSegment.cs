﻿using System;
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
            switch (type)
            {
                case SegmentType.DeviceSegment:
                case SegmentType.GenericSegment:
                case SegmentType.RemoteSegment:
                case SegmentType.RootSegment:
                case SegmentType.VolumeRelativeSegment:
                case SegmentType.ParentSegment:
                case SegmentType.SelfSegment:
                    this.Name = name;
                    break;
                case SegmentType.VolumelessRootSegment:
                case SegmentType.EmptySegment:
                case SegmentType.NullSegment:
                    this.Name = string.Empty;
                    break;
#pragma warning disable S3532 // Empty "default" clauses should be removed
                default:
                    break;
#pragma warning restore S3532 // Empty "default" clauses should be removed
            }
        }

        public static PosixSegment ParseFromString(string input)
        {
            var match = PathSegmentNotationSegmentRegex.Match(input);
            if (match.Success)
            {
                SegmentType segmentType = PathSegmentNotationComponents.StringToSegmentType[match.Groups[SegmentTypeGroupName].Value];
                string name = match.Groups[SegmentNameGroupName].Value;
                return new PosixSegment(segmentType, NameFromPathSegmentNotationEncodedName(name));
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
                else
                {
                    return new PosixSegment(SegmentType.GenericSegment, input);
                }
            }
        }

        public override bool IsLegalForSegmentOffset(int offset)
        {
            switch (this.SegmentType)
            {
                case SegmentType.EmptySegment:
                case SegmentType.NullSegment:
                    return string.IsNullOrWhiteSpace(Name);
                case SegmentType.SelfSegment:
                    return string.IsNullOrWhiteSpace(Name) || Name == ".";
                case SegmentType.ParentSegment:
                    return string.IsNullOrWhiteSpace(Name) || Name == "..";
                case SegmentType.VolumeRelativeSegment:
                case SegmentType.VolumelessRootSegment:
                case SegmentType.DeviceSegment:
                    return false;
                default:
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
                    break;
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
