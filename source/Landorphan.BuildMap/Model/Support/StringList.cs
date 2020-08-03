using System.Collections.Generic;

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
    }
}