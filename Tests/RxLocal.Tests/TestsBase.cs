using System.Security.Principal;

namespace RxLocal.Tests
{
    public abstract class TestsBase
    {
        protected static IPrincipal CreatePrincipal(string name, params string[] roles)
        {
            return new GenericPrincipal(new GenericIdentity(name, "TestIdentity"), roles);
        }
    }
}
