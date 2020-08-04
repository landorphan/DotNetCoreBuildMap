
// File copy from Landorphan.Common
// ReSharper disable once CheckNamespace
namespace Landorphan.Common
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Landorphan.Common.Exceptions;

    /// <summary>
    /// Extension methods for working with <see cref="Enum"/> instances that have the <see cref="FlagsAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Generic constraints do not support 'enum', so restricting to struct, IComparable, IFormattable, IConvertible.  However, this still
    /// leaves the extension available to too many commonly used types, so placing these extensions in an isolated namespace.
    /// </remarks>
    public static class FlagsEnumExtensions
    {
        /// <summary>
        /// Validates a [Flags] enumeration argument value.
        /// </summary>
        /// <typeparam name="T">
        /// The flags enumeration type.
        /// </typeparam>
        /// <param name="value">
        /// The value to be validated.
        /// </param>
        /// <param name="parameterName">
        /// The name of the parameter.
        /// </param>
        /// <exception cref="ExtendedInvalidEnumArgumentException">
        /// Thrown when the value is not valid.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <typeparamref name="T"/> is not an enumeration,
        /// -or-
        /// when <typeparamref name="T"/> is not attributed with [Flags].
        /// </exception>
        // Constraint type 'System.IConvertible' is not CLS-compliant
        public static void ArgumentMustBeValidFlagsEnumValue<T>(this T value, string parameterName)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (IsValidFlagsValue(value))
            {
                return;
            }

            // if reach here, T is an enum with [Flags] so it can be converted to a long value.
            throw new ExtendedInvalidEnumArgumentException(parameterName, Convert.ToInt64(value, CultureInfo.InvariantCulture), typeof(T));
        }

        /// <summary>
        /// Determines whether the given flag is set in the specified value.
        /// </summary>
        /// <typeparam name="T">
        /// The flags enumeration type.
        /// </typeparam>
        /// <param name="value">
        /// The value to inspect.
        /// </param>
        /// <param name="flag">
        /// The flag.
        /// </param>
        /// <returns>
        /// <c> true </c> when the flag is set; otherwise, <c> false </c>.
        /// </returns>
        public static bool IsFlagSet<T>(this T value, T flag) where T : struct, IComparable, IFormattable, IConvertible
        {
            var t = typeof(T);
            TypeMustBeFlagsEnumeration(t);

            var v = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
            var f = Convert.ToUInt64(flag, CultureInfo.InvariantCulture);

            return (v & f) == f;
        }

        /// <summary>
        /// Determines whether a the given value is a valid combination of flag values.
        /// </summary>
        /// <typeparam name="T">
        /// The flags enumeration type.
        /// </typeparam>
        /// <param name="value">
        /// The value to be validated.
        /// </param>
        /// <returns>
        /// <c> true </c> when <paramref name="value"/> is a valid combination of flag values; otherwise, <c> false </c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <typeparamref name="T"/> is not an enumeration,
        /// -or-
        /// when <typeparamref name="T"/> is not attributed with [Flags].
        /// </exception>
        // Constraint type 'System.IConvertible' is not CLS-compliant
        [SuppressMessage("SonarLint.CodeSmell", "S3776: Cognitive Complexity of methods should not be too high")]
        [SuppressMessage("SonarLint.CodeSmell", "S1541: Methods and properties should not be too complex")]
        [SuppressMessage("SonarLint.CodeSmell", "S138: Functions should not have too many lines of code")]
        public static bool IsValidFlagsValue<T>(this T value) where T : struct, IComparable, IFormattable, IConvertible
        {
            var t = typeof(T);

            TypeMustBeFlagsEnumeration(t);

            unchecked
            {
                // Convert.To* was compiled with overflow checking turned on, must use casting in unchecked block to handle unsigned values.

                // BOXING/UNBOXING ISSUE here:  must first unbox the value as its base type, then cast it.
                var underlyingType = Enum.GetUnderlyingType(t);

                long allValues = 0;
                foreach (var flag in Enum.GetValues(t))
                {
                    long convertedFlag = 0;
                    if (underlyingType == typeof(sbyte))
                    {
                        convertedFlag = (sbyte)flag;
                    }
                    else if (underlyingType == typeof(byte))
                    {
                        convertedFlag = (byte)flag;
                    }
                    else if (underlyingType == typeof(short))
                    {
                        convertedFlag = (short)flag;
                    }
                    else if (underlyingType == typeof(ushort))
                    {
                        convertedFlag = (ushort)flag;
                    }
                    else if (underlyingType == typeof(int))
                    {
                        convertedFlag = (int)flag;
                    }
                    else if (underlyingType == typeof(uint))
                    {
                        convertedFlag = (uint)flag;
                    }
                    else if (underlyingType == typeof(long))
                    {
                        convertedFlag = (long)flag;
                    }
                    else if (underlyingType == typeof(ulong))
                    {
                        convertedFlag = (long)(ulong)flag;
                    }

                    allValues |= convertedFlag;
                }

                long castValue = 0;
                if (underlyingType == typeof(sbyte))
                {
                    castValue = (sbyte)(object)value;
                }
                else if (underlyingType == typeof(byte))
                {
                    castValue = (byte)(object)value;
                }
                else if (underlyingType == typeof(short))
                {
                    castValue = (short)(object)value;
                }
                else if (underlyingType == typeof(ushort))
                {
                    castValue = (ushort)(object)value;
                }
                else if (underlyingType == typeof(int))
                {
                    castValue = (int)(object)value;
                }
                else if (underlyingType == typeof(uint))
                {
                    castValue = (uint)(object)value;
                }
                else if (underlyingType == typeof(long))
                {
                    castValue = (long)(object)value;
                }
                else if (underlyingType == typeof(ulong))
                {
                    castValue = (long)(ulong)(object)value;
                }

                // determine if any invalid bits are on.
                return (~allValues & castValue) == 0;
            }
        }

        private static void TypeMustBeFlagsEnumeration(Type t)
        {
            t.ArgumentNotNull(nameof(t));

            if (!t.IsEnum)
            {
                throw new InvalidOperationException(t.Name + " is not an enumeration type.");
            }

            if (!t.IsDefined(typeof(FlagsAttribute), false))
            {
                throw new InvalidOperationException(t.Name + " is not an attributed with [Flags].");
            }
        }
    }
}
