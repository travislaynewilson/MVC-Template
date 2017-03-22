using System;

namespace RxLocal.Core.Domain.Common
{
    public partial class Address : DomainModel, ICloneable
    {
        public AddressType AddressType { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }


        public override int GetHashCode()
        {
            return GenerateHashCode(AddressType, Address1, Address2, Address3, City, State, County, Zip, Country);
        }

        public object Clone()
        {
            var address = new Address()
            {
                AddressType = AddressType,
                Address1    = Address1,
                Address2    = Address2,
                Address3    = Address3,
                City        = City,
                County      = County,
                State       = State,
                Zip         = Zip,
                Country     = Country
            };
            return address;
        }
    }
}