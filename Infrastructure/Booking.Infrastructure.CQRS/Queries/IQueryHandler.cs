using System.Threading.Tasks;

namespace Booking.Infrastructure.CQRS.Queries
{
    public interface IQueryHandler<in TQueryParameters, TResult> where TQueryParameters : IQuery
    {
        Task<TResult> ExecuteQueryAsync(TQueryParameters queryParameters);
    }
}
