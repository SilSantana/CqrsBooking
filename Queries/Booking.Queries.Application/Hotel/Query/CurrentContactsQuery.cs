using Booking.Infrastructure.CQRS.Queries;
using System;

namespace Booking.Queries.Application.Hotel.Query
{
    public class CurrentContactsQuery : IQuery
    {
        public Guid HotelCode { get; }

        public CurrentContactsQuery(Guid hotelCode)
        {
            HotelCode = hotelCode;
        }

    }
}
