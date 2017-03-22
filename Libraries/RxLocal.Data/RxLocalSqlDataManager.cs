using System;
using RxLocal.Data.Contexts;
using RxLocal.Data.Repositories.Customers;

namespace RxLocal.Data
{
    public class RxLocalSqlDataManager : IDataManager, IDisposable
    {
        private RxLocalSyncSqlDbContext _dbContext;

        public RxLocalSqlDataManager()
        {
            _dbContext = new RxLocalSyncSqlDbContext();
        }


        private ICustomerRepository _customers;
        public ICustomerRepository Customers => _customers ?? (_customers = new CustomerSqlRepository(_dbContext));


        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }
    }
}
