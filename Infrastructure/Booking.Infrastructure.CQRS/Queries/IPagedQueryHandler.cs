using System.Threading.Tasks;

namespace Booking.Infrastructure.CQRS.Queries
{
    public interface IPagedQueryHandler<in TPagedQueryParameters, TResult> where TPagedQueryParameters : IPagedQuery<PagingParameters>
    {
        Task<TResult> ExecuteQueryAsync(TPagedQueryParameters queryParameters);

    }
}
