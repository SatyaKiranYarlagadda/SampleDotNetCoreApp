using API.Infrastructure.Domain.Query;
using SampleAspNetCoreApplication.Domain.Queries;
using SampleAspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleAspNetCoreApplication.Domain.QueryHandler
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, Product>
    {
        private readonly GetProductByIdQuery _query;

        public GetProductByIdQueryHandler(GetProductByIdQuery query)
        {
            _query = query;
        }

        public Product Get()
        {
            #region data
            var products = new List<Product>
            {
                new Product
                {
                    Id = "1",
                    Name = "Apple",
                    Quantity = 100,
                    Price = 2
                },
                new Product
                {
                    Id = "2",
                    Name = "Orange",
                    Quantity = 200,
                    Price = 3
                },
                new Product
                {
                    Id = "3",
                    Name = "Pears",
                    Quantity = 300,
                    Price = 1
                },
            };
            #endregion

            return products.FirstOrDefault(x => x.Id.Equals(_query.Id, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
