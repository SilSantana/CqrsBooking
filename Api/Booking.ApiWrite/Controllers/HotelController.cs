﻿using System;
using System.Threading.Tasks;
using Booking.ApiWrite.Model.Management;
using Booking.Infrastructure.CQRS.Commands;
using Booking.Management.Application.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.ApiWrite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public HotelController(
            ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }


        [HttpPost("")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> RegisterHotel(Hotel hotel)
        {
            var result = await _commandDispatcher.ExecuteAsync(
                            new CreateHotel(hotel.Name,
                                            hotel.StarsOfCategory,
                                            hotel.Street,
                                            hotel.District,
                                            hotel.City,
                                            hotel.Country,
                                            hotel.Zipcode,
                                            hotel.Email,
                                            hotel.Phone,
                                            hotel.Mobile));

            if (result.Failure)
            {
                return UnprocessableEntity(result);
            }

            return Created("", result);
        }

        [HttpPut("{hotelCode:guid}/address/update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateHotelAddress(Guid hotelCode, HotelAddress hotelAddress)
        {
            var result = await _commandDispatcher.ExecuteAsync(
                new UpdateHotelAddress(hotelCode,
                                       hotelAddress.Street,
                                       hotelAddress.District,
                                       hotelAddress.City,
                                       hotelAddress.Country,
                                       hotelAddress.Zipcode));

            if (result.Failure)
            {
                return UnprocessableEntity(result);
            }

            return NoContent();
        }

        [HttpPut("{hotelCode:guid}/contacts/update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateHotelContacts(Guid hotelCode, HotelContacts hotelContacts)
        {
            var result = await _commandDispatcher.ExecuteAsync(
                new UpdateHotelContacts(hotelCode,
                                        hotelContacts.Email,
                                        hotelContacts.Phone,
                                        hotelContacts.Mobile));

            if (result.Failure)
            {
                return UnprocessableEntity(result);
            }

            return NoContent();
        }

        [HttpPost("{hotelCode:guid}/room")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddHotelRoom(Guid hotelCode, [FromBody] HotelRoom hotelRoom)
        {
            var result = await _commandDispatcher.ExecuteAsync(
                new AddRoomToHotel(hotelCode,
                                   hotelRoom.Name,
                                   hotelRoom.Description,
                                   hotelRoom.Capacity,
                                   hotelRoom.AvailableQuantity,
                                   hotelRoom.PricePerNight,
                                   hotelRoom.Amenities));

            if (result.Failure)
            {
                return UnprocessableEntity(result);
            }

            return NoContent();
        }

    }
}
