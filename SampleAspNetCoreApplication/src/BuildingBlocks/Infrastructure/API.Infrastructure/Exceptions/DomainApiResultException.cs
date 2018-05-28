using System;
using System.Net;
using System.Runtime.Serialization;

namespace API.Infrastructure.Exceptions
{
    public class DomainApiResultException<T> : DomainException 
    {
        public T ExceptionDetails { get; set; }

        public DomainApiResultException(string message, HttpStatusCode httpStatusCode)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public DomainApiResultException(string message, Exception innerException, HttpStatusCode httpStatusCode)
            : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }

        protected DomainApiResultException(SerializationInfo info, StreamingContext context, HttpStatusCode httpStatusCode)
            : base(info, context)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
