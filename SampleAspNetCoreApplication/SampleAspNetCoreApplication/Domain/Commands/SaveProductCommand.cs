using API.Infrastructure.Domain.Command;
using SampleAspNetCoreApplication.Models;

namespace SampleAspNetCoreApplication.Domain.Commands
{
    public class SaveProductCommand : ICommand<CommandResponse>
    {
        public Product Product { get; private set; }

        public SaveProductCommand(Product item)
        {
            Guard.ArgumentNotNullOrEmpty(nameof(item.Id), item.Id);
            Guard.ArgumentNotNullOrEmpty(nameof(item.Name), item.Name);
            Guard.ArgumentValid(item.Price > 0, nameof(item.Price), item.Price.ToString());
            Guard.ArgumentValid(item.Quantity >= 0, nameof(item.Quantity), item.Quantity.ToString());

            Product = item;
        }
    }
}
