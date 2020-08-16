using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths
{
    using System.Security.Cryptography.X509Certificates;
    using Landorphan.Abstractions.FileSystem.Paths.Internal.Windows;

    public abstract class Segment : ISegment
    {
        public SegmentType SegmentType { get; protected set; }
        public String Name { get; internal set; }
        public ISegment NextSegment { get; internal set; }
        public ISegment LastSegment { get; internal set;  }

        public bool IsRootSegment => SegmentType == SegmentType.RootSegment ||
                                     SegmentType == SegmentType.RemoteSegment ||
                                     SegmentType == SegmentType.VolumelessRootSegment;
        public abstract bool IsLegal();
        public string NameWithoutExtension {
            get
            {
                int loc = Name.LastIndexOf(WindowsRelevantPathCharacters.Period);
                if (loc >= 1 && loc < Name.Length)
                {
                    return Name.Substring(0, loc);
                }

                return Name;
            }
        }

        public string Extension
        {
            get
            {
                string retval = string.Empty;
                int loc = Name.LastIndexOf(WindowsRelevantPathCharacters.Period);
                if (loc >= 1 && loc < Name.Length-1)
                {
                    retval = Name.Substring(loc);
                }

                return retval;
            }
        }

        public bool HasExtension {
            get
            {
                return Extension?.Length > 0;
            }
        }

        public abstract ISegment Clone();
    }
}
