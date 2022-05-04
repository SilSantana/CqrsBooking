using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingUI.ClientServices;
using BookingUI.Models.Management;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingUI.Areas.Management.Pages
{
    public class ListRoomsModel : PageModel
    {
        private readonly HotelReadService HotelReadService;
        public IEnumerable<RegisteredRoom> Rooms { get; set; }

        public Guid HotelCode { get; set; }

        public ListRoomsModel(HotelReadService hotelService)
        {
            HotelReadService = hotelService;
        }

        public async Task OnGet(Guid hotelCode)
        {
            Rooms = await HotelReadService.GetRegisteredRooms(hotelCode);
            HotelCode = hotelCode;
        }
    }
}
