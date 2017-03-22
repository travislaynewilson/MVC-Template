using System;
using System.Globalization;
using RxLocal.Tests;
using NUnit.Framework;

namespace RxLocal.Core.Tests
{
    [TestFixture]
    public class CommonHelperTests
    {
        [Test]
        public void CanCoalesce()
        {
            CommonHelper.Coalesce(null, string.Empty, "", "some value").ShouldEqual("some value");
            CommonHelper.Coalesce(null, string.Empty, "").ShouldEqual(string.Empty);
        }

        [Test]
        public void CanMask()
        {
            CommonHelper.Mask("1234567890", 4, '*').ShouldEqual("******7890");
            CommonHelper.Mask("1234", 4, '*').ShouldEqual("1234");
            CommonHelper.Mask("1234567890", 1, '^').ShouldEqual("^^^^^^^^^0");
            CommonHelper.Mask("", 4, '*').ShouldEqual("");
        }

        [Test]
        public void CanValidateEmail()
        {
            // Valid emails
            CommonHelper.IsValidEmail("a@a.a").ShouldBeTrue();
            CommonHelper.IsValidEmail("validemail364901@gmail.com").ShouldBeTrue();
            CommonHelper.IsValidEmail("validEmailWithCasing@gmail.com").ShouldBeTrue();
            CommonHelper.IsValidEmail("validemail+label@gmail.com").ShouldBeTrue();
            CommonHelper.IsValidEmail("1valid.email_with-symbols@gmail.com").ShouldBeTrue();

            // Invalid emails
            CommonHelper.IsValidEmail(null).ShouldBeFalse();
            CommonHelper.IsValidEmail(string.Empty).ShouldBeFalse();
            CommonHelper.IsValidEmail("@invalid.email").ShouldBeFalse();
            CommonHelper.IsValidEmail("invalidemail@gmail").ShouldBeFalse();
            CommonHelper.IsValidEmail("invalidemailgmail.com").ShouldBeFalse();
            CommonHelper.IsValidEmail("invalid email with spaces@gmail.com").ShouldBeFalse();
            CommonHelper.IsValidEmail("reallyinvalidemail").ShouldBeFalse();
        }

        [Test]
        public void CanGenerateRandomDigitCode()
        {
            // Ensure that the generated string's length is valid.
            CommonHelper.GenerateRandomDigitCode(5).Length.ShouldEqual(5);

            // Attempt to parse the generated string as an int.
            var parsedValue = -1;
            var parsed = int.TryParse(CommonHelper.GenerateRandomDigitCode(1), out parsedValue);

            parsed.ShouldBeTrue();
            parsedValue.ShouldBeGreaterThanOrEqual(0);
        }

        [Test]
        public void CanGenerateRandomInteger()
        {
            // Ensure that the generated int is at least 0.
            const int min = 0;
            const int max = 10;
            var generatedInt = CommonHelper.GenerateRandomInteger(min, max);

            generatedInt.ShouldBeGreaterThanOrEqual(min);
            generatedInt.ShouldBeLessThanOrEqual(max);
        }

        [Test]
        public void CanEnsureMaximumLength()
        {
            const string title = "abcdefghijklmnopqrstuvwxyz";

            // Ensure the truncation works
            var truncatedTitle = CommonHelper.EnsureMaximumLength(title, 5);
            truncatedTitle.ShouldEqual("abcde");

            // Ensure that the postfix works
            var truncatedTitleWithPostfix = CommonHelper.EnsureMaximumLength(title, 7, "...");
            truncatedTitleWithPostfix.ShouldEqual("abcdefg...");
        }

        [Test]
        public void CanEnsureNumericOnly()
        {
            // Ensure the filter works
            CommonHelper.EnsureNumericOnly("a1b2c3").ShouldEqual("123");

            // Ensure the filter works with a string containing no numbers
            CommonHelper.EnsureNumericOnly("abcdef").ShouldEqual(string.Empty);

            // Ensure the filter works with an empty string
            CommonHelper.EnsureNumericOnly(string.Empty).ShouldEqual(string.Empty);
        }

        [Test]
        public void CanEnsureNotNull()
        {
            string nullString = null;

            // Ensure the checker works
            CommonHelper.EnsureNotNull(nullString).ShouldEqual(string.Empty);
            CommonHelper.EnsureNotNull(string.Empty).ShouldEqual(string.Empty);
            CommonHelper.EnsureNotNull("").ShouldEqual(string.Empty);

            // Ensure the checker works with a valid string
            CommonHelper.EnsureNotNull("abcdef").ShouldEqual("abcdef");
        }

