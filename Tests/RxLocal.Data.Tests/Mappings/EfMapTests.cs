using NUnit.Framework;
using AutoMapper;
using RxLocal.Tests;
using RxLocal.Core.Domain.Common;
using RxLocal.Core.Domain.Customers;
using RxLocal.Data.Profiles;

namespace RxLocal.Data.Tests.Mappings
{
    [TestFixture]
    public class EfMapTests
    {
        public IMapper EfMapper { get; set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => 
                cfg.AddProfile<EfProfile>()
            );

            EfMapper = config.CreateMapper();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            EfMapper = null;
        }


        [Test] 
        public void Map_EfConfigurationIsValid()
        {
            EfMapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void Map_CustomerGenderResolver()
        {
            var source = new Entities.Customer {Gender = "M"};
            var target = EfMapper.Map<Entities.Customer, Customer>(source);
            target.Gender.ShouldEqual(Gender.Male);

            source.Gender = "F";
            target = EfMapper.Map<Entities.Customer, Customer>(source);
            target.Gender.ShouldEqual(Gender.Female);

            source.Gender = "U";
            target = EfMapper.Map<Entities.Customer, Customer>(source);
            target.Gender.ShouldEqual(Gender.Unspecified);

            source.Gender = "47";
            target = EfMapper.Map<Entities.Customer, Customer>(source);
            target.Gender.ShouldEqual(Gender.Unspecified);
        }

        [Test]
        public void Map_CustomerDisplayName()
        {
            var source = new Entities.Customer { Company = "Acme", FirstName = "John", LastName = "Doe" };
            var target = EfMapper.Map<Entities.Customer, Customer>(source);
            target.DisplayName.ShouldEqual("Acme");

            source.Company = string.Empty;
            target = EfMapper.Map<Entities.Customer, Customer>(source);
            target.DisplayName.ShouldEqual("John Doe");
        }

        [Test]
        public void Map_CustomerAddressTypes()
        {
            var source = new Entities.Customer();
            var target = EfMapper.Map<Entities.Customer, Customer>(source);

            target.MainAddress.AddressType.ShouldEqual(AddressType.Main);
            target.MailingAddress.AddressType.ShouldEqual(AddressType.Mailing);
            target.OtherAddress.AddressType.ShouldEqual(AddressType.Other);
        }
    }
}
