using System.Collections.Generic;
using System.Text;

namespace Landorphan.BuildMap.Model.Support
{
    public class StringList : List<string>
    {
        public StringList()
        {
        }

        public StringList(ICollection<string> strings) : base(strings)
        {
        } 
        
        public static implicit operator StringList(string[] strings)
        {
            return new StringList(strings);
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            int item = 0;
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
    }
}