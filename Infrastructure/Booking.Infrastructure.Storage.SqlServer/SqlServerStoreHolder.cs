using System;
using System.Data;

namespace Booking.Infrastructure.Storage.SqlServer
{
    internal class SqlServerStoreHolder : ISqlServerStoreHolder
    {

        private readonly SqlServerSettings _sqlServerSettings;
        private readonly Lazy<IDbConnection> _dbConnection;

        public SqlServerStoreHolder(Lazy<IDbConnection> dbConnection)
        {
            //_sqlServerSettings = optionsDatabaseSettings.Value;
            _dbConnection = dbConnection;
        }


        public IDbConnection DbConnection => _dbConnection.Value;
    }
}
