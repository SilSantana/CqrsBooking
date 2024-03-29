﻿using Booking.Infrastructure.CQRS.Commands;
using Booking.Management.Application.Commands;
using Booking.Management.Application.Domain;
using Booking.Management.Application.Repository;
using System;
using System.Threading.Tasks;

namespace Booking.Management.Application.Handlers
{
    internal class AddRoomToHotelHandler : ICommandHandler<AddRoomToHotel>
    {
        private readonly HotelPersistence _hotelPersistence;

        public AddRoomToHotelHandler(HotelPersistence hotelPersistence)
        {
            _hotelPersistence = hotelPersistence;
        }

        public async Task<CommandResult> ExecuteAsync(AddRoomToHotel command)
        {
            try
            {
                var searchedHotel = await _hotelPersistence.RetrieveHotelByCodeAsync(command.HotelCode);

                if (searchedHotel == null)
                    return CommandResult.Fail("There isn't an hotel to add a new room");

                var room = new Room(command.Name,
                                    command.Description,
                                    command.Capacity,
                                    command.AvailableQuantity,
                                    command.PricePerNight);

                foreach (string amenity in command.Amenities)
                {
                    room.AddAmenities(amenity);
                }

                searchedHotel.AddRoom(room);

                await _hotelPersistence.AddRoomToHotel(searchedHotel);

                return CommandResult.Ok();
            }
            catch (Exception ex)
            {
                return CommandResult.Fail($"Error while adding a room for the hotel." + ex.Message);
            }
        }

    }
}
