using System.Threading.Tasks;

namespace Booking.Infrastructure.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<CommandResult> ExecuteAsync(TCommand command);
    }
}
