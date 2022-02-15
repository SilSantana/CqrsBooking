using Booking.Message.Consumer.Models;
using Booking.Message.Consumer.Models.Events;

namespace Booking.Message.Consumer.Extensions
{
    internal static class HotelAddressMessageExtension
    {
        internal static HotelAddressData ParserTo(this HotelAddressChangedMessage hotelAddressMessage)
        {
            return new HotelAddressData
            {
                HotelCode = hotelAddressMessage.HotelCode,
                Street = hotelAddressMessage.Street,
                District = hotelAddressMessage.District,
                City = hotelAddressMessage.City,
                Country = hotelAddressMessage.Country,
                ZipCode = hotelAddressMessage.ZipCode
            };
        }
    }
}
