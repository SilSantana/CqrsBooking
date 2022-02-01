using Booking.Infrastructure.CQRS.Queries;
using Booking.Queries.Application.Hotel.Query;
using Booking.Queries.Application.Hotel.ReadModel;
using Booking.Queries.Application.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking.Queries.Application.Hotel.Processors
{
    internal class HotelQueryHandler :
        IQueryHandler<HotelQuery, IEnumerable<HotelListItem>>,
        IQueryHandler<RoomQuery, IEnumerable<RoomListItem>>,
        IQueryHandler<AvailableRoomsQuery, IEnumerable<AvailableRooms>>,
        IQueryHandler<CurrentAddressQuery, CurrentAddress>,
        IQueryHandler<CurrentContactsQuery, CurrentContacts>
    {

        private readonly HotelPersistence _hotelPersistence;

        public HotelQueryHandler(HotelPersistence hotelPersistence)
        {
            _hotelPersistence = hotelPersistence;
        }


        public async Task<CurrentContacts> ExecuteQueryAsync(CurrentContactsQuery queryParameters)
        {
            return await _hotelPersistence.RetrieveCurrentContacts(queryParameters.HotelCode);
        }

        public async Task<CurrentAddress> ExecuteQueryAsync(CurrentAddressQuery queryParameters)
        {
            return await _hotelPersistence.RetrieveCurrentAddress(queryParameters.HotelCode);
        }

        public async Task<IEnumerable<AvailableRooms>> ExecuteQueryAsync(AvailableRoomsQuery queryParameters)
        {
            return await _hotelPersistence.RetrieveAvailableRooms(queryParameters.Checkin, queryParameters.Checkout);
        }

        public async Task<IEnumerable<RoomListItem>> ExecuteQueryAsync(RoomQuery queryParameters)
        {
            return await _hotelPersistence.RetrieveRooms(queryParameters.HotelCode);
        }

        public async Task<IEnumerable<HotelListItem>> ExecuteQueryAsync(HotelQuery queryParameters)
        {
            return await _hotelPersistence.RetrieveHotels();
        }
    }
}
