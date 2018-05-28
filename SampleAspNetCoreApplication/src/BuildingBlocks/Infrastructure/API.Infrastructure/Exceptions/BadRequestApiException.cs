using System;
using System.Runtime.Serialization;

namespace API.Infrastructure.Exceptions
{
    public class BadRequestApiException<T> : DomainApiResultException<T>
    {
        public BadRequestApiException(string message, T data)
            : base(message, System.Net.HttpStatusCode.BadRequest)
        {
            ExceptionDetails = data;
        }

        public BadRequestApiException(string message, Exception innerException, T data)
            : base(message, innerException, System.Net.HttpStatusCode.BadRequest)
        {
            ExceptionDetails = data;
        }

        protected BadRequestApiException(SerializationInfo info, StreamingContext context, T data)
            : base(info, context, System.Net.HttpStatusCode.BadRequest)
        {
            ExceptionDetails = data;
        }
    }
}
