using Booking.Infrastructure.CQRS.Commands;
using Booking.Management.Application.Commands;
using Booking.Management.Application.Domain;
using Booking.Management.Application.Repository;
using MonoidSharp;
using System;
using System.Threading.Tasks;

namespace Booking.Management.Application.Handlers
{
    internal class CreateHotelHandler : ICommandHandler<CreateHotel>
    {
        private readonly HotelPersistence _hotelPersistence;

        public CreateHotelHandler(HotelPersistence hotelPersistence)
        {
            _hotelPersistence = hotelPersistence;
        }

        public async Task<CommandResult> ExecuteAsync(CreateHotel command)
        {
            try
            {
                var address = Address.Create(command.Street, command.District, command.City, command.Country, command.Zipcode);

                var contacts = Contacts.Create(command.Email, command.Phone, command.Mobile);

                var domainCombinedValues = Outcome.Combine(address, contacts);

                if (domainCombinedValues.Failure)
                {
                    return CommandResult.Fail(domainCombinedValues.ErrorMessages);
                }

                var hotel = new Hotel(command.Name, command.StarsOfCategory, address.Value, contacts.Value);

                await _hotelPersistence.CreateHotelAsync(hotel);

                return CommandResult.Ok();
            }
            catch (Exception ex)
            {
                // Here you can call an app log method to register whatever exception that might happen
                return CommandResult.Fail($"Error while creating the hotel {command.Name}" + ex.Message);
            }
        }
    }
}
