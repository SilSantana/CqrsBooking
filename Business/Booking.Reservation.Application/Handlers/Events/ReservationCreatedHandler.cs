using Booking.Infrastructure.CQRS.Events;
using Booking.Infrastructure.KafkaProducer;
using Booking.Reservation.Application.Domain.Events;
using Booking.Reservation.Application.Settings;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Booking.Reservation.Application.Handlers.Events
{
    internal class ReservationCreatedHandler : IEventHandler<ReservationCreated>
    {
        private readonly ReservationProducerSettings reservationProducerSettings;

        public ReservationCreatedHandler(IOptions<ReservationProducerSettings> options)
        {
            reservationProducerSettings = options.Value;
        }

        public async Task HandleAsync(ReservationCreated @event)
        {
            using (var producer = new KafkaProducer<string, ReservationEventBaseV1>(
                   this.reservationProducerSettings.TopicName,
                   this.reservationProducerSettings.Server))
            {
                await producer.ProduceMessage(@event, @event.PartitionKey());
            }
        }
    }
}
