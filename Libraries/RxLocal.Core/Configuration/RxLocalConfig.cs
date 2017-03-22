using System;
using System.Configuration;
using System.Xml;
using RxLocal.Core.Infrastructure;

namespace RxLocal.Core.Configuration
{
    /// <summary>
    /// Represents an RxLocalConfig
    /// </summary>
    public partial class RxLocalConfig : IConfigurationSectionHandler
    {
        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <param name="parent">Parent object.</param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        /// <returns>The created section handler object.</returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            // IOC Configuration
            var config = new RxLocalConfig();

            var dynamicDiscoveryNode = section.SelectSingleNode("dynamicDiscovery");
            if (dynamicDiscoveryNode?.Attributes != null)
            {
                bool enabled;
                if (TryGet(dynamicDiscoveryNode, "enabled", out enabled)) config.DynamicDiscovery = enabled;
            }

            // Engine Configuration
            var engineNode = section.SelectSingleNode("engine");
            if (engineNode?.Attributes != null)
            {
                string engineType;
                if (TryGet(engineNode, "type", out engineType)) config.EngineType = engineType;
            }

            // Startup Configuration
            var startupNode = section.SelectSingleNode("startup");
            if (startupNode?.Attributes != null)
            {
                bool ignore;
                if (TryGet(startupNode, "ignoreStartupTasks", out ignore)) config.IgnoreStartupTasks = ignore;
            }

            // API Configuration
            var apiNode = section.SelectSingleNode("api");
            if (apiNode?.Attributes != null)
            {
                string connectionString;
                if (TryGet(apiNode, "connectionString", out connectionString)) config.ConnectionString = connectionString;
            }

            return config;
        }

        /// <summary>
        /// In addition to configured assemblies examine and load assemblies in the bin directory.
        /// </summary>
        public bool DynamicDiscovery { get; private set; }

        /// <summary>
        /// A custom <see cref="IEngine"/> to manage the application instead of the default.
        /// </summary>
        public string EngineType { get; private set; }

        /// <summary>
        /// Indicates whether we should ignore startup tasks
        /// </summary>
        public bool IgnoreStartupTasks { get; private set; }

        /// <summary>
        /// The name of the connection string to use for sync SQL calls
        /// </summary>
        public string ConnectionString { get; set; }

        #region Helpers
        protected static bool TryGet<T>(XmlNode node, string attributeName, out T result)
        {
            var attribute = node.Attributes[attributeName];
            if (attribute != null)
            {
                result = CommonHelper.To<T>(attribute.Value);
                return true;
            }

            result = default(T);
            return false;
        }
        #endregion
    }
}
