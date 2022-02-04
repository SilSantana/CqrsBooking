using Booking.Infrastructure.Storage.RavenDB;
using Booking.Reservation.Application.Domain;
using Raven.Client.Documents;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Reservation.Application.Repository
{
    internal class HotelPersistence
    {

        private readonly IRavenDocumentStoreHolder _ravenDocumentStore;

        public HotelPersistence(IRavenDocumentStoreHolder ravenDocumentStore)
        {
            _ravenDocumentStore = ravenDocumentStore;
        }


        internal async Task<Hotel> RetrieveHotelAndRoomByCodeAsync(Guid hotelCode, Guid roomCode)
        {
            using (var session = _ravenDocumentStore.Store.OpenAsyncSession())
            {
                var searchedHotel = await session.Query<Hotel>()
                                                 .Where(hotel => hotel.Code == hotelCode &&
                                                                 hotel.Rooms.Any(room => room.Code == roomCode))
                                                 .FirstOrDefaultAsync();

                return searchedHotel;
            }
        }

    }
}
