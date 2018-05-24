using API.Infrastructure.ActionResults;
using API.Infrastructure.Exceptions;
using CorrelationId;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace API.Infrastructure.Filters
{
    public partial class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger, ICorrelationContextAccessor correlationContext)
        {
            this.env = env;
            this.logger = logger;
            _correlationContext = correlationContext;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception is DomainException)
            {
                var exception = context.Exception as DomainException;
                var json = new JsonErrorResponse
                {
                    Messages = new[] { exception.Message, $"CorrelationId: {_correlationContext.CorrelationContext.CorrelationId}" }
                };

                context.Result = new ObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)exception.HttpStatusCode;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An error occurred. Try it again.", $"CorrelationId: {_correlationContext.CorrelationContext.CorrelationId}" }
                };

                //if (env.IsDevelopment())
                //{
                //    json.DeveloperMessage = context.Exception;
                //}

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }
}
