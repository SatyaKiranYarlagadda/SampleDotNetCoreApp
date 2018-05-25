using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API.Infrastructure.Exceptions;
using API.Infrastructure.FaultTolerance;
using BuildingBlocks.Resilience.Http;
using CorrelationId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SampleAspNetCoreApplication.Configuration;
using SampleAspNetCoreApplication.Domain;
using SampleAspNetCoreApplication.Domain.Commands;
using SampleAspNetCoreApplication.Domain.Queries;
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

        [HttpGet("Products/{id}")]
        public Product GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var handler = QueryHandlerFactory.Build(query);
            return handler.Get();
        }

        [HttpPost("Products")]
        public IActionResult Post([FromBody]Product item)
        {
            var command = new SaveProductCommand(item);
            var handler = CommandHandlerFactory.Build(command);
            var response = handler.Execute();
            if (response.Success)
            {
                item.Id = response.Id;
                return Ok(item);
            }

            // an example of what might have gone wrong
            var message = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(response.Message),
                ReasonPhrase = "InternalServerError"
            };

            throw new HttpException(message);
        }
    }
}
