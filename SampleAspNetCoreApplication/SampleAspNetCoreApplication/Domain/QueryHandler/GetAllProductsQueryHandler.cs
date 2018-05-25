using API.Infrastructure.Domain.Query;
using SampleAspNetCoreApplication.Domain.Queries;
using SampleAspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAspNetCoreApplication.Domain.QueryHandler
{
    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        public IEnumerable<Product> Get()
        {
            return new List<Product>
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
        }
    }
}
