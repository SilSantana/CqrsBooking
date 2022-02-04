using System;
using System.Collections.Generic;
using System.Linq;
using static System.String;

namespace Booking.Reservation.Application.Domain
{
    internal class Room
    {
        public Guid Code { get; }
        public string Name { get; }
        public string Description { get; }
        public int Capacity { get; }
        public int AvailableQuantity { get; }
        public decimal PricePerNight { get; }
        public IReadOnlyList<string> Amenities => _amenities.ToList();

        private readonly IList<string> _amenities;

        public Room(string name, string description, int capacity,
            int availableQuantity, decimal pricePerNight, Guid code)
        {
            if (IsNullOrWhiteSpace(name))
                throw new InvalidOperationException($"The room's {nameof(name)} MUST be filled");

            Code = code;

            Name = name;
            Description = description;
            Capacity = capacity;
            AvailableQuantity = availableQuantity;
            PricePerNight = pricePerNight;

            _amenities = new List<string>();
        }

    }
}
