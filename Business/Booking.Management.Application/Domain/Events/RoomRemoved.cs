﻿namespace Booking.Management.Application.Domain.Events
{
    internal sealed class RoomRemoved : HotelEventBaseV1
    {
        public RoomRemoved() : base(nameof(RoomRemoved))
        {
        }
    }
}
