using System;
using System.Threading.Tasks;
using BookingUI.ClientServices;
using BookingUI.Models.Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingUI.Areas.Management.Pages
{
    public class RegisterRoomModel : PageModel
    {

        private readonly HotelWriteService HotelService;

        [BindProperty]
        public RoomForRegistring NewRoom { get; set; }

        public Guid HotelCode { get; set; }

        public RegisterRoomModel(HotelWriteService hotelService)
        {
            HotelService = hotelService;
        }

        public void OnGet(Guid hotelCode)
        {
            HotelCode = hotelCode;
        }

        public async Task<IActionResult> OnPost(Guid hotelCode)
        {
            await HotelService.RegisterRoom(hotelCode, NewRoom);

            return RedirectToPage("/Index", new { area = "Management" });
        }
    }
}
