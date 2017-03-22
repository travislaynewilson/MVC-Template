using System.Collections.Generic;
using RxLocal.Tests;
using NUnit.Framework;
using RxLocal.Core.Caching;

namespace RxLocal.Core.Tests.Caching
{
    [TestFixture]
    public class MemoryCacheManagerTests
    {
        [Test]
        public void CanSetAndGetObjectsFromCache()
        {
            var cacheManager = new MemoryCacheManager();
            cacheManager.Clear();

            // Ensure that simple objects can be cached
            cacheManager.Set("some_key_1", 3, int.MaxValue);
            cacheManager.Get<int>("some_key_1").ShouldEqual(3);

            // Ensure that collections can be cached
            cacheManager.Set("some_key_2", new List<int>() { 1, 2, 3 }, int.MaxValue);
            cacheManager.Get<List<int>>("some_key_2").ShouldNotBeNull();
            cacheManager.Get<List<int>>("some_key_2").Count.ShouldEqual(3);
        }

        [Test]
        public void CanOverwriteExistingItemsInCache()
        {
            var cacheManager = new MemoryCacheManager();
            cacheManager.Clear();

            cacheManager.Set("some_key_1", 3, int.MaxValue);
            cacheManager.Set("some_key_1", 99, int.MaxValue);

            cacheManager.Get<int>("some_key_1").ShouldEqual(99);
        }

        [Test]
        public void CanValidateWhetherObjectIsCached()
        {
            var cacheManager = new MemoryCacheManager();
            cacheManager.Clear();

            cacheManager.Set("some_key_1", 3, int.MaxValue);
            cacheManager.Set("some_key_2", 4, int.MaxValue);

            cacheManager.IsSet("some_key_1").ShouldEqual(true);
            cacheManager.IsSet("some_key_2").ShouldEqual(true);
            cacheManager.IsSet("some_key_3").ShouldEqual(false);
        }

        [Test]
        public void CanClearCache()
        {
            var cacheManager = new MemoryCacheManager();
            cacheManager.Set("some_key_99", 3, int.MaxValue);

            cacheManager.Clear();

            cacheManager.IsSet("some_key_99").ShouldEqual(false);
        }
    }
}
