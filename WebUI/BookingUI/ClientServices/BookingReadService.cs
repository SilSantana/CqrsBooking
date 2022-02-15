using System.Net.Http;
using System.Threading.Tasks;
using BookingUI.Models;

namespace BookingUI.ClientServices
{
    public class BookingReadService
    {

        private readonly HttpClient ReservationClient;

        public BookingReadService(HttpClient client)
        {
            ReservationClient = client;
        }

        public async Task<ReservationDetail> GetDetails(string reservationCode)
        {
            try
            {
                var relativePathEndpoint =
                    string.Format(ClientServiceEndpoints.BookingEndpoints.ReservationDetails, reservationCode);

                var response = await ReservationClient.GetAsync(relativePathEndpoint);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<ReservationDetail>();
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

    }
}
