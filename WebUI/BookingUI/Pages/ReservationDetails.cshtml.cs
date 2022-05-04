using System.Threading.Tasks;
using BookingUI.ClientServices;
using BookingUI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingUI.Pages
{
    public class ReservationDetailsModel : PageModel
    {
        private readonly BookingReadService BookingReadService;

        public ReservationDetail Details { get; set; }

        public ReservationDetailsModel(BookingReadService bookingService)
        {
            BookingReadService = bookingService;

            Details = new ReservationDetail();
        }

        public async Task OnGet(string reservationCode)
        {
            Details = await BookingReadService.GetDetails(reservationCode);
        }
    }
}
