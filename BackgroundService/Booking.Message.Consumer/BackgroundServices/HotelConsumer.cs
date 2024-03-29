﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Booking.Infrastructure.KafkaConsumer;
using Booking.Message.Consumer.Converters;
using Booking.Message.Consumer.Models.Events;
using Booking.Message.Consumer.Repository;
using Booking.Message.Consumer.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Booking.Message.Consumer.Extensions;

namespace Booking.Message.Consumer.BackgroundServices
{
    internal class HotelConsumer : BackgroundService
    {
        private readonly HotelConsumerSettings _hotelConsumerSettings;
        private readonly HotelPersistenceSynchronizer _hotelPersistenceSynchronizer;

        public HotelConsumer(
            HotelPersistenceSynchronizer hotelPersistenceSynchronizer,
            IOptions<HotelConsumerSettings> options)
        {
            _hotelConsumerSettings = options.Value;
            _hotelPersistenceSynchronizer = hotelPersistenceSynchronizer;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ConsumeHotelMessage();

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }

        private async Task ConsumeHotelMessage()
        {
            var consumer = new KafkaConsumer<string, InternalEventBase>(_hotelConsumerSettings.GroupName,
                                                                        _hotelConsumerSettings.Server,
                                                                        _hotelConsumerSettings.TopicName,
                                                                        new HotelEventConverter())
            {
                OnConsumingAsync = OnHotelConsumingAsync
            };

            var cancellationToken = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancellationToken.Cancel();
            };

            await consumer.ConsumeAsync(cancellationToken);
        }

        private async Task OnHotelConsumingAsync(InternalEventBase hotelEventMessage)
        {
            switch (hotelEventMessage)
            {
                case HotelCreatedMessage hotelMessage:
                    Console.WriteLine($"The processed event was {nameof(hotelMessage)}");
                    await _hotelPersistenceSynchronizer.SynchronizeHotelData(hotelMessage.ParserTo());
                    return;

                case HotelAddressChangedMessage hotelAddressMessage:
                    Console.WriteLine($"The processed event was {nameof(hotelAddressMessage)}");
                    await _hotelPersistenceSynchronizer.SynchronizeHotelAddressData(hotelAddressMessage.ParserTo());
                    return;

                case HotelContactsChangedMessage hotelContactsMessage:
                    Console.WriteLine($"The processed event was {nameof(hotelContactsMessage)}");
                    await _hotelPersistenceSynchronizer.SynchronizeHotelContactsData(hotelContactsMessage.ParserTo());
                    return;

                case RoomAddedMessage roomMessage:
                    Console.WriteLine($"The processed event was {nameof(roomMessage)}");
                    await _hotelPersistenceSynchronizer.SynchronizeRoomData(roomMessage.ParserTo());
                    return;

                default:
                    throw new ArgumentException(
                        message: "Event is not a recognized as valid",
                        paramName: nameof(hotelEventMessage));
            }
        }

    }
}
