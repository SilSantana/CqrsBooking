using Booking.Infrastructure.CQRS.Events;

namespace Booking.Reservation.Application.Domain.Events
{
    internal class ReservationEventBaseV1 : EventBase
    {
        public ReservationEventBaseV1(string eventName)  : base(eventName, "1.0")
        {
        }

        public string PartitionKey() => this.EventId.ToString();

    }
}
