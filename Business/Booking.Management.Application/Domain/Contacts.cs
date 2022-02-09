using Booking.Infrastructure.CQRS.Domain;
using MonoidSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Booking.Management.Application.Domain
{
    internal class Contacts : ValueObject
    {
        public string Mobile { get; }
        public string Phone { get; }
        public string Email { get; }


        [JsonConstructor]
        private Contacts(string email, string phone, string mobile)
        {
            Phone = phone;
            Mobile = mobile;
            Email = email;
        }

        public static Outcome<Contacts> Create(PossibleBe<string> email, PossibleBe<string> phone, PossibleBe<string> mobile)
        {
            if (AreContactsValid(email, phone, mobile))            
                return Outcome.Successfully(
                    new Contacts(
                        email.Value,
                        phone.Value,
                        mobile.Value));            

            return Outcome.Failed<Contacts>("All contacts are invalid. Please verify all contact values!");
        }

        private static bool AreContactsValid(PossibleBe<string> email, PossibleBe<string> phone, PossibleBe<string> mobile)
        {
            return phone.HasValue && mobile.HasValue && email.HasValue;
        }

        protected override IEnumerable<object> GetEqualityProperties()
        {
            yield return Mobile;
            yield return Phone;
            yield return Email;
        }

    }
}
