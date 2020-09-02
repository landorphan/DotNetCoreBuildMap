namespace Landorphan.BuildMap.Model.Support
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class StringList : List<string>
    {
        public StringList()
        {
        }

        public StringList(ICollection<string> strings) : base(strings)
        {
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            var item = 0;
            foreach (var str in this)
            {
                if (item++ > 0)
                {
                    output.Append(';');
                }

                output.Append(str);
            }

            return output.ToString();
        }

        public static implicit operator StringList(string[] strings)
        {
            return new StringList(strings);
        }
    }
}
