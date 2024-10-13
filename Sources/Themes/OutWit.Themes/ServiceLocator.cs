using System;
using System.Collections.Generic;
using System.Text;
using OutWit.Common.Interfaces;
using OutWit.Common.Services;
using OutWit.Themes.Interfaces;

namespace OutWit.Themes
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

        public static ServiceLocator Get
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new ServiceLocator();
                    }
                }

                return m_instance;
            }
        }

        #endregion

        #region Services

        public IThemeContainer ThemeContainer => Resolve<IThemeContainer>();

        public IResourcesManager ResourcesManager => Resolve<IResourcesManager>();


        #endregion
    }
}
