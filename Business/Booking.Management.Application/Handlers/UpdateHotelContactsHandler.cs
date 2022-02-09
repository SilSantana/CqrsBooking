using Booking.Infrastructure.CQRS.Commands;
using Booking.Management.Application.Commands;
using Booking.Management.Application.Domain;
using Booking.Management.Application.Repository;
using MonoidSharp;
using System;
using System.Threading.Tasks;

namespace Booking.Management.Application.Handlers
{
    internal class UpdateHotelContactsHandler : ICommandHandler<UpdateHotelContacts>
    {
        private readonly HotelPersistence _hotelPersistence;

        public UpdateHotelContactsHandler(HotelPersistence hotelPersistence)
        {
            _hotelPersistence = hotelPersistence;
        }

        public async Task<CommandResult> ExecuteAsync(UpdateHotelContacts command)
        {
            try
            {
                var hotel = await _hotelPersistence.RetrieveHotelByCodeAsync(command.HotelCode);

                if (hotel == null)
                    return CommandResult.Fail("There isn't an hotel to update the Contacts");

                var contacts = Contacts.Create(command.NewEmail, command.NewPhone, command.NewMobile);

                var domainCombinedValues = Outcome.Combine(contacts);

                if (domainCombinedValues.Failure)
                {
                    return CommandResult.Fail(domainCombinedValues.ErrorMessages);
                }

                hotel.ChangeContacts(contacts.Value);

                await _hotelPersistence.UpdateHotelContacts(hotel);

                return CommandResult.Ok();
            }
            catch (Exception ex)
            {
                return CommandResult.Fail($"Error while updating the contacts for the hotel." + ex.Message);
            }
        }

    }
}
