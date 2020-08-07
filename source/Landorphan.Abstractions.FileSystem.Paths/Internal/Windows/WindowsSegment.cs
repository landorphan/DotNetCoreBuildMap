using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.NIO.Paths.Internal
{
   using System.Linq;

   // TODO: Make this internal once we have enough build system to do InternalsVisibleTo
   public class WindowsSegment : ISegment
   {
      public static readonly string[] DeviceNames = new String[]
      {
         "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8",
         "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
      };
      public static readonly WindowsSegment NullSegment = new WindowsSegment(SegmentType.NullSegment, null);
      public static readonly WindowsSegment EmptySegment = new WindowsSegment(SegmentType.EmptySegment, string.Empty);
      public static readonly WindowsSegment SelfSegment = new WindowsSegment(SegmentType.SelfSegment, ".");
      public static readonly WindowsSegment ParentSegment = new WindowsSegment(SegmentType.ParentSegment, "..");

      public static WindowsSegment ParseFromString(string input)
      {
         if (input == ".")
         {
            return SelfSegment;
         }
         if (input == "..")
         {
            return ParentSegment;
         }
         if (input == null)
         {
            return NullSegment;
         }
         if (input.Length == 0)
         {
            return EmptySegment;
         }
         else if (IsDeviceSegment(input))
         {
            return new WindowsSegment(SegmentType.DeviceSegment, input);
         }
         else
         {
            return new WindowsSegment(SegmentType.GenericSegment, input);
         }
      }

      public static bool IsDeviceSegment(string input)
      {
         return DeviceNames.Contains(input);
      }

      public WindowsSegment(SegmentType type, string name)
      {
         this.SegmentType = type;
         this.Name = name;
      }

      public SegmentType SegmentType { get; protected set; }
      public String Name { get; protected set; }
      public ISegment NextSegment { get; set; }
   }
}
