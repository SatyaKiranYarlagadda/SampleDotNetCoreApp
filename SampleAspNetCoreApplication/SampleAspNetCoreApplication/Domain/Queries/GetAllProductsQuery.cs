using API.Infrastructure.Domain.Query;
using SampleAspNetCoreApplication.Models;
using System.Collections.Generic;

namespace SampleAspNetCoreApplication.Domain.Queries
{
    public class GetAllProductsQuery : IQuery<IEnumerable<Product>> { }
}
