using System.Collections.Generic;
using RxLocal.Core.Domain.Customers;

namespace RxLocal.Data.Repositories.Customers
{
    public interface ICustomerRepository
    {
        int Authenticate(string loginName, string password);

        Customer GetCustomer(int customerID);
        Customer GetCustomer(string loginName);
        IList<Customer> GetCustomers(int[] customerIDs);
        int CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
    }
}
