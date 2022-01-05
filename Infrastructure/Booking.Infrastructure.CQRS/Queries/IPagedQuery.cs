namespace Booking.Infrastructure.CQRS.Queries
{
    public interface IPagedQuery<in TPagedQueryParameters> where TPagedQueryParameters : PagingParameters
    {
    }
}
