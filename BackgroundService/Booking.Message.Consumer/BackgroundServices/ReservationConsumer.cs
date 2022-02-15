using System;
using System.Threading;
using System.Threading.Tasks;
using Booking.Infrastructure.KafkaConsumer;
using Booking.Message.Consumer.Converters;
using Booking.Message.Consumer.Extensions;
using Booking.Message.Consumer.Models.Events;
using Booking.Message.Consumer.Repository;
using Booking.Message.Consumer.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Booking.Message.Consumer.BackgroundServices
{
    internal class ReservationConsumer : BackgroundService
    {
        private readonly ReservationConsumerSettings _reservationConsumerSettings;
        private readonly ReservationPersistenceSynchronizer _reservationPersistenceSynchronizer;

        public ReservationConsumer(
            ReservationPersistenceSynchronizer reservationPersistenceSynchronizer,
            IOptions<ReservationConsumerSettings> options)
        {
            _reservationConsumerSettings = options.Value;
            _reservationPersistenceSynchronizer = reservationPersistenceSynchronizer;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ConsumeReservationMessage();

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }

        private async Task ConsumeReservationMessage()
        {
            var consumer = new KafkaConsumer<string, InternalEventBase>(_reservationConsumerSettings.GroupName,
                                                                        _reservationConsumerSettings.Server,
                                                                        _reservationConsumerSettings.TopicName,
                                                                        new ReservationEventConverter())
            {
                OnConsumingAsync = OnReservationConsumingAsync
            };

            var cancellationToken = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancellationToken.Cancel();
            };

            await consumer.ConsumeAsync(cancellationToken);
        }

        private async Task OnReservationConsumingAsync(InternalEventBase reservationEventMessage)
        {
            switch (reservationEventMessage)
            {
                case ReservationCreatedMessage reservationCreatedMessage:
                    Console.WriteLine($"The processed event was {nameof(reservationCreatedMessage)}");
                    await _reservationPersistenceSynchronizer.SynchronizeReservationData(reservationCreatedMessage.ParserTo());
                    return;

                default:
                    throw new ArgumentException(
                        message: "Event is not a recognized as valid",
                        paramName: nameof(reservationEventMessage));
            }
        }

    }
}
