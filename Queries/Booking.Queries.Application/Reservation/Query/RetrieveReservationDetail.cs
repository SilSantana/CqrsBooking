using Booking.Infrastructure.CQRS.Queries;

namespace Booking.Queries.Application.Reservation.Query
{
    public class RetrieveReservationDetail : IQuery
    {
        public string ReservationCode { get; }

        public RetrieveReservationDetail(string reservationCode)
        {
            ReservationCode = reservationCode;
        }
    }
}
