using RxLocal.Data.Repositories.Customers;

namespace RxLocal.Data
{
    public interface IDataManager
    {
        ICustomerRepository Customers { get; }
    }
}
