using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths
{
    public abstract class Segment : ISegment
    {
        public SegmentType SegmentType { get; protected set; }
        public String Name { get; protected set; }
        public ISegment NextSegment { get; set; }
        public abstract bool IsLegal();
        public string NameWithoutExtension {
            get
            {
                int loc = Name.LastIndexOf('.');
                if (loc >= 0 && loc < Name.Length)
                {
                    return Name.Substring(0, loc);
                }

                return Name;
            }
        }

        public abstract ISegment Clone();
    }
}
