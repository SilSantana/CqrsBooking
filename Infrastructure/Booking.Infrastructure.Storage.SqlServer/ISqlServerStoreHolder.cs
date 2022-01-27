using System.Data;

namespace Booking.Infrastructure.Storage.SqlServer
{
    public interface ISqlServerStoreHolder
    {
        IDbConnection DbConnection { get; }

    }
}
