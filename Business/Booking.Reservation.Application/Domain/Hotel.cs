using System;
using System.Collections.Generic;
using System.Linq;

namespace Booking.Reservation.Application.Domain
{
    internal class Hotel
    {

        private IList<Room> _rooms;
        public Guid Code { get; }
        public string Name { get; }

        public int StarsOfCategory { get; }

        public Address Address { get; }

        public Contacts Contacts { get; }

        public IEnumerable<Room> Rooms
        {
            get => _rooms.ToList();
            set => _rooms = value.ToList();
        }

        public Hotel(string name, int starsOfCategory, Address address, Contacts contacts)
        {
            Name = name;
            StarsOfCategory = starsOfCategory;
            Address = address;
            Contacts = contacts;

            _rooms = new List<Room>();
            Rooms = new List<Room>();
        }
    }
}
