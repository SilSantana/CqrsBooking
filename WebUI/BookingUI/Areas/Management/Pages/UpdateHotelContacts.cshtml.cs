using System;
using System.Threading.Tasks;
using BookingUI.ClientServices;
using BookingUI.Models.Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingUI.Areas.Management.Pages
{
    public class UpdateHotelContactsModel : PageModel
    {
        private readonly HotelWriteService HotelWriteService;
        private readonly HotelReadService HotelReadService;

        [BindProperty]
        public CurrentContacts CurrentContacts { get; set; }

        public UpdateHotelContactsModel(
            HotelWriteService hotelService,
            HotelReadService hotelReadService)
        {
            HotelWriteService = hotelService;
            HotelReadService = hotelReadService;
        }

        public async Task OnGet(Guid hotelCode)
        {
            CurrentContacts = await HotelReadService.GetCurrentContacts(hotelCode);
            CurrentContacts.HotelCode = hotelCode;
        }

        public async Task<IActionResult> OnPost()
        {
            await HotelWriteService.UpdateHotelContacts(CurrentContacts);

            return RedirectToPage("/Index", new { area = "Management" });
        }
    }
}
