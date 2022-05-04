using System;
using System.Threading.Tasks;
using BookingUI.ClientServices;
using BookingUI.Models.Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingUI.Areas.Management.Pages
{
    public class UpdateHotelAddressModel : PageModel
    {
        private readonly HotelWriteService HotelWriteService;
        private readonly HotelReadService HotelReadService;

        [BindProperty]
        public CurrentAddress CurrentAddress { get; set; }

        public UpdateHotelAddressModel(
            HotelWriteService hotelService,
            HotelReadService hotelReadService)
        {
            HotelWriteService = hotelService;
            HotelReadService = hotelReadService;
        }

        public async Task OnGet(Guid hotelCode)
        {
            CurrentAddress = await HotelReadService.GetCurrentAddress(hotelCode);
            CurrentAddress.HotelCode = hotelCode;
        }

        public async Task<IActionResult> OnPost()
        {
            await HotelWriteService.UpdateHotelAddress(CurrentAddress);

            return RedirectToPage("/Index", new { area = "Management" });
        }
    }
}
