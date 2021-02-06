using System;
using System.Runtime.Serialization;

namespace FoodVault.Framework.Domain
{
    /// <summary>
    /// Basic domain layer exception. Occurs only at domain layer.
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        public DomainException() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        /// <param name="message"></param>
        public DomainException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        /// <param name="message">Exceptions message.</param>
        /// <param name="innerException">Inner exception for further details.</param>
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        /// <param name="serializationInfo">Infos for serialization</param>
        /// <param name="streamingContext">Context to stream from.</param>
        public DomainException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {

        }
    }
}
