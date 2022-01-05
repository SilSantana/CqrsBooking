using System.Threading.Tasks;

namespace Booking.Infrastructure.CQRS.Queries
{
    public interface IQueryProcessor
    {
        Task<TResult> ExecuteQueryAsync<TQueryParameters, TResult>(TQueryParameters queryParameters)
            where TQueryParameters : IQuery;

        Task<TResult> ExecutePagedQueryAsync<TPagedQueryParameters, TResult>(TPagedQueryParameters pagingQueryParameters)
            where TPagedQueryParameters : IPagedQuery<PagingParameters>;
    }
}
