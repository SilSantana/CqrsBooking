using Booking.Infrastructure.CQRS.Commands;
using System;

namespace Booking.Reservation.Application.Commands
{
    public class CancelReservation : ICommand
    {
        public Guid ReservationCode { get; }

        public CancelReservation(Guid reservationCode)
        {
            ReservationCode = reservationCode;
        }
    }
}
