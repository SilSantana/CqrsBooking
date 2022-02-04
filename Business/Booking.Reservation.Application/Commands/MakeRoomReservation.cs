using Booking.Infrastructure.CQRS.Commands;
using System;

namespace Booking.Reservation.Application.Commands
{
    public class MakeRoomReservation : ICommand
    {
        public Guid HotelCode { get; }
        public Guid RoomCode { get; }
        public DateTime CheckingDate { get; }
        public DateTime CheckoutDate { get; }
        public string Guest { get; }
        public int NumberOfGuests { get; }
        public bool BreakfastIncluded { get; }


        public MakeRoomReservation(
            Guid hotelCode, Guid roomCode, DateTime checkingDate,
            DateTime checkoutDate, string guest, int numberOfGuests, bool breakfastIncluded)
        {
            HotelCode = hotelCode;
            RoomCode = roomCode;
            CheckingDate = checkingDate;
            CheckoutDate = checkoutDate;
            Guest = guest;
            NumberOfGuests = numberOfGuests;
            BreakfastIncluded = breakfastIncluded;
        }

    }
}
