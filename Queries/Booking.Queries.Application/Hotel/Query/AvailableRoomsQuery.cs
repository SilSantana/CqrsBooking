using Booking.Infrastructure.CQRS.Queries;
using System;

namespace Booking.Queries.Application.Hotel.Query
{
    public class AvailableRoomsQuery : IQuery
    {
        public DateTime Checkin { get; }
        public DateTime Checkout { get; }

        public AvailableRoomsQuery(DateTime checkin, DateTime checkout)
        {
            Checkin = checkin;
            Checkout = checkout;
        }
    }
}
