using Booking.Infrastructure.CQRS.Domain;
using Booking.Reservation.Application.Domain.Events;
using System;
using System.Linq;

namespace Booking.Reservation.Application.Domain
{
    internal class ReservationDetail : Aggregate
    {
        public string BookedBy { get; }
        public int NumberOfGuests { get; }
        public int NumberOfNights => Period.NumberOfNights(BookingPeriod);
        public decimal PaidPrice => NumberOfNights * BookedRoom.PricePerNight;
        public string Code { get; }
        public bool BreakfastIncluded { get; }
        public Period BookingPeriod { get; }
        public BookedHotel BookedHotel { get; }
        public BookedRoom BookedRoom { get; }


        public ReservationDetail(BookedHotel bookedHotel, BookedRoom bookedRoom,
          Period bookingPeriod, string bookedBy, int numberOfGuests, bool breakfastIncluded)
              : base(Guid.NewGuid())
        {
            BookingPeriod = bookingPeriod;
            BookedBy = bookedBy;
            NumberOfGuests = numberOfGuests;
            BreakfastIncluded = breakfastIncluded;
            BookedHotel = bookedHotel;
            BookedRoom = bookedRoom;
            Code = GenerateReservationNumber();

            AddEvent(new ReservationCreated(Code,
                                            bookedBy,
                                            numberOfGuests,
                                            NumberOfNights,
                                            PaidPrice,
                                            breakfastIncluded,
                                            bookingPeriod,
                                            bookedHotel,
                                            bookedRoom));
        }

        private string GenerateReservationNumber()
        {
            long i = Guid.NewGuid()
                         .ToByteArray()
                         .Aggregate<byte, long>(1, (current, b) => current * (b + 1));

            return $"{i - BookingPeriod.Checkin.Ticks:x}";
        }

    }
}
