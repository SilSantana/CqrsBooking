using Booking.Infrastructure.CQRS.Commands;
using System;
using System.Collections.Generic;

namespace Booking.Management.Application.Commands
{
    public class AddRoomToHotel : ICommand
    {
        public Guid HotelCode { get; }
        public string Name { get; }
        public string Description { get; }
        public int Capacity { get; }
        public int AvailableQuantity { get; }
        public decimal PricePerNight { get; }
        public IList<string> Amenities { get; }

                
        public AddRoomToHotel(Guid hotelCode, string name, string description, int capacity,
                    int availableQuantity, decimal pricePerNight, IList<string> amenities)
        {
            HotelCode = hotelCode;
            Name = name;
            Description = description;
            Capacity = capacity;
            AvailableQuantity = availableQuantity;
            PricePerNight = pricePerNight;
            Amenities = amenities;
        }
    }
}
