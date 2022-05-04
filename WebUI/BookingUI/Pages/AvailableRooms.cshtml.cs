using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingUI.ClientServices;
using BookingUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookingUI.Pages
{
    public class AvailableRoomsModel : PageModel
    {
        private readonly HotelReadService HotelReadService;
        private readonly BookingWriteService BookingWriteService;
        private readonly ILogger<AvailableRoomsModel> _logger;

        [BindProperty(SupportsGet = true)]
        public DateTime SearchChecking { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime SearchCheckout { get; set; }

        public IEnumerable<AvailableRooms> AvailableRooms { get; set; }

        public AvailableRoomsModel(
            HotelReadService hotelReadService,
            BookingWriteService bookingService,
            ILogger<AvailableRoomsModel> logger)
        {
            HotelReadService = hotelReadService;
            BookingWriteService = bookingService;
            _logger = logger;
            AvailableRooms = Enumerable.Empty<AvailableRooms>();
        }

        public async Task OnGetAvailableRooms()
        {
            AvailableRooms = await HotelReadService.GetAvailableRooms(SearchChecking, SearchCheckout);
        }

        public async Task OnPostBookThisRoom(
            Guid hotelCode,
            Guid roomCode,
            string checkin,
            string checkout,
            string guestName,
            int numberOfGuests,
            bool breakfastIncluded)
        {
            await BookingWriteService.BookRoom(
                hotelCode,
                roomCode,
                checkin,
                checkout,
                guestName,
                numberOfGuests,
                breakfastIncluded);
        }
    }
}
