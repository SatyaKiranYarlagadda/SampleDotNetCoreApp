using API.Infrastructure.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SampleAspNetCoreApplication.Exceptions
{
    [Serializable]
    public class SampleNotFoundException : DomainException
    {
        public string SampleId { get; set; }

        public SampleNotFoundException()
        {        
        }

        public SampleNotFoundException(string sampleId)
            : base($"Sample with Id:{sampleId} is not found.", System.Net.HttpStatusCode.NotFound)
        {
            SampleId = sampleId;            
        }

        public SampleNotFoundException(string sampleId, string message, Exception innerException = null)
            : base(message, innerException, System.Net.HttpStatusCode.NotFound)
        {
            SampleId = sampleId;
        }

        protected SampleNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context, System.Net.HttpStatusCode.NotFound)
        {
            SampleId = info.GetString("SampleId");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("SampleId", SampleId);
            base.GetObjectData(info, context);
        }
    }
}
