using System;
using System.Collections.Generic;
using System.Text;

namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Posix
{
    internal static class PosixRelevantPathChars
    {
        public const char Null                   =            (char) 0x00;
        public const char Period                 = '.';    // (char) 0X2E;
        public const char ForwardSlash           = '/';    // (char) 0x2F;
        public static readonly char[] NonPrintableCharacters = 
        {
            Null
        };
        public static readonly char[] AlwaysIllegalCharacters = 
        {
            Null, ForwardSlash
        };

    }
}
