using System.Linq;
using System.Web;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using RxLocal.Core;
using RxLocal.Core.Caching;
using RxLocal.Core.Fakes;
using RxLocal.Core.Infrastructure;
using RxLocal.Core.Infrastructure.DependencyManagement;
using RxLocal.Data;
using RxLocal.Services;
using RxLocal.Services.Authentication;
using RxLocal.Services.Customers;

namespace RxLocal.Web.Framework.Infrastructure
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
            // HTTP context and other related stuff
            // Register FakeHttpContext when HttpContext is not available
            builder.Register(c => HttpContext.Current != null ? (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) : (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<HttpContextBase>().Request).As<HttpRequestBase>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response).As<HttpResponseBase>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server).As<HttpServerUtilityBase>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session).As<HttpSessionStateBase>().InstancePerLifetimeScope();

            // Work Context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("rxl_cache_per_request"))
                .InstancePerLifetimeScope();

            // Web Helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            // Controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            // Cache Managers
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("rxl_cache_static").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("rxl_cache_per_request").InstancePerLifetimeScope();
            builder.RegisterType<NullCacheManager>().As<ICacheManager>().Named<ICacheManager>("rxl_cache_none").InstancePerLifetimeScope();

            // Repositories & Data Managers
            builder.RegisterType<RxLocalSqlDataManager>().As<IDataManager>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<RxLocalServiceManager>().As<IServiceManager>().InstancePerLifetimeScope();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order => 0;
    }
}
