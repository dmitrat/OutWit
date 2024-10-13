using System.Linq;
using System.Net.Mime;
using System.Windows;
using OutWit.Common.Controls.Interfaces;
using OutWit.Common.Controls.Windows;
using OutWit.Common.Logging.Aspects;
using OutWit.Common.MVVM.Utils;
using OutWit.Common.Prism.Interfaces;
using OutWit.Common.Settings.Interfaces;
using OutWit.Demo.Services;
using OutWit.Demo.Views;
using OutWit.Engine.Data.Interfaces;
using OutWit.Engine.Interfaces;
using OutWit.Themes.Interfaces;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using Unity;

namespace OutWit.Demo.ViewModels
{
    public class ApplicationViewModel : BindableBase
    {
        #region Constructors

        public ApplicationViewModel(MainWindow window)
        {
            Window = window;

            InitEvents();
            InitServices();
            InitSettings();
            InitViewModels();
        }

        #endregion

        #region Initialization

        private void InitEvents()
        {
            Window.Loaded += OnWindowLoaded;
        }

        private void InitServices()
        {
            ServiceLocator.Get.Register<IWindowManager>(new WindowManager(Window));
            ServiceLocator.Get.Register<IKeyboardManager>(new KeyboardManager(Window));
            ServiceLocator.Get.Register<INavigationManager>(new NavigationManager());
            ServiceLocator.Get.Register<ISettingsManager>(new SettingsManager());
            ServiceLocator.Get.Register<IPopupManager>(new PopupManager());
            ServiceLocator.Get.Register<IThemeContainerBase>(new ThemeManager());

            ServiceLocator.Get.Register<IWItEngineManager>(new EngineManager());
        }

        private void InitViewModels()
        {
            NavigationVm = new NavigationViewModel(this);
            HeaderPanelVm = new HeaderPanelViewModel(this);
            ThemesVm = new ThemesViewModel(this);
            LanguagesVm = new LanguagesViewModel(this);
            HeaderMenuVm = new HeaderMenuViewModel(this);
            HeaderToolbarVm = new HeaderToolbarViewModel(this);
            SettingsVm = new SettingsViewModel(this);
            WindowStateVm = new WindowStateViewModel(this);
        }

        private void InitSettings()
        {
            ServiceLocator.Get.SettingsManager.AddCollection(ServiceLocator.Get.Settings.General);
            ServiceLocator.Get.SettingsManager.AddCollection(ServiceLocator.Get.Settings.AvailableLanguages);
            ServiceLocator.Get.SettingsManager.AddConfiguration(ServiceLocator.Get.Settings);
        }

        #endregion

        #region Functions

        public void RefreshVisual()
        {
            ServiceLocator.Get.ResourcesManager.ResetCulture(ServiceLocator.Get.Settings.CurrentCulture);

            ServiceLocator.Get.SettingsManager.Reset();
            ThemesVm.Reset();

            Application.Current.UpdateBinding();

            foreach (var view in ServiceLocator.Get.RegionManager.Regions.SelectMany(x => x.Views).OfType<DependencyObject>())
                view.UpdateBinding();
        }

        #endregion

        #region Event Handlers

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            RefreshVisual();
        } 

        #endregion

        #region Properties

        public NavigationViewModel NavigationVm { get; private set; }
        public HeaderPanelViewModel HeaderPanelVm { get; private set; }
        public ThemesViewModel ThemesVm { get; private set; }
        public LanguagesViewModel LanguagesVm { get; private set; }
        public HeaderMenuViewModel HeaderMenuVm { get; private set; }
        public HeaderToolbarViewModel HeaderToolbarVm { get; private set; }
        public SettingsViewModel SettingsVm { get; private set; }
        public WindowStateViewModel WindowStateVm { get; private set; }

        private MainWindow Window { get; }

        #endregion
    }
}