        [Test]
        public void CanEnsureStringsAreNullOrEmpty()
        {
            // Ensure the checker can identify null or empty strings
            CommonHelper.AreNullOrEmpty("", "b", "c").ShouldBeTrue();
            CommonHelper.AreNullOrEmpty("a", string.Empty, "c").ShouldBeTrue();
            CommonHelper.AreNullOrEmpty("a", "b", null).ShouldBeTrue();

            // Ensure the checker handles valid strings as well
            CommonHelper.AreNullOrEmpty("a", "b", "c").ShouldBeFalse();
        }

        [Test]
        public void CanEnsureArraysEqual()
        {
            // Ensure the checker can verify that two arrays are equal
            CommonHelper.ArraysEqual(new int[] {}, new int[] {}).ShouldBeTrue();
            CommonHelper.ArraysEqual(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }).ShouldBeTrue();

            // Ensure the checker can verify that arrays can not be equal
            CommonHelper.ArraysEqual(new[] { 1 }, new[] { 1, 2 }).ShouldBeFalse();
            CommonHelper.ArraysEqual(new[] { 1, 2, 3 }, new[] { 3, 2, 1 }).ShouldBeFalse();
        }


        [Test]
        public void CanSetPropertyToClassInstance()
        {
            var testClass = new TestCustomer()
            {
                CustomerID = 0
            };

            // Ensure you can set a property on a class instance.
            CommonHelper.SetProperty(testClass, "CustomerID", 5);
            testClass.CustomerID.ShouldEqual(5);

            // Ensure you cannot set a read-only property on a class instance.
            typeof(RxLocalException).ShouldBeThrownBy(() =>
            {
                CommonHelper.SetProperty(testClass, "DisplayName", "some value");
            });

            // Ensure you cannot set a non-existant property on a class instance.
            typeof(RxLocalException).ShouldBeThrownBy(() =>
            {
                CommonHelper.SetProperty(testClass, "SomeNonExistantProperty", "some value");
            });
        }

        [Test]
        public void CanConvertToTypes()
        {
            var x1 = CommonHelper.To<int>("5");
            x1.ShouldBe<int>();
            x1.ShouldEqual(5);

            var x2 = CommonHelper.To<string>(5);
            x2.ShouldBe<string>();
            x2.ShouldEqual("5");

            var x3 = CommonHelper.To<double>("5.53");
            x3.ShouldBe<double>();
            x3.ShouldEqual(5.53);

            var x4 = CommonHelper.To<string>(5.53);
            x4.ShouldBe<string>();
            x4.ShouldEqual("5.53");

            var x5 = CommonHelper.To<DateTime>("12/28/2015");
            x5.ShouldBe<DateTime>();
            x5.Month.ShouldEqual(12);
            x5.Day.ShouldEqual(28);
        }

        [Test]
        public void CanConvertToTypesUsingSpecificCulture()
        {
            var x1 = CommonHelper.To<decimal>("5.31", CultureInfo.CreateSpecificCulture("en-US"));
            x1.ShouldBe<decimal>();
            x1.ShouldEqual(5.31);

            var x2 = CommonHelper.To<decimal>("5,31", CultureInfo.CreateSpecificCulture("fr-FR"));
            x2.ShouldBe<decimal>();
            x2.ShouldEqual(5.31);

            var x3 = CommonHelper.To<DateTime>("12/28/2015", CultureInfo.CreateSpecificCulture("en-US"));
            x3.ShouldBe<DateTime>();
            x3.Month.ShouldEqual(12);
            x3.Day.ShouldEqual(28);

            var x4 = CommonHelper.To<DateTime>("28/12/2015", CultureInfo.CreateSpecificCulture("en-GB"));
            x4.ShouldBe<DateTime>();
            x4.Month.ShouldEqual(12);
            x4.Day.ShouldEqual(28);
        }

        [Test]
        public void CanGetEnumDisplayName()
        {
            // Ensure that a string can be converted
            CommonHelper.GetEnumDisplayName("MyEnumDescription").ShouldEqual("My Enum Description");

            // Ensure that an enum can be converted
            CommonHelper.GetEnumDisplayName(TestEnum.FooBarBaz).ShouldEqual("Foo Bar Baz");
        }


        public class TestCustomer
        {
            public int CustomerID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string DisplayName
            {
                get { return FirstName + " " + LastName; }
            }
        }

        public enum TestEnum
        {
            Foo = 1,
            Bar = 2,
            Baz = 3,
            FooBarBaz = 4
        }
    }
}
