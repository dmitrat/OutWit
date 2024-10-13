using System;
using System.Reflection;
using OutWit.Common.Interfaces;
using OutWit.Common.MEF;
using OutWit.Common.Utils;
using OutWit.Themes.Interfaces;
using OutWit.Themes.Services;

namespace OutWit.Themes
{
    public class Selector
    {
        #region Constants

        private const string DEFAULT_MODULE_PATH = "@Themes";

        #endregion

        #region Static Fields

        private static volatile Selector m_instance = null;
        private static readonly object m_syncRoot = new Object();

        #endregion

        #region Constructors

        private Selector()
        {
            ServiceLocator.Get.Register<IThemeContainer>(new ThemeContainer());
        }

        #endregion

        #region Functions

        public void Reload(IResourcesManager resourcesManager, string moduleFolder = null)
        {
            if (string.IsNullOrEmpty(moduleFolder))
                moduleFolder = Assembly.GetExecutingAssembly().AssemblyDirectory().AppendPath(DEFAULT_MODULE_PATH);

            if (resourcesManager != null)
                ServiceLocator.Get.Register<IResourcesManager>(resourcesManager);

            ThemeContainer.Clear();

            Bootstrapper = new Bootstrapper<IThemeModule>(ServiceLocator.Get, moduleFolder, true);
            Bootstrapper.Run();
        }

        #endregion

        #region Properties

        public IThemeContainer ThemeContainer => ServiceLocator.Get.ThemeContainer;

        private Bootstrapper<IThemeModule> Bootstrapper { get; set; }

        #endregion

        #region Static Properties

        public static Selector Get
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new Selector();
                    }
                }

                return m_instance;
            }
        }

        #endregion
    }
}
