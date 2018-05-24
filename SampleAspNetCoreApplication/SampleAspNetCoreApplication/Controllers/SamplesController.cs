using System;
using System.Collections.Generic;
using CorrelationId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SampleAspNetCoreApplication.Configuration;
using SampleAspNetCoreApplication.Exceptions;

namespace SampleAspNetCoreApplication.Controllers
{
    [Route("api/[controller]")]
    public class SamplesController : Controller
    {
        private ACMEConfig _acmeConfig { get; }
        private readonly ICorrelationContextAccessor _correlationContext;

        public SamplesController(IOptions<ACMEConfig> acmeConfig, ICorrelationContextAccessor correlationContext)
        {
            _acmeConfig = acmeConfig.Value;
            _correlationContext = correlationContext;
        }

        // GET api/samples
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var result = new List<string>
            {
                _acmeConfig.Endpoint,
                _acmeConfig.UserName,
                _acmeConfig.Password
            };

            return result;
        }

        // GET api/samples/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            if(id == "1")
            {
                throw new InvalidOperationException("Throwing invalid exception.");
            }
            else if (id == "2")
            {
                throw new SampleNotFoundException(id);
            }

            return "All Good!!!";
        }

        [HttpGet("CorrelationContext")]
        public string GetCorrelationContext()
        {
            return _correlationContext.CorrelationContext.CorrelationId;
        }

        // POST api/samples
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/samples/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/samples/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
