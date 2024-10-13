using OutWit.Common.Configuration;
using OutWit.Common.Controls.Interfaces;
using OutWit.Common.Interfaces;
using OutWit.Common.Logging.Interfaces;
using OutWit.Common.Prism.Interfaces;
using OutWit.Common.Settings.Interfaces;
using OutWit.Demo.WitEngine.Properties;
using OutWit.Demo.WitEngine.Views;
using Prism.Ioc;
using Prism.Navigation.Regions;
using System.Reflection;
using Microsoft.Extensions.Logging;
using OutWit.Demo.WitEngine.Interfaces;
using OutWit.Demo.WitEngine.Services;
using OutWit.Engine.Data.Interfaces;
using OutWit.Engine.Interfaces;
using OutWit.Themes.Interfaces;
using Prism.Modularity;

namespace OutWit.Demo.WitEngine
{
    public class WitEngineModule : IModule
    {
        #region IModule

        public void OnInitialized(IContainerProvider containerProvider)
        {
            InitServices(containerProvider);
            InitEvents();
            InitResources();
            InitSettings();
            InitView();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        #endregion

        #region Initialization

        private void InitServices(IContainerProvider containerProvider)
        {
            ServiceLocator.Get.Register(containerProvider.Resolve<IRegionManager>());
            ServiceLocator.Get.Register(containerProvider.Resolve<INavigationManager>());
            ServiceLocator.Get.Register(containerProvider.Resolve<ISettingsManager>());
            ServiceLocator.Get.Register(containerProvider.Resolve<IKeyboardManager>());
            ServiceLocator.Get.Register(containerProvider.Resolve<IResourcesManager>());
            ServiceLocator.Get.Register(containerProvider.Resolve<IPopupManager>());
            ServiceLocator.Get.Register(containerProvider.Resolve<IWindowManager>());
            ServiceLocator.Get.Register(containerProvider.Resolve<IWItEngineManager>());
            ServiceLocator.Get.Register(containerProvider.Resolve<IThemeContainerBase>());

            ServiceLocator.Get.Register<IResources>(new ResourcesBase<Resources>(Assembly.GetExecutingAssembly()));
            ServiceLocator.Get.Register<ISettings>(new SettingsService());

            ServiceLocator.Get.Register<ILogger>(containerProvider.Resolve<ILogManager>().LoggerFactory.CreateLogger(nameof(OutWit.Demo.WitEngine)));
        }

        private void InitEvents()
        {
            ServiceLocator.Get.WindowManager.WindowLoaded += OnWindowLoaded;
        }

        private void InitResources()
        {
            ServiceLocator.Get.ResourcesManager.AddResourceDictionary(ServiceLocator.Get.Resources);
        }

        private void InitSettings()
        {
            ServiceLocator.Get.SettingsManager.AddCollection(ServiceLocator.Get.Settings.General);
            ServiceLocator.Get.SettingsManager.AddConfiguration(ServiceLocator.Get.Settings);
        }

        private void InitView()
        {
            ServiceLocator.Get.NavigationManager.Register<Navigation>("WitEngine");

            ServiceLocator.Get.NavigationManager.Register<Views.JobEditor>();
        }

        #endregion

        #region Event Handlers

        private void OnWindowLoaded()
        {
            //ServiceLocator.Get.NavigationManager.Register<MenuViewAnalysis>(MenuRoots.View);
        }

        #endregion
    }
}
