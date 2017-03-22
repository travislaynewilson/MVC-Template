using System;
using System.Collections.Generic;

namespace RxLocal.Core.Domain.Common
{
    public static class AddressExtensions
    {
        public static string GetFullStreetAddress(this Address address)
        {
            if (address == null)
                return string.Empty;

            var parts = new List<string>();

            if (string.IsNullOrEmpty(address.Address1)) parts.Add(address.Address1);
            if (string.IsNullOrEmpty(address.Address2)) parts.Add(address.Address2);
            if (string.IsNullOrEmpty(address.Address3)) parts.Add(address.Address3);

            return string.Join(", ", parts);
        }

        public static bool IsComplete(this Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            if (string.IsNullOrEmpty(address.Address1))
                return false;

            if (string.IsNullOrEmpty(address.City))
                return false;

            if (string.IsNullOrEmpty(address.State))
                return false;

            if (string.IsNullOrEmpty(address.Zip))
                return false;

            if (string.IsNullOrEmpty(address.Country))
                return false;

            return true;
        }
    }
}