using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using RxLocal.Core.Configuration;

namespace RxLocal.Core.Infrastructure
{
    /// <summary>
    /// Provides access to the singleton instance of the RxLocal engine.
    /// </summary>
    public class EngineContext
    {
        #region Utilities

        /// <summary>
        /// Creates a factory instance and adds a http application injecting facility.
        /// </summary>
        /// <param name="config">Config</param>
        /// <returns>New engine instance</returns>
        protected static IEngine CreateEngineInstance(RxLocalConfig config)
        {
            if (config != null && !string.IsNullOrEmpty(config.EngineType))
            {
                var engineType = Type.GetType(config.EngineType);
                if (engineType == null)
                    throw new ConfigurationErrorsException("The type '" + config.EngineType + "' could not be found. Please check the configuration at /configuration/RxLocal/engine[@engineType] or check for missing assemblies.");
                if (!typeof(IEngine).IsAssignableFrom(engineType))
                    throw new ConfigurationErrorsException("The type '" + engineType + "' doesn't implement 'RxLocal.Core.Infrastructure.IEngine' and cannot be configured in /configuration/RxLocal/engine[@engineType] for that purpose.");
                return Activator.CreateInstance(engineType) as IEngine;
            }

            return new RxLocalEngine();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a static instance of the RxLocal factory.
        /// </summary>
        /// <param name="forceRecreate">Creates a new factory instance even though the factory has been previously initialized.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                var sectionName = "RxLocalConfig";
                var section = ConfigurationManager.GetSection(sectionName);
                if (section == null)
                {
                    throw new ConfigurationErrorsException($"A required \"{sectionName}\" configuration section could not be found for this application.");
                }

                var config = section as RxLocalConfig;

                Singleton<IEngine>.Instance = CreateEngineInstance(config);

                Singleton<IEngine>.Instance.Initialize(config);
            }
            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton RxLocal engine used to access RxLocal services.
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }

        #endregion
    }
}
