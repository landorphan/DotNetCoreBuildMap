
// File copy from Landorphan.Common
// ReSharper disable once CheckNamespace
namespace Landorphan.Common.Resources
{
    internal static class StringResources
    {
        internal const string ArgumentContainsInvalidValueExceptionDefaultMessage = "The value must not contain invalid values.";
        internal const string ArgumentContainsNullExceptionDefaultMessage = "The value must not contain null values.";
        internal const string ArgumentEmptyExceptionDefaultMessage = "The value must not be empty.";
        internal const string ArgumentWhitespaceExceptionDefaultMessage = "The value must not be composed entirely of white-space.";
        internal const string EventHandlerMustNotHaveNullMethodArgumentExceptionFmt =
            "The event handler argument '{0}' must not return a null method, but did.";
        internal const string EventHandlerMustNotHaveStaticMethodArgumentExceptionFmt =
            "The event handler argument '{0}' must not return a static method, but did.";
        internal const string ExtendedInvalidEnumArgumentExceptionMessageFmt =
            "The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.";
        internal const string InvalidLockTimeoutArgumentExceptionMessageFmt =
            @"The value of argument '{0}' ({1} ms) is invalid.  Timeout values must be between -1 (which represents ""never"") and {2} total milliseconds.";
        internal const string LockRecursionExceptionReadAfterUpgradeNotAllowed =
            "A read lock may not be acquired with the upgradeable read lock held in this mode.";
        internal const string NullReplacementValue = "null";
        internal const string ObjectMustBeOfTypeBooleanOrInterlockedBoolean = @"Object must be of type Boolean or of type InterlockedBoolean.";
        internal const string ObjectMustBeOfTypeTimespanOrInterlockedTimespan = @"Object must be of type Timespan or of type InterlockedTimespan.";
        internal const string ObjectMustBeOfTypeDateTimeOffsetOrInterlockedDateTimeOffset = @"Object must be of type DateTimeOffset or of type InterlockedDateTimeOffset.";
        internal const string ObjectMustBeOfTypeDateTimeOrInterlockedDateTime = @"Object must be of type DateTime or of type InterlockedDateTime.";

        internal const string PublicKey = "0024000004800000140100000602000000240000525341310008000001000100c33ba254dba2d79dba4c1fe136efb36588f73cc16afd66a43aebf7489ac82ab66c2fff42d8503459eff95139e0" +
                                          "ff790b6688513b19edf991760ef7ad02740a296c3ce5d098586b236b6abaa75038eb03af7d643d9a84b47bca0ed1cabbf4de9605c76007aab2e3abe7633d90860648ab1e38035fda5943971c64" +
                                          "71249a0bd80f2ab04686c2110ebb10f909fb773e3d87f67fb1e2f33ee791e4d8284fe9c6848ea81b3cf6a081df100716a10c68e0dd5219ff1657995777bf03961afdc3c09b040edb5a36baab50" +
                                          "75410507f3ba1d9f59c6bd67401819abbbe7712b5f473e052b96efe98c39210bb485c1ba0489ed396983beb914a3b2443f6aa2be4f49a88bb0";

        internal const string TheCurrentInstanceIsReadOnly = "The current instance is read-only.";
        internal const string TimeoutElapsedBeforeLockObtainedExceptionDefaultMessageFmt =
            "A lock was not obtained before the timeout elapsed ({0}).";
        internal const string ValueMustBeGreaterThanFmt = "The value must be greater than '{0}' but is '{1}'.";
        internal const string ValueMustBeGreaterThanOrEqualToFmt = "The value must be greater than or equal to '{0}' but is '{1}'.";
        internal const string ValueMustBeLessThanFmt = "The value must be less than '{0}' but is '{1}'.";
        internal const string ValueMustBeLessThanOrEqualToFmt = "The value must be less than or equal to '{0}' but is '{1}'.";
    }
}
