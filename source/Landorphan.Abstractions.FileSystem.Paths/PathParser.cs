using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.NIO.Paths
{
   using System.Linq;
   using System.Runtime.InteropServices;
   using Landorphan.Abstractions.NIO.Paths.Internal;

   public class PathParser : IPathParser
   {
      public IPath Parse(String pathString, OSPlatform platform)
      {
         var tokenizer = GetTokenizer(pathString, platform);
         var tokens = tokenizer.GetTokens();
         var rawSegments = this.GetSegments(tokens, platform);
         return ParsedPath.CreateFromSegments(pathString, rawSegments);
         return null;
      }

      // TODO: Make this internal once we have enough of the build system to use InternalsVisibleTo
      public WindowsSegment[] GetSegments(string [] tokens, OSPlatform platform)
      {
         if (OSPlatform.Windows == platform)
         {
            return GetWindowsSegments(tokens);
         }
         else
         {
            return GetPosixSegments(tokens);
         }
      }

      internal WindowsSegment[] GetPosixSegments(string[] tokens)
      {
         throw new NotImplementedException();
      }

      internal WindowsSegment[] GetWindowsSegments(string[] tokens)
      {
         IList<WindowsSegment> segments = new List<WindowsSegment>();

         if (tokens.Length == 0)
         {
            segments.Add(WindowsSegment.NullSegment);
            return segments.ToArray();
         }
         for (int i = 0; i<tokens.Length; i++)
         {
            if (i == 0)
            {
               if (tokens[i].StartsWith("UNC:"))
               {
                  segments.Add(new WindowsSegment(SegmentType.UncSegment, tokens[i].Substring(4)));
               }
               else if (tokens[i].Contains(":"))
               {
                  var parts = tokens[i].Split(':');
                  if (parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1]))
                  {
                     segments.Add(new WindowsSegment(SegmentType.VolumeRelativeSegment, parts[0] + ":"));
                     segments.Add(WindowsSegment.ParseFromString(parts[1]));
                  }
                  else
                  {
                     if (WindowsSegment.IsDeviceSegment(parts[0]))
                     {
                        segments.Add(new WindowsSegment(SegmentType.DeviceSegment, parts[0]));
                     }
                     else
                     {
                        segments.Add(new WindowsSegment(SegmentType.RootSegment, parts[0] + ":"));
                     }
                  }
               }
               else if (tokens.Length == 1)
               {
                  if (tokens[i] == null)
                  {
                     segments.Add(WindowsSegment.NullSegment);
                  }
                  if (tokens[i] == string.Empty)
                  {
                     segments.Add(WindowsSegment.EmptySegment);
                  }
                  else
                  {
                     segments.Add(WindowsSegment.ParseFromString(tokens[i]));
                  }
               }
               else if (tokens[i] == string.Empty)
               {
                  segments.Add(WindowsSegment.EmptySegment);
                  var name = tokens[++i];
                  segments.Add(new WindowsSegment(SegmentType.VolumelessRootSegment, name));
               }
               else
               {
                  segments.Add(WindowsSegment.ParseFromString(tokens[i]));
               }
            }
            else if (i >= 1)
            {
               segments.Add(WindowsSegment.ParseFromString(tokens[i]));
            }
         }

         return segments.ToArray();
      }

      private PathTokenizer GetTokenizer(string pathString, OSPlatform platform)
      {
         if (platform == OSPlatform.Windows)
         {
            return new WindowsPathTokenizer(pathString);
         }

         return null;
      }
   }
}
