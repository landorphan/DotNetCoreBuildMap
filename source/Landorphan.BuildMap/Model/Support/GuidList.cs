using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.BuildMap.Model.Support
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

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            int item = 0;
            foreach (var guid in this)
            {
                if (item++ > 0)
                {
                    output.Append(';');
                }
                output.Append(guid);
            }

            return output.ToString();
        }
    }
}