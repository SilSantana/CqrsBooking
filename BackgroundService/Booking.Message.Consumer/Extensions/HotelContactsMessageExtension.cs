using Booking.Message.Consumer.Models;
using Booking.Message.Consumer.Models.Events;

namespace Booking.Message.Consumer.Extensions
{
    internal static class HotelContactsMessageExtension
    {
        internal static HotelContactsData ParserTo(this HotelContactsChangedMessage hotelContactsMessage)
        {
            return new HotelContactsData
            {
                HotelCode = hotelContactsMessage.HotelCode,
                Email = hotelContactsMessage.Email,
                Phone = hotelContactsMessage.Phone,
                Mobile = hotelContactsMessage.Mobile
            };
        }
    }
}
