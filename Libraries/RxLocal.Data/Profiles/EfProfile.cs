using System.Text.RegularExpressions;
using AutoMapper;
using RxLocal.Core.Domain.Common;
using RxLocal.Data.Profiles.Resolvers;
using Domain = RxLocal.Core.Domain;

namespace RxLocal.Data.Profiles
{
    public class EfProfile : Profile
    {
        public EfProfile()
        {
            CreateMap<Entities.Customer, Domain.Customers.Customer>()
                  .Ignore(c => c.TaxID)
                  .Ignore(c => c.Password)

                  .ForMember(target => target.FullName,
                      opt => opt.MapFrom(source => Regex.Replace(string.Join(" ",
                          source.FirstName,
                          source.MiddleName,
                          source.LastName,
                          source.NameSuffix), @"\s+", " ").Trim()))

                  .ForMember(target => target.DisplayName,
                      opt => opt.MapFrom(source =>
                          (!string.IsNullOrEmpty(source.Company))
                                ? source.Company
                                : Regex.Replace(string.Join(" ",
                                      source.FirstName,
                                      source.MiddleName,
                                      source.LastName,
                                      source.NameSuffix), @"\s+", " ").Trim()
                      ))

                  .ForMember(target => target.Gender,
                      opt => opt.ResolveUsing<EfCustomerGenderResolver, string>(src => src.Gender))

                  .ForMember(target => target.MainAddress,
                      opt => opt.MapFrom(source => new Address
                      {
                          AddressType = AddressType.Main,
                          Address1    = source.MainAddress1,
                          Address2    = source.MainAddress2,
                          Address3    = source.MainAddress3,
                          City        = source.MainCity,
                          County      = source.MainCounty,
                          State       = source.MainState,
                          Zip         = source.MainZip,
                          Country     = source.MainCountry
                      }))

                .ForMember(target => target.MailingAddress,
                      opt => opt.MapFrom(source => new Address
                      {
                          AddressType = AddressType.Mailing,
                          Address1    = source.MailAddress1,
                          Address2    = source.MailAddress2,
                          Address3    = source.MailAddress3,
                          City        = source.MailCity,
                          County      = source.MailCounty,
                          State       = source.MailState,
                          Zip         = source.MailZip,
                          Country     = source.MailCountry
                      }))

                .ForMember(target     => target.OtherAddress,
                      opt             => opt.MapFrom(source => new Address
                      {
                          AddressType = AddressType.Other,
                          Address1    = source.OtherAddress1,
                          Address2    = source.OtherAddress2,
                          Address3    = source.OtherAddress3,
                          City        = source.OtherCity,
                          County      = source.OtherCounty,
                          State       = source.OtherState,
                          Zip         = source.OtherZip,
                          Country     = source.OtherCountry
                      })).ReverseMap();

            CreateMap<Entities.CustomerType, Domain.Customers.CustomerType>().ReverseMap();

            CreateMap<Entities.CustomerStatus, Domain.Customers.CustomerStatus>().ReverseMap();
        }
    }
}
