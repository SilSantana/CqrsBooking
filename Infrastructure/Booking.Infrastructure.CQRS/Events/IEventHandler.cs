using System.Threading.Tasks;

namespace Booking.Infrastructure.CQRS.Events
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
