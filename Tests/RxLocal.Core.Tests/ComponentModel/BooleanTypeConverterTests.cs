using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RxLocal.Tests;
using NUnit.Framework;
using RxLocal.Core.ComponentModel;

namespace RxLocal.Core.Tests.ComponentModel
{
    [TestFixture]
    public class BooleanTypeConverterTests
    {
        [SetUp]
        public void SetUp()
        {
            TypeDescriptor.AddAttributes(typeof(bool), new TypeConverterAttribute(typeof(BooleanTypeConverter)));
        }

        [Test]
        public void CanGetBooleanTypeConverter()
        {
            var validSourceTypes = new[] { typeof(bool) };

            foreach (var validSourceType in validSourceTypes)
            {
                TypeDescriptor.GetConverter(validSourceType).GetType().ShouldEqual(typeof(BooleanTypeConverter));
            }
        }

        [Test]
        public void CanGetTrueBooleanFromString()
        {
            var values    = new[] {"true", "yes", "1", "on", "active"};
            var converter = TypeDescriptor.GetConverter(typeof(bool));

            foreach (var value in values)
            {
                var result = (bool)converter.ConvertFrom(value);

                result.ShouldNotBeNull();
                result.ShouldBeTrue();
            }
        }

        [Test]
        public void CanGetFalseBooleanFromString()
        {
            var values    = new[] { "false", "no", "0", "off", "inactive", "", string.Empty, null };
            var converter = TypeDescriptor.GetConverter(typeof(bool));

            foreach (var value in values)
            {
                var result = (bool)converter.ConvertFrom(value);

                result.ShouldNotBeNull();
                result.ShouldBeFalse();
            }
        }
    }
}
