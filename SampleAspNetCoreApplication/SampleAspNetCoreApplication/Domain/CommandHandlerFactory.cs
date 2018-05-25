using API.Infrastructure.Domain.Command;
using SampleAspNetCoreApplication.Domain.CommandHandler;
using SampleAspNetCoreApplication.Domain.Commands;

namespace SampleAspNetCoreApplication.Domain
{
    public static class CommandHandlerFactory
    {
        public static ICommandHandler<SaveProductCommand, CommandResponse> Build(SaveProductCommand command)
        {
            return new SaveProductCommandHandler(command);
        }
    }
}
