
// File copy from Landorphan.Common
// ReSharper disable once CheckNamespace
namespace Landorphan.Common.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using System.Security;
    using Landorphan.Common.Resources;

    /// <summary>
    /// The exception that is thrown when a collection containing an invalid value is passed to a method that rejects the collection as a valid argument.
    /// </summary>
    [Serializable]
    public sealed class ArgumentContainsInvalidValueException : LandorphanArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentContainsNullException"/> class.
        /// </summary>
        public ArgumentContainsInvalidValueException() : this(null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentContainsNullException"/> class with the name of the parameter that gave rise to this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for this exception.
        /// </param>
        public ArgumentContainsInvalidValueException(string message) : this(null, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentContainsNullException"/> class with the inner exception that gave rise to this exception.
        /// </summary>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public ArgumentContainsInvalidValueException(Exception innerException) : this(null, null, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentContainsNullException"/> class with a specified error message and the exception gave rise to this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for this exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public ArgumentContainsInvalidValueException(string message, Exception innerException) : this(null, message, innerException)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ArgumentContainsNullException"/> class with a specified error message and the name of the parameter that gave rise to this exception.
        /// </summary>
        /// <param name="paramName">
        /// The name of the parameter that caused the exception.
        /// </param>
        /// <param name="message">
        /// The error message that explains the reason for this exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        [SuppressMessage("SonarLint.Security", "S5766: Deserializing objects without performing data validation is security-sensitive")]
        public ArgumentContainsInvalidValueException(string paramName, string message, Exception innerException)
            : base(paramName, message ?? StringResources.ArgumentContainsInvalidValueExceptionDefaultMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentContainsNullException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The Object that holds the serialized Object data.
        /// </param>
        /// <param name="context">
        /// A value type that describes the source or destination of the serialized data.
        /// </param>
        [SecurityCritical]
        private ArgumentContainsInvalidValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
