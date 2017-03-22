using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RxLocal.Tests;
using NUnit.Framework;
using RxLocal.Core.ComponentModel;

namespace RxLocal.Core.Tests.ComponentModel
{
    [TestFixture]
    public class GenericListTypeConverter
    {
        [SetUp]
        public void SetUp()
        {
            TypeDescriptor.AddAttributes(typeof(IList<int>), new TypeConverterAttribute(typeof(GenericListTypeConverter<int>)));
            TypeDescriptor.AddAttributes(typeof(IList<decimal>), new TypeConverterAttribute(typeof(GenericListTypeConverter<decimal>)));
            TypeDescriptor.AddAttributes(typeof(IList<double>), new TypeConverterAttribute(typeof(GenericListTypeConverter<double>)));
            TypeDescriptor.AddAttributes(typeof(IList<string>), new TypeConverterAttribute(typeof(GenericListTypeConverter<string>)));
        }

        [Test]
        public void CanGetIntListTypeConverter()
        {
            var validSourceTypes = new[] { typeof(int[]), typeof(List<int>), typeof(ReadOnlyCollection<int>), typeof(Collection<int>) };

            foreach (var validSourceType in validSourceTypes)
            {
                TypeDescriptor.GetConverter(validSourceType).GetType().ShouldEqual(typeof(GenericListTypeConverter<int>));
            }
        }

        [Test]
        public void CanGetStringListTypeConverter()
        {
            var validSourceTypes = new[] { typeof(string[]), typeof(List<string>), typeof(ReadOnlyCollection<string>), typeof(Collection<string>) };

            foreach (var validSourceType in validSourceTypes)
            {
                TypeDescriptor.GetConverter(validSourceType).GetType().ShouldEqual(typeof(GenericListTypeConverter<string>));
            }
        }

        [Test]
        public void CanGetDecimalListTypeConverter()
        {
            var validSourceTypes = new[] { typeof(decimal[]), typeof(List<decimal>), typeof(ReadOnlyCollection<decimal>), typeof(Collection<decimal>) };

            foreach (var validSourceType in validSourceTypes)
            {
                TypeDescriptor.GetConverter(validSourceType).GetType().ShouldEqual(typeof(GenericListTypeConverter<decimal>));
            }
        }

        [Test]
        public void CanGetDoubleListTypeConverter()
        {
            var validSourceTypes = new[] { typeof(double[]), typeof(List<double>), typeof(ReadOnlyCollection<double>), typeof(Collection<double>) };

            foreach (var validSourceType in validSourceTypes)
            {
                TypeDescriptor.GetConverter(validSourceType).GetType().ShouldEqual(typeof(GenericListTypeConverter<double>));
            }
        }

        [Test]
        public void CanGetIntListFromString()
        {
            var items = "10,20,30,40,50";
            var converter = TypeDescriptor.GetConverter(typeof(List<int>));
            var result = converter.ConvertFrom(items) as IList<int>;
            result.ShouldNotBeNull();
            result.Count.ShouldEqual(5);
        }

        [Test]
        public void CanGetStringListFromString()
        {
            var items = "foo, bar, day";
            var converter = TypeDescriptor.GetConverter(typeof(List<string>));
            var result = converter.ConvertFrom(items) as List<string>;
            result.ShouldNotBeNull();
            result.Count.ShouldEqual(3);
        }

        [Test]
        public void CanGetDecimalListFromString()
        {
            var items = "0.01, 0.02, 0.03";
            var converter = TypeDescriptor.GetConverter(typeof(List<decimal>));
            var result = converter.ConvertFrom(items) as List<decimal>;
            result.ShouldNotBeNull();
            result.Count.ShouldEqual(3);
        }

        [Test]
        public void CanGetDoubleListFromString()
        {
            var items = "0.01, 0.02, 0.03";
            var converter = TypeDescriptor.GetConverter(typeof(List<double>));
            var result = converter.ConvertFrom(items) as List<double>;
            result.ShouldNotBeNull();
            result.Count.ShouldEqual(3);
        }

        [Test]
        public void CanConvertIntListToString()
        {
            var items = new List<int> { 10, 20, 30, 40, 50 };
            var converter = TypeDescriptor.GetConverter(items.GetType());
            var result = converter.ConvertTo(items, typeof(string)) as string;

            result.ShouldNotBeNull();
            result.ShouldEqual("10,20,30,40,50");
        }

        [Test]
        public void CanConvertStringListToString()
        {
            var items = new List<string> { "foo", "bar", "day" };
            var converter = TypeDescriptor.GetConverter(items.GetType());
            var result = converter.ConvertTo(items, typeof(string)) as string;

            result.ShouldNotBeNull();
            result.ShouldEqual("foo,bar,day");
        }

        [Test]
        public void CanConvertStringListToDecimal()
        {
            var items = new List<decimal> { 0.01M, 0.2M, 0.3000M, 1M };
            var converter = TypeDescriptor.GetConverter(items.GetType());
            var result = converter.ConvertTo(items, typeof(string)) as string;

            result.ShouldNotBeNull();
            result.ShouldEqual("0.01,0.2,0.3000,1");
        }

        [Test]
        public void CanConvertStringListToDouble()
        {
            var items = new List<double> { 0.01, 0.2, 0.3000, 1 };
            var converter = TypeDescriptor.GetConverter(items.GetType());
            var result = converter.ConvertTo(items, typeof(string)) as string;

            result.ShouldNotBeNull();
            result.ShouldEqual("0.01,0.2,0.3,1");
        }
    }
}
