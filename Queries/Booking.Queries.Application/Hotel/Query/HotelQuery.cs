using Booking.Infrastructure.CQRS.Queries;
using System;

namespace Booking.Queries.Application.Hotel.Query
{
    public class HotelQuery :IQuery
    {
        public Guid Code { get; }

        public HotelQuery()
        {
        }

        public HotelQuery(Guid code)
        {
            Code = code;
        }
    }
}
