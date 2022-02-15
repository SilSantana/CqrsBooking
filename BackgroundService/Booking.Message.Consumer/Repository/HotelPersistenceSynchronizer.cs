using System;
using System.Threading.Tasks;
using Booking.Infrastructure.Storage.SqlServer;
using Booking.Message.Consumer.Models;
using Dapper;

namespace Booking.Message.Consumer.Repository
{
    internal class HotelPersistenceSynchronizer : RepositoryBase
    {
        public HotelPersistenceSynchronizer(ISqlServerStoreHolder sqlServerStoreHolder) : base(sqlServerStoreHolder)
        {
        }

        public async Task SynchronizeHotelData(HotelData hotelData)
        {
            await HandleConnection(async (connection) =>
            {
                return await connection.InsertAsync<Guid, HotelData>(hotelData);
            });
        }

        public async Task SynchronizeHotelAddressData(HotelAddressData hotelAddressData)
        {
            await HandleConnection(async (connection) =>
            {
                return await connection.UpdateAsync(hotelAddressData);
            });
        }

        public async Task SynchronizeHotelContactsData(HotelContactsData hotelContactsData)
        {
            await HandleConnection(async (connection) =>
            {
                return await connection.UpdateAsync(hotelContactsData);
            });
        }

        public async Task SynchronizeRoomData(RoomData roomData)
        {
            await HandleConnection(async (connection) =>
            {
                return await connection.InsertAsync<Guid, RoomData>(roomData);
            });
        }
        
    }    
}
