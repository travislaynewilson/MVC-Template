using Autofac;
using RxLocal.Core.Infrastructure;
using RxLocal.Core.Infrastructure.DependencyManagement;

namespace RxLocal.Web.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order => 20;
    }
}
