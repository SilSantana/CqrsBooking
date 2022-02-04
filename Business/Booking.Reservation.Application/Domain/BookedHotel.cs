using MonoidSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Booking.Reservation.Application.Domain
{
    internal class BookedHotel
    {
        public string Name { get; }
        public string Address { get; }
        public int StarsOfCategory { get; }


        [JsonConstructor]
        private BookedHotel(string name, string address, int starsOfCategory)
        {
            Name = name;
            Address = address;
            StarsOfCategory = starsOfCategory;
        }

        public static Outcome<BookedHotel> Create(
        PossibleBe<string> name,
        PossibleBe<string> address,
        PossibleBe<int> starsOfCategory)
        {
            var hotelValid = IsValidHotelToBook(name, address, starsOfCategory);
            if (!hotelValid.Success)            
                return Outcome.Failed<BookedHotel>(hotelValid.ErrorMessages);
            
            return Outcome.Successfully(
                new BookedHotel(
                    name.Value,
                    address.Value,
                    starsOfCategory.Value));
        }

        private static Outcome<bool> IsValidHotelToBook(
            PossibleBe<string> name,
            PossibleBe<string> address,
            PossibleBe<int> starsOfCategory)
        {
            List<string> errors = new List<string>();

            if (name.HasNoValue)
                errors.Add($"The Hotel {nameof(name)} MUST be filled");

            if (address.HasNoValue)
                errors.Add($"The Hotel {nameof(address)} MUST be filled");

            if (starsOfCategory.HasNoValue)
                errors.Add($"The Hotel {nameof(starsOfCategory)} MUST be greater than Zero");

            return errors.Count == 0 ? Outcome.Successfully(true) : Outcome.Failed<bool>(errors);
        }
    }
}
