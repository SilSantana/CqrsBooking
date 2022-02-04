using Newtonsoft.Json;

namespace Booking.Reservation.Application.Domain
{
    internal class Contacts
    {
        public string Mobile { get; }
        public string Phone { get; }
        public string Email { get; }


        [JsonConstructor]
        private Contacts(string phone, string mobile, string email)
        {
            Phone = phone;
            Mobile = mobile;
            Email = email;
        }

    }
}
