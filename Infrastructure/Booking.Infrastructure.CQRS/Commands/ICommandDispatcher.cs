using System.Threading.Tasks;

namespace Booking.Infrastructure.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task<CommandResult> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;

    }
}
