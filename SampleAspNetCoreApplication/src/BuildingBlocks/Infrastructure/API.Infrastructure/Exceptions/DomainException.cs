using System;
using System.Net;
using System.Runtime.Serialization;

namespace API.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    [Serializable]
    public class DomainException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.BadRequest;

        public DomainException()
        { }

        public DomainException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public DomainException(string message, Exception innerException, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }

        protected DomainException(SerializationInfo info, StreamingContext context, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            : base(info, context)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
