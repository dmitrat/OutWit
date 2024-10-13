using System;
using Microsoft.Extensions.Logging;
using OutWit.Common.Controls.Interfaces;
using OutWit.Common.Interfaces;
using OutWit.Common.Logging.Interfaces;
using OutWit.Common.Prism.Interfaces;
using OutWit.Common.Services;
using OutWit.Common.Settings.Interfaces;
using OutWit.Demo.WitEngine.Interfaces;
using OutWit.Engine.Data.Interfaces;
using OutWit.Engine.Interfaces;
using OutWit.Themes.Interfaces;

namespace OutWit.Demo.WitEngine
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

        public IResources Resources => Resolve<IResources>();

        public IResourcesManager ResourcesManager => Resolve<IResourcesManager>();

        public ILogger Logger => Resolve<ILogger>();
        
        public ILogManager LogManager => Resolve<ILogManager>();

        public ISettings Settings => Resolve<ISettings>();
        
        public ISettingsManager SettingsManager => Resolve<ISettingsManager>();
        
        public IPopupManager PopupManager => Resolve<IPopupManager>();
        
        public IWindowManager WindowManager => Resolve<IWindowManager>();
        
        public INavigationManager NavigationManager => Resolve<INavigationManager>();

        public IWItEngineManager EngineManager => Resolve<IWItEngineManager>();

        public IThemeContainerBase ThemeContainer => Resolve<IThemeContainerBase>();
        
        #endregion
    }
}
