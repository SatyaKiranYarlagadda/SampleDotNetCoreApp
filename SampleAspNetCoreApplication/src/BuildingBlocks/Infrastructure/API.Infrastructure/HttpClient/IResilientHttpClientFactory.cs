using BuildingBlocks.Resilience.Http;
using System;

namespace API.Infrastructure.HttpClient
{
    public interface IResilientHttpClientFactory
    {
        ResilientHttpClient CreateResilientHttpClient();
    }
}