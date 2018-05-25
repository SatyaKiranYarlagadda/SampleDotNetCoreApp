using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Infrastructure.Exceptions;
using API.Infrastructure.FaultTolerance;
using BuildingBlocks.Resilience.Http;
using CorrelationId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SampleAspNetCoreApplication.Configuration;
using SampleAspNetCoreApplication.Exceptions;
using SampleAspNetCoreApplication.Models;

namespace SampleAspNetCoreApplication.Controllers
{
    [Route("api/[controller]")]
    public class SamplesController : Controller
    {
        private ACMEConfig _acmeConfig { get; }
        private TestAppicationSettings _testSettings { get; }
        private readonly ICorrelationContextAccessor _correlationContext;
        private readonly IHttpClient _httpClient;
        private readonly IRetryableOperation _retryableOperation;

        public SamplesController(IOptions<ACMEConfig> acmeConfig, IOptions<TestAppicationSettings> testSettings,
            ICorrelationContextAccessor correlationContext, IHttpClient httpClient, IRetryableOperation retryableOperation)
        {
            _acmeConfig = acmeConfig.Value;
            _testSettings = testSettings.Value;
            _correlationContext = correlationContext;
            _httpClient = httpClient;
            _retryableOperation = retryableOperation;
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

        [HttpPost("RetryOperation")]
        public string PerformRetryOperation()
        {
            return _retryableOperation.TryExecute<string>(() =>
            {
                Random rnd = new Random();
                var result = rnd.Next(1, 10) % 2;
                if (result == 0)
                    return "Lucky!! Even number this time";
                else if (rnd.Next(1, 5) == 3)
                {
                    throw new Exception("Sorry can't get an even, try it again...");
                }
                else
                    throw new DomainException("This is Domain Exception");
            },
            (exception) => 
            {
                if (exception is DomainException)
                    return true;
                return false;
            });
        }

        // POST api/samples
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddSampleRequest request)
        {
            var path = _testSettings.Endpoint + "api/values";
            var result = await _httpClient.GetStringAsync(path);
            return Ok(result);
        }        
    }
}
