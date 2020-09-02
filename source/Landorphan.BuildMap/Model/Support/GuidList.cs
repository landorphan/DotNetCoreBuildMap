namespace Landorphan.BuildMap.Model.Support
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class GuidList : List<Guid>
    {
        public GuidList()
        {
        }

        public GuidList(ICollection<Guid> guids) : base(guids)
        {
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            var item = 0;
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

        public static implicit operator GuidList(Guid[] guids)
        {
            return new GuidList(guids);
        }
    }
}
