using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.HealthChecks;

namespace SampleAspNetCoreApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/HealthCheck")]
    public class HealthCheckController : Controller
    {
        private readonly IHealthCheckService _healthCheckService;

        public HealthCheckController(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            CompositeHealthCheckResult healthCheckResult = await _healthCheckService.CheckHealthAsync();

            bool somethingIsWrong = healthCheckResult.CheckStatus != CheckStatus.Healthy;

            if (somethingIsWrong)
            {
                var failedHealthCheckDescriptions = healthCheckResult.Results.Where(r => r.Value.CheckStatus != CheckStatus.Healthy)
                                                                     .Select(r => r.Value.Description)
                                                                     .ToList();

                return new JsonResult(new { Errors = failedHealthCheckDescriptions }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            return Ok("Healthy");
        }
    }
}