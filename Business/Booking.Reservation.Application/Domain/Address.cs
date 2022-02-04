using Newtonsoft.Json;
using System;

namespace Booking.Reservation.Application.Domain
{
    internal class Address
    {
        public string Street { get; }
        public string District { get; }
        public string City { get; }
        public string Country { get; }
        public int ZipCode { get; }


        [JsonConstructor]
        private Address(string street, string district, string city, string country, int zipCode)
        {
            Street = street;
            District = district;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }

        public override string ToString()
        {
            return $"{Street}{Environment.NewLine}," +
                   $"{District}{Environment.NewLine}," +
                   $"{City}{Environment.NewLine}," +
                   $"{Country}{Environment.NewLine}," +
                   $"{ZipCode}";
        }
    }
}
