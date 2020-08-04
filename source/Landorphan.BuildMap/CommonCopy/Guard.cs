
// File copy from Landorphan.Common
// ReSharper disable once CheckNamespace
namespace Landorphan.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Landorphan.Common.Exceptions;
    using Landorphan.Common.Resources;

    /// <summary>
    /// Defines extension methods for performing validation of argument values in many common scenarios.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="value"/> is not greater than <paramref name="comparand"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the values to compare.
        /// </typeparam>
        /// <param name="value">
        /// The value to inspect.
        /// </param>
        /// <param name="comparand">
        /// The comparand.
        /// </param>
        /// <param name="paramName">
        /// The parameter name.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is not greater than <paramref name="comparand"/>.
        /// </exception>
        public static void ArgumentGreaterThan<T>(this T value, T comparand, string paramName) where T : struct, IComparable<T>
        {
            if (value.CompareTo(comparand) > 0)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                string.Format(CultureInfo.InvariantCulture, StringResources.ValueMustBeGreaterThanFmt, comparand, value));
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="value"/> is not greater than or equal to
        /// <paramref name="comparand"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the values to compare.
        /// </typeparam>
        /// <param name="value">
        /// The value to inspect.
        /// </param>
        /// <param name="comparand">
        /// The comparand.
        /// </param>
        /// <param name="paramName">
        /// The parameter name.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is not greater than or equal to <paramref name="comparand"/>.
        /// </exception>
        public static void ArgumentGreaterThanOrEqualTo<T>(this T value, T comparand, string paramName) where T : struct, IComparable<T>
        {
            if (value.CompareTo(comparand) >= 0)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                string.Format(CultureInfo.InvariantCulture, StringResources.ValueMustBeGreaterThanOrEqualToFmt, comparand, value));
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="value"/> is not less than <paramref name="comparand"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the values to compare.
        /// </typeparam>
        /// <param name="value">
        /// The value to inspect.
        /// </param>
        /// <param name="comparand">
        /// The comparand.
        /// </param>
        /// <param name="paramName">
        /// The parameter name.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is not less than <paramref name="comparand"/>.
        /// </exception>
        public static void ArgumentLessThan<T>(this T value, T comparand, string paramName) where T : struct, IComparable<T>
        {
            if (value.CompareTo(comparand) < 0)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                string.Format(CultureInfo.InvariantCulture, StringResources.ValueMustBeLessThanFmt, comparand, value));
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="value"/> is not less than or equal to
        /// <paramref name="comparand"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the values to compare.
        /// </typeparam>
        /// <param name="value">
        /// The value to inspect.
        /// </param>
        /// <param name="comparand">
        /// The comparand.
        /// </param>
        /// <param name="paramName">
        /// The parameter name.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is not less than or equal to <paramref name="comparand"/>.
        /// </exception>
        public static void ArgumentLessThanOrEqualTo<T>(this T value, T comparand, string paramName) where T : struct, IComparable<T>
        {
            if (value.CompareTo(comparand) <= 0)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                string.Format(CultureInfo.InvariantCulture, StringResources.ValueMustBeLessThanOrEqualToFmt, comparand, value));
        }

        /// <summary>
        /// Throws <see cref="ArgumentEmptyException"/> when the given <paramref name="value"/> is empty, and
        /// <see cref="ArgumentEmptyException"/> when the given <paramref name="value"/> is entirely composed of white-space.
        /// </summary>
        /// <param name="value">
        /// The string to inspect.
        /// </param>
        /// <param name="paramName">
        /// The parameter name.
        /// </param>
        /// <remarks>
        /// Allows <c> null </c> values.
        /// </remarks>
        /// <exception cref="ArgumentEmptyException">
        /// Thrown when the given <paramref name="value"/> is empty
        /// </exception>
        /// <exception cref="ArgumentEmptyException">
        /// Thrown when the given <paramref name="value"/> is entirely composed of white-space.
        /// </exception>
        public static void ArgumentNotEmptyNorWhiteSpace(this string value, string paramName)
        {
            if (ReferenceEquals(value, null))
            {
                return;
            }

            if (value.Length == 0)
            {
                throw new ArgumentEmptyException(paramName ?? string.Empty, null, null);
            }

            if (value.Trim().Length == 0)
            {
                throw new ArgumentWhiteSpaceException(paramName ?? string.Empty, null, null);
            }
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when <paramref name="value"/> is a null reference.
        /// </summary>
        /// <param name="value">
        /// The value to inspect.
        /// </param>
        /// <param name="argumentName">
        /// The argumentName.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is a null reference.
        /// </exception>
        [SuppressMessage("SonarLint.CodeSmell", "S4225: Extension methods should not extend object")]
        [DebuggerStepThrough]
        public static void ArgumentNotNull([ValidatedNotNull] this object value, string argumentName)
        {
            if (ReferenceEquals(value, null))
            {
                throw new ArgumentNullException(argumentName ?? string.Empty);
            }
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when the given <paramref name="value"/> is a null reference, and
        /// <see cref="ArgumentContainsNullException"/> when the given <paramref name="value"/> contains a null reference.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the values to compare.
        /// </typeparam>
        /// <param name="value">
        /// The collection to inspect.
        /// </param>
        /// <param name="paramName">
        /// The parameter name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is a null reference.
        /// </exception>
        /// <exception cref="ArgumentContainsNullException">
        /// Thrown when <paramref name="value"/> contains a null reference.
        /// </exception>
        public static void ArgumentNotNullNorContainsNull<T>([ValidatedNotNull] this IEnumerable<T> value, string paramName) where T : class
        {
            value.ArgumentNotNull(paramName);

            if ((from v in value where ReferenceEquals(v, null) select v).Any())
            {
                throw new ArgumentContainsNullException(paramName ?? string.Empty, null, null);
            }
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when the given <paramref name="value"/> is a null reference, and
        /// <see cref="ArgumentEmptyException"/> when the given <paramref name="value"/> is empty.
        /// </summary>
        /// <param name="value">
        /// The collection to inspect.
        /// </param>
        /// <param name="paramName">
        /// The parameter name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is a null reference.
        /// </exception>
        /// <exception cref="ArgumentEmptyException">
        /// Thrown when <paramref name="value"/> is empty.
        /// </exception>
        public static void ArgumentNotNullNorEmpty([ValidatedNotNull] this IEnumerable value, string paramName)
        {
            value.ArgumentNotNull(paramName);
            if (!value.Cast<object>().Any())
            {
                throw new ArgumentEmptyException(paramName ?? string.Empty, null, null);
            }
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when the given <paramref name="value"/> is a null reference, and
        /// <see cref="ArgumentEmptyException"/> when the given <paramref name="value"/> is empty, and
        /// <see cref="ArgumentWhiteSpaceException"/> when the given <paramref name="value"/> is entirely composed of white-space.
        /// </summary>
        /// <param name="value">
        /// The string to inspect.
        /// </param>
        /// <param name="paramName">
        /// The parameter name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is a null reference.
        /// </exception>
        /// <exception cref="ArgumentEmptyException">
        /// Thrown when <paramref name="value"/> is empty.
        /// </exception>
        /// <exception cref="ArgumentWhiteSpaceException">
        /// Thrown when <paramref name="value"/> is entirely composed of white-space.
        /// </exception>
        public static void ArgumentNotNullNorEmptyNorWhiteSpace([ValidatedNotNull] this string value, string paramName)
        {
            value.ArgumentNotNullNorEmpty(paramName);
            if (value.Trim().Length == 0)
            {
                throw new ArgumentWhiteSpaceException(paramName ?? string.Empty, null, null);
            }
        }
    }
}
