using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using RxLocal.Core.Configuration;
using RxLocal.Core.Infrastructure.DependencyManagement;

namespace RxLocal.Core.Infrastructure
{
    /// <summary>
    /// Engine
    /// </summary>
    public class RxLocalEngine : IEngine
    {
        #region Fields

        private ContainerManager _containerManager;

        #endregion

        #region Utilities

        /// <summary>
        /// Run startup tasks
        /// </summary>
        protected virtual void RunStartupTasks()
        {
            var typeFinder       = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks     = startUpTaskTypes.Select(t => (IStartupTask) Activator.CreateInstance(t)).ToList();

            // Order the tasks by priority
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();

            // Run each startup task
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        /// <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="config">Config</param>
        protected virtual void RegisterDependencies(RxLocalConfig config)
        {
            var builder = new ContainerBuilder();

            // Register dependencies
            var typeFinder = new WebAppTypeFinder(config);
            builder        = new ContainerBuilder();

            builder.RegisterInstance(config).As<RxLocalConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            // Register any dependencies provided by other assemblies
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = drTypes.Select(drType => (IDependencyRegistrar) Activator.CreateInstance(drType)).ToList();

            // Order the dependency registrars by priority
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);


            var container = builder.Build();
            this._containerManager = new ContainerManager(container);

            // Set the dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Initialize components and plugins in the RxLocal environment.
        /// </summary>
        /// <param name="config">Config</param>
        public void Initialize(RxLocalConfig config)
        {
            //register dependencies
            RegisterDependencies(config);

            //startup tasks
            if (!config.IgnoreStartupTasks)
            {
                RunStartupTasks();
            }
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
		{
            return ContainerManager.Resolve<T>();
		}

        /// <summary>
        /// Resolve named dependency
        /// </summary>
        /// <param name="name">The name of the registered depedency</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T ResolveNamed<T>(string name) where T : class
        {
            return ContainerManager.ResolveNamed<T>(name);
        }

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }
        
        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

		#endregion

        #region Properties

        /// <summary>
        /// Container manager
        /// </summary>
        public ContainerManager ContainerManager => _containerManager;

        #endregion
    }
}
