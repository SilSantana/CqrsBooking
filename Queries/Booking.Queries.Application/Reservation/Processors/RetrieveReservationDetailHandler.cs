using Booking.Infrastructure.CQRS.Queries;
using Booking.Queries.Application.Repository;
using Booking.Queries.Application.Reservation.Query;
using Booking.Queries.Application.Reservation.ReadModel;
using System.Threading.Tasks;

namespace Booking.Queries.Application.Reservation.Processors
{
    internal class RetrieveReservationDetailHandler : IQueryHandler<RetrieveReservationDetail, ReservationDetail>
    {

        private readonly ReservationPersistence _reservationPersistence;

        public RetrieveReservationDetailHandler(ReservationPersistence reservationPersistence)
        {
            _reservationPersistence = reservationPersistence;
        }

        public async Task<ReservationDetail> ExecuteQueryAsync(RetrieveReservationDetail queryParameters)
        {
            return await _reservationPersistence.RetrieveReservationDetail(queryParameters.ReservationCode);
        }
    }
}
