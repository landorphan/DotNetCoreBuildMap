namespace Landorphan.Abstractions.FileSystem.Paths.Internal.Windows
{
    internal static class WindowsRelevantPathCharacters
    {
        public const char Null = (char)0x00;
        public const char StartOfHeader = (char)0x01;
        public const char StartOfText = (char)0x02;
        public const char EndOfText = (char)0x03;
        public const char EndOfTransmission = (char)0x04;
        public const char Enquiry = (char)0x05;
        public const char Acknowledge = (char)0x06;
        public const char Bell = (char)0x07;
        public const char Backspace = (char)0x08;
        public const char HorizontalTabulation = (char)0x09;
        public const char LineFeed = (char)0x0A;
        public const char VerticalTabulation = (char)0x0B;
        public const char FormFeed = (char)0x0C;
        public const char CarriageReturn = (char)0x0D;
        public const char ShiftOut = (char)0x0E;
        public const char ShiftIn = (char)0x0F;
        public const char DataLinkEscape = (char)0x10;
        public const char DeviceControl1 = (char)0x11;
        public const char DeviceControl2 = (char)0x12;
        public const char DeviceControl3 = (char)0x13;
        public const char DeviceControl4 = (char)0x14;
        public const char NegativeAcknowledge = (char)0x15;
        public const char SynchronousIdle = (char)0x16;
        public const char EndOfTransmissionBlock = (char)0x17;
        public const char Cancel = (char)0x18;
        public const char EndOfMedium = (char)0x19;
        public const char Substitute = (char)0x1A;
        public const char Escape = (char)0x1B;
        public const char FileSeparator = (char)0x1C;
        public const char GroupSeparator = (char)0x1D;
        public const char RecordSeparator = (char)0x1E;
        public const char UnitSeparator = (char)0x1F;
        public const char Space = ' '; // (char) 0x20;
        public const char DoubleQuote = '"'; // (char) 0x22;
        public const char Asterisk = '*'; // (char) 0x2A;
        public const char Period = '.'; // (char) 0X2E;
        public const char ForwardSlash = '/'; // (char) 0x2F;
        public const char Colon = ':'; // (char) 0x3A;
        public const char LessThanSign = '<'; // (char) 0x3C;
        public const char GreaterThanSign = '>'; // (char) 0x3E;
        public const char QuestionMark = '?'; // (char) 0x3F;
        public const char BackSlash = '\\'; // (char) 0x5C;
        public const char VerticalBar = '|'; // (char) 0x7C;
        public static readonly char[] NonPrintableCharacters =
        {
            Null, StartOfHeader, StartOfText, EndOfText, EndOfTransmission, Enquiry, Acknowledge, Bell, Backspace, HorizontalTabulation,
            LineFeed, VerticalTabulation, FormFeed, CarriageReturn, ShiftOut, ShiftIn, DataLinkEscape, DeviceControl1, DeviceControl2, DeviceControl3,
            DeviceControl4, NegativeAcknowledge, SynchronousIdle, EndOfTransmissionBlock, Cancel, EndOfMedium, Substitute, Escape, FileSeparator,
            GroupSeparator, RecordSeparator, UnitSeparator
        };
        public static readonly char[] AlwaysIllegalCharacters =
        {
            Null, StartOfHeader, StartOfText, EndOfText, EndOfTransmission, Enquiry, Acknowledge, Bell, Backspace, HorizontalTabulation,
            LineFeed, VerticalTabulation, FormFeed, CarriageReturn, ShiftOut, ShiftIn, DataLinkEscape, DeviceControl1, DeviceControl2, DeviceControl3,
            DeviceControl4, NegativeAcknowledge, SynchronousIdle, EndOfTransmissionBlock, Cancel, EndOfMedium, Substitute, Escape, FileSeparator,
            GroupSeparator, RecordSeparator, UnitSeparator, DoubleQuote, Asterisk, ForwardSlash, LessThanSign, GreaterThanSign, BackSlash, VerticalBar
        };
        public static readonly char[] IllegalAfterFirstSegment =
        {
            Colon, QuestionMark
        };
    }
}
