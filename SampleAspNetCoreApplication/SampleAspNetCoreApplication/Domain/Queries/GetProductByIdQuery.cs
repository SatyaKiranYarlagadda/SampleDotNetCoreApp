using API.Infrastructure.Domain.Query;
using SampleAspNetCoreApplication.Models;

namespace SampleAspNetCoreApplication.Domain.Queries
{
    public class GetProductByIdQuery : IQuery<Product>
    {
        public string Id { get; private set; }

        public GetProductByIdQuery(string id)
        {
            Guard.ArgumentNotNullOrEmpty(nameof(id), id);
            Id = id;
        }
    }
}
