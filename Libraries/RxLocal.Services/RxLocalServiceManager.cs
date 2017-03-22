using RxLocal.Core.Infrastructure;
using RxLocal.Data;
using RxLocal.Services.Authentication;
using RxLocal.Services.Customers;

namespace RxLocal.Services
{
    public class RxLocalServiceManager : IServiceManager
    {
        private readonly IDataManager _dataManager;

        public RxLocalServiceManager(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        private IAuthenticationService _authentication;
        public IAuthenticationService Authentication => _authentication ?? (_authentication = EngineContext.Current.Resolve<IAuthenticationService>());

        private ICustomerService _customers;
        public ICustomerService Customers => _customers ?? (_customers = EngineContext.Current.Resolve<ICustomerService>());
    }
}
