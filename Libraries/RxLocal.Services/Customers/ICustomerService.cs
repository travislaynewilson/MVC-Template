using RxLocal.Core.Domain.Customers;

namespace RxLocal.Services.Customers
{
    public partial interface ICustomerService
    {
        CustomerAuthenticationResults AuthenticateCustomer(string loginName, string password, out Customer customer);
        bool AuthorizeCustomer(Customer customer);

        Customer GetCustomer(int customerID);
        Customer GetCustomer(string loginName);
    }
}
