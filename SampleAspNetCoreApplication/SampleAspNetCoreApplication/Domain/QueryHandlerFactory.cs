using API.Infrastructure.Domain.Query;
using SampleAspNetCoreApplication.Domain.Queries;
using SampleAspNetCoreApplication.Domain.QueryHandler;
using SampleAspNetCoreApplication.Models;
using System.Collections.Generic;

namespace SampleAspNetCoreApplication.Domain
{
    public static class QueryHandlerFactory
    {
        public static IQueryHandler<GetProductByIdQuery, Product> Build(GetProductByIdQuery query)
        {
            return new GetProductByIdQueryHandler(query);
        }

        public static IQueryHandler<GetAllProductsQuery, IEnumerable<Product>> Build(GetAllProductsQuery query)
        {
            return new GetAllProductsQueryHandler();
        }
    }
}
