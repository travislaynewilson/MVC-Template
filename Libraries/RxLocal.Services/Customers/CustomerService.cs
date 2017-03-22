using RxLocal.Core.Domain.Customers;
using RxLocal.Data;

namespace RxLocal.Services.Customers
{
    public partial class CustomerService : ICustomerService
    {
        private readonly IDataManager _dataManager;

        public CustomerService(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }


        public CustomerAuthenticationResults AuthenticateCustomer(string loginName, string password, out Customer customer)
        {
            customer = GetCustomer(loginName);

            if (customer == null)
                return CustomerAuthenticationResults.CustomerNotExist;
            if (customer.IsDeleted())
                return CustomerAuthenticationResults.Deleted;
            if (!customer.CanLogin)
                return CustomerAuthenticationResults.LoginDisabled;

            var customerID = _dataManager.Customers.Authenticate(loginName, password);
            if (customerID != customer.CustomerID)
                return CustomerAuthenticationResults.InvalidPassword;

            var authorized = AuthorizeCustomer(customer);
            if (!authorized)
                return CustomerAuthenticationResults.NotAuthorized;

            return CustomerAuthenticationResults.Successful;
        }
        public bool AuthorizeCustomer(Customer customer)
        {
            // If the customer is required to have a subscription or other flag that restricts access,
            // put that logic here.
            // Return true if they're good to go, and false if they are not authorized to continue.

            return true;
        }

        public virtual Customer GetCustomer(int customerID)
        {
            if (customerID == 0) 
                return null;

            var customer = _dataManager.Customers.GetCustomer(customerID);

            // Business logic goes here...

            return customer;
        }
        public virtual Customer GetCustomer(string loginName)
        {
            if (string.IsNullOrEmpty(loginName))
                return null;

            var customer = _dataManager.Customers.GetCustomer(loginName);

            // Business logic goes here...

            return customer;
        }
    }
}