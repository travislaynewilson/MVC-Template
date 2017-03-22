using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RxLocal.Core.Domain.Customers;
using RxLocal.Data.Contexts;

namespace RxLocal.Data.Repositories.Customers
{
    public partial class CustomerSqlRepository : ICustomerRepository
    {
        private readonly RxLocalSyncSqlDbContext _dbContext;

        public CustomerSqlRepository(RxLocalSyncSqlDbContext dbContext)
        {
            _dbContext         = dbContext;
        }


        public int Authenticate(string loginName, string password)
        {
            var customerID = _dbContext.AuthenticateCustomer(loginName, password);

            return customerID;
        }

        public Customer GetCustomer(int customerID)
        {
            var customer = _dbContext.Customers
                    .FirstOrDefault(c => c.CustomerID == customerID);

            return Mapper.Map<Entities.Customer, Customer>(customer);
        }
        public Customer GetCustomer(string loginName)
        {
            var customer = _dbContext.Customers
                    .Where(c => c.LoginName == loginName)
                    .FirstOrDefault(c => c.CanLogin == true);

            return Mapper.Map<Entities.Customer, Customer>(customer);
        }
        public IList<Customer> GetCustomers(int[] customerIDs)
        {
            var customers = _dbContext.Customers
                .Where(c => customerIDs.Contains(c.CustomerID))
                .OrderBy(c => c.CustomerID)
                .ToList();

            return Mapper.Map<List<Entities.Customer>, List<Customer>>(customers);
        }
        public int CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
