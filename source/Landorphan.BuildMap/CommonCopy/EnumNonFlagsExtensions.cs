
// File copy from Landorphan.Common
// ReSharper disable once CheckNamespace
namespace Landorphan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Landorphan.Common.Exceptions;

    /// <summary>
    /// Extension methods for working with <see cref="Enum"/> instances that do not have the <see cref="FlagsAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Generic constraints do not support 'enum', so restricting to struct, IComparable, IFormattable, IConvertible.  However, this still leaves the
    /// extension available to too many commonly used types, so placing these extensions in an isolated namespace.
    /// </remarks>
    public static class NonFlagsEnumExtensions
    {
        /// <summary>
        /// Validates a non-[Flags] enumeration argument value.
        /// </summary>
        /// <typeparam name="T">
        /// The enumeration type.
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
        /// when <typeparamref name="T"/> is attributed with [Flags].
        /// </exception>
        // Constraint type 'System.IConvertible' is not CLS-compliant
        public static void ArgumentMustBeValidEnumValue<T>(this T value, string parameterName)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (IsValidEnumValue(value))
            {
                return;
            }

            throw new ExtendedInvalidEnumArgumentException(parameterName, Convert.ToInt64(value, CultureInfo.InvariantCulture), typeof(T));
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
        public static bool IsValidEnumValue<T>(this T value) where T : struct, IComparable, IFormattable, IConvertible
        {
            var t = typeof(T);

            TypeMustBeNonFlagsEnumeration(t);

            unchecked
            {
                // Convert.To* was compiled with overflow checking turned on, must use casting in unchecked block to handle unsigned values.

                // BOXING/UNBOXING ISSUE here:  must first unbox the value as its base type, then cast it.
                var underlyingType = Enum.GetUnderlyingType(t);

                var allValues = new HashSet<long>();

                foreach (var v in Enum.GetValues(t))
                {
                    long convertedValue = 0;
                    if (underlyingType == typeof(sbyte))
                    {
                        convertedValue = (sbyte)v;
                    }
                    else if (underlyingType == typeof(byte))
                    {
                        convertedValue = (byte)v;
                    }
                    else if (underlyingType == typeof(short))
                    {
                        convertedValue = (short)v;
                    }
                    else if (underlyingType == typeof(ushort))
                    {
                        convertedValue = (ushort)v;
                    }
                    else if (underlyingType == typeof(int))
                    {
                        convertedValue = (int)v;
                    }
                    else if (underlyingType == typeof(uint))
                    {
                        convertedValue = (uint)v;
                    }
                    else if (underlyingType == typeof(long))
                    {
                        convertedValue = (long)v;
                    }
                    else if (underlyingType == typeof(ulong))
                    {
                        convertedValue = (long)(ulong)v;
                    }

                    allValues.Add(convertedValue);
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

                return allValues.Contains(castValue);
            }
        }

        private static void TypeMustBeNonFlagsEnumeration(Type t)
        {
            t.ArgumentNotNull(nameof(t));

            if (!t.IsEnum)
            {
                throw new InvalidOperationException(t.Name + " is not an enumeration type.");
            }

            if (t.IsDefined(typeof(FlagsAttribute), false))
            {
                throw new InvalidOperationException(t.Name + " is attributed with [Flags].");
            }
        }
    }
}
