using System;
using System.Threading.Tasks;
using Booking.ApiWrite.Model.Booking;
using Booking.Infrastructure.CQRS.Commands;
using Booking.Reservation.Application.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.ApiWrite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public BookingController(
            ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post(RoomReservation roomReservation)
        {
            var result = await _commandDispatcher.ExecuteAsync(
                                    new MakeRoomReservation(roomReservation.HotelCode,
                                                        roomReservation.RoomCode,
                                                        roomReservation.CheckingDate,
                                                        roomReservation.CheckoutDate,
                                                        roomReservation.Guest,
                                                        roomReservation.NumberOfGuests,
                                                        roomReservation.BreakfastIncluded));

            if (result.Failure)           
                return UnprocessableEntity(result);
          

            return Ok();
        }

        [HttpDelete]
        [Route("{reservationCode:guid}/cancel")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public IActionResult CancelReservation(Guid reservationCode)
        {
            _commandDispatcher.ExecuteAsync(new CancelReservation(reservationCode));

            return Ok();
        }

    }
}
