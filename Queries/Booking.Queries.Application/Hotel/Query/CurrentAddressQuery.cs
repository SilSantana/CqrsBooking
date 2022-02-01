using Booking.Infrastructure.CQRS.Queries;
using System;

namespace Booking.Queries.Application.Hotel.Query
{
    public class CurrentAddressQuery : IQuery
    {
        public Guid HotelCode { get; }

        public CurrentAddressQuery(Guid hotelCode)
        {
            HotelCode = hotelCode;
        }
    }
}
