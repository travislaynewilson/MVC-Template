using AutoMapper;
using RxLocal.Core.Domain.Customers;
using Customer = RxLocal.Data.Entities.Customer;

namespace RxLocal.Data.Profiles.Resolvers
{
    public class EfCustomerGenderResolver : IMemberValueResolver<Entities.Customer, Core.Domain.Customers.Customer, string, Gender>
    {
        public Gender Resolve(Customer source, Core.Domain.Customers.Customer destination, string sourceMember, Gender destMember, ResolutionContext context)
        {
            switch (source.Gender)
            {
                case "M":
                    return Gender.Male;
                case "F":
                    return Gender.Female;
                case "U":
                default:
                    return Gender.Unspecified;
            }
        }
    }
}