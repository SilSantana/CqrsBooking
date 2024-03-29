﻿using Booking.Message.Consumer.Models;
using Booking.Message.Consumer.Models.Events;

namespace Booking.Message.Consumer.Extensions
{
    internal static class HotelMessageExtension
    {
        internal static HotelData ParserTo(this HotelCreatedMessage hotelMessage)
        {
            return new HotelData
            {
                Code = hotelMessage.Code,
                Name = hotelMessage.Name,
                StarsOfCategory = hotelMessage.StarsOfCategory,
                StarsOfRating = hotelMessage.StarsOfRating,
                Street = hotelMessage.Street,
                District = hotelMessage.District,
                City = hotelMessage.City,
                Country = hotelMessage.Country,
                ZipCode = hotelMessage.ZipCode,
                MobileNumber = hotelMessage.Mobile,
                Email = hotelMessage.Email,
                PhoneNumber = hotelMessage.Phone
            };
        }
    }
}
