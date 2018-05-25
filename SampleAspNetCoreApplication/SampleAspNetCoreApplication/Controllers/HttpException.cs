using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace SampleAspNetCoreApplication.Controllers
{
    [Serializable]
    internal class HttpException : Exception
    {
        private HttpResponseMessage message;

        public HttpException()
        {
        }

        public HttpException(HttpResponseMessage message)
        {
            this.message = message;
        }

        public HttpException(string message) : base(message)
        {
        }

        public HttpException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}