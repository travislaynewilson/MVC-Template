using RxLocal.Services.Authentication;
using RxLocal.Services.Customers;

namespace RxLocal.Services
{
    public interface IServiceManager
    {
        IAuthenticationService Authentication { get; }
        ICustomerService Customers { get; }
    }
}
