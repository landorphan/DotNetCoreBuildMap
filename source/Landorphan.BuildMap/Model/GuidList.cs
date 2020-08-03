using System;
using System.Collections.Generic;

namespace Landorphan.BuildMap.Model
{
    public class GuidList : List<Guid>
    {
        public GuidList()
        {
        }

        public GuidList(ICollection<Guid> guids) : base(guids)
        {
        }

        public static implicit operator GuidList(Guid[] guids)
        {
            return new GuidList(guids);
        }
    }
}