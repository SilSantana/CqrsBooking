using System;
using System.Threading.Tasks;

namespace Booking.Infrastructure.CQRS.Commands
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly DependencyResolver _dependencyResolver;

        public CommandDispatcher(DependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public async Task<CommandResult> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentException(nameof(command));

            var handler = _dependencyResolver.Resolve<ICommandHandler<TCommand>>();
            return await handler.ExecuteAsync(command);
        }
    }
}
