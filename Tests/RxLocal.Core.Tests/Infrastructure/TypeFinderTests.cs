using NUnit.Framework;
using System.Linq;
using RxLocal.Tests;
using RxLocal.Core.Infrastructure;

namespace RxLocal.Core.Tests.Infrastructure
{
    [TestFixture]
    public class TypeFinderTests
    {
        [Test]
        public void TypeFinderBenchmarkFindings()
        {
            var finder = new AppDomainTypeFinder();

            var type = finder.FindClassesOfType<ISomeInterface>();
            type.Count().ShouldEqual(1);
            typeof(ISomeInterface).IsAssignableFrom(type.FirstOrDefault()).ShouldBeTrue();
        }

        public interface ISomeInterface
        {
        }

        public class SomeClass : ISomeInterface
        {
        }
    }

}
