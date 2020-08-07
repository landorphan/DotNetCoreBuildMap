using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.NIO.Paths.Internal
{
   using System.Linq;

   public abstract class PathTokenizer
   {
      private readonly string[] tokens;

      protected PathTokenizer(string path)
      {
         if (path == null)
         {
            tokens = Array.Empty<string>();
         }
         else
         {
            tokens = path.Split('/');
         }
      }

      public string[] GetTokens()
      {
         return tokens.ToArray();
      }
   }
}
