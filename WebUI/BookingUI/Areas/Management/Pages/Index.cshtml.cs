using System.Collections.Generic;
using System.Threading.Tasks;
using BookingUI.ClientServices;
using BookingUI.Models.Management;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingUI.Areas.Management.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HotelReadService HotelReadService;

        public IEnumerable<RegisteredHotel> Hotels { get; set; }

        public IndexModel(HotelReadService hotelService)
        {
            HotelReadService = hotelService;
        }

        public async Task OnGet()
        {
            Hotels = await HotelReadService.GetRegisteredHotels();
        }
    }
}
