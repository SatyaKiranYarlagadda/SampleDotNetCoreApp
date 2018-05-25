using API.Infrastructure.Domain.Command;
using SampleAspNetCoreApplication.Domain.Commands;
using SampleAspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAspNetCoreApplication.Domain.CommandHandler
{
    public class SaveProductCommandHandler : ICommandHandler<SaveProductCommand, CommandResponse>
    {
        private readonly SaveProductCommand _command;

        public SaveProductCommandHandler(SaveProductCommand command)
        {
            _command = command;
        }

        public CommandResponse Execute()
        {
            var Products = new List<Product>();

            Products.Add(_command.Product);

            return new CommandResponse
            {
                Id = _command.Product.Id,
                Success = true
            };
        }
    }
}
