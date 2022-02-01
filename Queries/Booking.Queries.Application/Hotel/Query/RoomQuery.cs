using Booking.Infrastructure.CQRS.Queries;
using System;

namespace Booking.Queries.Application.Hotel.Query
{
    public class RoomQuery : IQuery
    {
        public Guid HotelCode { get; }

        public RoomQuery(Guid hotelCode)
        {
            HotelCode = hotelCode;
        }
    }
}
