using Booking.Infrastructure.CQRS.Commands;
using Booking.Infrastructure.CQRS.Queries;
using Booking.Queries.Application.Reservation.Query;
using Booking.Queries.Application.Reservation.ReadModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Booking.ApiRead.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IQueryProcessor _queryProcessor;

        public BookingController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }


        [HttpGet]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{reservationCode}/detail")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReservationDetail(string reservationCode)
        {
            var retrieveReservationDetail = new RetrieveReservationDetail(reservationCode);

            var result = await _queryProcessor.ExecuteQueryAsync<RetrieveReservationDetail, ReservationDetail>(retrieveReservationDetail);

            return Ok(result);
        }

    }
}
