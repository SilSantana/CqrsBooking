using Booking.Infrastructure.CQRS.Events;
using Booking.Infrastructure.KafkaProducer;
using Booking.Management.Application.Domain.Events;
using Booking.Management.Application.Settings;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Booking.Management.Application.Handlers.Events
{
    internal class HotelAddressUpdatedHandler : IEventHandler<HotelAddressUpdated>
    {
        private readonly ManagementProducerSettings managementProducerSettings;

        public HotelAddressUpdatedHandler(IOptions<ManagementProducerSettings> options)
        {
            managementProducerSettings = options.Value;
        }

        public async Task HandleAsync(HotelAddressUpdated @event)
        {
            using (var producer = new KafkaProducer<string, HotelAddressUpdated>(
                    this.managementProducerSettings.TopicName,
                    this.managementProducerSettings.Server))
            {
                await producer.ProduceMessage(@event, @event.PartitionKey());
            }
        }
    }
}
