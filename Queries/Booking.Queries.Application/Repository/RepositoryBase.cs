﻿using Booking.Infrastructure.Storage.SqlServer;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Booking.Queries.Application.Repository
{
    internal class RepositoryBase
    {
        private readonly ISqlServerStoreHolder _sqlServerStoreHolder;

        public RepositoryBase(ISqlServerStoreHolder sqlServerStoreHolder)
        {
            _sqlServerStoreHolder = sqlServerStoreHolder;
        }

        protected async Task<T> HandleConnection<T>(Func<IDbConnection, Task<T>> func)
        {
            try
            {
                _sqlServerStoreHolder.DbConnection.Open();

                return await func(_sqlServerStoreHolder.DbConnection);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _sqlServerStoreHolder.DbConnection.Close();
            }
        }
    }
}
