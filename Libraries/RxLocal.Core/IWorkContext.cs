using RxLocal.Core.Domain.Customers;

namespace RxLocal.Core
{
    /// <summary>
    /// Work context
    /// </summary>
    public interface IWorkContext 
    {
        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        Customer CurrentCustomer { get; set; }
    }
}
