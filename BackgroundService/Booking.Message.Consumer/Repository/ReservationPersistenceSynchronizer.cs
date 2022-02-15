using System;
using System.Threading.Tasks;
using Booking.Infrastructure.Storage.SqlServer;
using Booking.Message.Consumer.Models;
using Dapper;

namespace Booking.Message.Consumer.Repository
{
    internal class ReservationPersistenceSynchronizer : RepositoryBase
    {

        public ReservationPersistenceSynchronizer(ISqlServerStoreHolder sqlServerStoreHolder) : base(sqlServerStoreHolder)
        {
        }

        public async Task SynchronizeReservationData(ReservationData reservationData)
        {
            await HandleConnection(async (connection) =>
            {
                return await connection.InsertAsync<Guid, ReservationData>(reservationData);
            });
        }

    }
}
