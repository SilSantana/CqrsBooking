﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookingUI.ClientServices
{
    public class BookingWriteService
    {
        private readonly HttpClient ReservationClient;

        public BookingWriteService(HttpClient client)
        {
            ReservationClient = client;
        }

        internal async Task<HttpResponseMessage> BookRoom(Guid hotelCode,
            Guid roomCode,
            string checkin,
            string checkout,
            string guestName,
            int numberOfGuests,
            bool breakfastIncluded)
        {
            try
            {
                var response = await ReservationClient.PostAsJsonAsync(
                    ClientServiceEndpoints.BookingEndpoints.BookRoom, new RoomReservation
                    {
                        HotelCode = hotelCode,
                        RoomCode = roomCode,
                        CheckingDate = Convert.ToDateTime(checkin),
                        CheckoutDate = Convert.ToDateTime(checkout),
                        Guest = guestName,
                        NumberOfGuests = numberOfGuests,
                        BreakfastIncluded = breakfastIncluded
                    });

                return response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
    }

    public class RoomReservation
    {
        public Guid HotelCode { get; set; }
        public Guid RoomCode { get; set; }
        public DateTime CheckingDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string Guest { get; set; }
        public bool BreakfastIncluded { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
