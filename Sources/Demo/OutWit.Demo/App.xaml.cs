using OutWit.Common.Configuration;
using OutWit.Common.Interfaces;
using OutWit.Common.Logging.Interfaces;
using OutWit.Demo.Interfaces;
using OutWit.Demo.Properties;
using OutWit.Demo.Services;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using System.Linq;
using Prism.Modularity;
using System.IO;
using OutWit.Common.Prism;
using OutWit.Common.Utils;
using Prism.Container.Unity;
using OutWit.Demo.Views;
using System.Threading.Tasks;

namespace OutWit.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Constants

        private const string MODULE_FILTER = "*.module";

        private const int SPLASH_SCREEN_DELAY = 300;

        #endregion

        #region Externs

        // Pinvoke declaration for ShowWindow
        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion

        #region Constructors

        public App()
        {
            InitServices();
            InitEvents();
        }

        #endregion

        #region Initialization

        private void InitServices()
        {
            ServiceLocator.Get.Register<IResourcesManager>(new ResourcesManager());
            ServiceLocator.Get.Register<IResources>(new ResourcesService());
            ServiceLocator.Get.Register<ISettings>(new SettingsService());
            ServiceLocator.Get.Register<ILogManager>(new LogManager());
            ServiceLocator.Get.Register<ILogger>(ServiceLocator.Get.LogManager.LoggerFactory.CreateLogger(nameof(OutWit.Demo)));
        }

        private void InitEvents()
        {
        }

        #endregion

        #region Prism Application

        protected override Window CreateShell()
        {
            if (ServiceLocator.Get.Settings.ShowSplashScreen)
            {
                SplashScreen = new Views.SplashScreen();
                SplashScreen.Show();
            }

            Window = new MainWindow();
            Window.Loaded += OnMainWindowLoaded;

            return Window;
        }

        protected override IContainerExtension CreateContainerExtension()
        {
            return new UnityContainerExtension(ServiceLocator.Get.Container);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var modulePath = Assembly.GetExecutingAssembly().AssemblyDirectory().AppendPath(ServiceLocator.Get.Settings.ModuleFolder);

            if (!Directory.Exists(modulePath))
                Directory.CreateDirectory(modulePath);

            return new DirectoriesModuleCatalog(modulePath, MODULE_FILTER);
        }

        #endregion

        #region Event Handlers

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (SplashScreen != null)
                Task.Delay(SPLASH_SCREEN_DELAY).ContinueWith(t =>
                {
                    Dispatcher.Invoke(() => SplashScreen.Close());
                });

        }

        #endregion

        #region Properties

        private Views.SplashScreen SplashScreen { get; set; }

        private MainWindow Window { get; set; }

        #endregion
    }

}
