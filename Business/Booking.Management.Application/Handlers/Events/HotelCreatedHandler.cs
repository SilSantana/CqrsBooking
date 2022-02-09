using Booking.Infrastructure.CQRS.Events;
using Booking.Infrastructure.KafkaProducer;
using Booking.Management.Application.Domain.Events;
using Booking.Management.Application.Settings;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Booking.Management.Application.Handlers.Events
{
    internal class HotelCreatedHandler : IEventHandler<HotelCreated>
    {
        private readonly ManagementProducerSettings managementProducerSettings;

        public HotelCreatedHandler(IOptions<ManagementProducerSettings> options)
        {
            managementProducerSettings = options.Value;
        }

        public async Task HandleAsync(HotelCreated @event)
        {
            try
            {
                using (var producer = new KafkaProducer<string, HotelEventBaseV1>(
                            this.managementProducerSettings.TopicName,
                            this.managementProducerSettings.Server))
                {
                    await producer.ProduceMessage(@event, @event.PartitionKey());
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
