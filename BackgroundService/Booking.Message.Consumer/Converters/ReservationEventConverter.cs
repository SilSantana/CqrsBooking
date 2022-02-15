using System;
using Booking.Infrastructure.KafkaConsumer;
using Booking.Message.Consumer.Constants;
using Booking.Message.Consumer.Models.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Booking.Message.Consumer.Converters
{
    public class ReservationEventConverter : JsonCreationConverter<InternalEventBase>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        protected override InternalEventBase Create(Type objectType, JObject jObject)
        {
            switch (jObject["EventName"].ToString())
            {
                case ReservationEventNames.ReservationCreated:
                    return new ReservationCreatedMessage();

                default:
                    throw new ArgumentException(
                        message: "The event is not recognized as valid",
                        paramName: jObject["EventName"].ToString());
            }
        }

    }
}
