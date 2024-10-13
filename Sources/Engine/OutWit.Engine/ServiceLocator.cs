using System;
using Microsoft.Extensions.Logging;
using OutWit.Common.Interfaces;
using OutWit.Common.Services;
using OutWit.Engine.Services;
using OutWit.Engine.Shared.Interfaces;

namespace OutWit.Engine
{
    internal class ServiceLocator : ServiceLocatorBase
    {
        #region Static Fields

        private static volatile ServiceLocator m_instance = null;
        private static readonly object m_syncRoot = new object();

        #endregion

        #region Constructors

        private ServiceLocator()
        {
        }

        ~ServiceLocator()
        {
            if (!IsDisposed)
                Dispose();
        }

        #endregion

        #region Static Properties

        /// <summary>
        /// Class singleton instance
        /// </summary>
        public static ServiceLocator Get
        {
            get
            {
                if (m_instance != null)
                    return m_instance;

                m_instance = new ServiceLocator();
                return m_instance;
            }
        }

        #endregion

        #region Services

        public IWitResources Resources => Resolve<IWitResources>();

        public IResourcesManager ResourcesManager => Resolve<IResourcesManager>();

        public ILogger Logger => Resolve<ILogger>();

        public IWitProcessingManager ProcessingManager => Resolve<IWitProcessingManager>();

        public IWitControllerManager ControllerManager => Resolve<IWitControllerManager>();

        #endregion
    }
}
