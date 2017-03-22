using System;

namespace RxLocal.Core.Domain.Customers
{
    public static class CustomerExtensions
    {
        public static bool IsDeleted(this Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            return customer.CustomerStatusID == 0;
        }
    }
}
