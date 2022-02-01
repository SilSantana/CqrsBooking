using Booking.Infrastructure.Storage.SqlServer;
using Booking.Queries.Application.Reservation.ReadModel;
using Dapper;
using System.Threading.Tasks;

namespace Booking.Queries.Application.Repository
{
    internal class ReservationPersistence
    {

        private readonly ISqlServerStoreHolder _sqlServerStoreHolder;

        public ReservationPersistence(ISqlServerStoreHolder sqlServerStoreHolder)
        {
            _sqlServerStoreHolder = sqlServerStoreHolder;
        }

        public async Task<ReservationDetail> RetrieveReservationDetail(string reservationCode)
        {
            using (var connection = _sqlServerStoreHolder.DbConnection)
            {
                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<ReservationDetail>(
                    "SELECT * FROM Reservations where Code = @reservationCode",
                    new { reservationCode });
            }
        }

        public async Task<ReservationDetail> RetrieveReservations()
        {
            using (var connection = _sqlServerStoreHolder.DbConnection)
            {
                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<ReservationDetail>("SELECT * FROM Reservations");
            }
        }

    }
}
