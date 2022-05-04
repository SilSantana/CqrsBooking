using System.Threading.Tasks;
using BookingUI.ClientServices;
using BookingUI.Models.Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingUI.Areas.Management.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly HotelWriteService HotelService;

        [BindProperty]
        public HotelForRegistring NewHotel { get; set; }

        public RegisterModel(HotelWriteService hotelService)
        {
            HotelService = hotelService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            await HotelService.Register(NewHotel);

            return RedirectToPage("/Index", new { area = "Management" });
        }
    }
}
