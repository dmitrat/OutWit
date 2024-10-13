using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OutWit.Common.Aspects.Utils;
using OutWit.Common.Prism;
using OutWit.Common.Prism.Interfaces;
using Prism.Navigation;
using Prism.Navigation.Regions;
using Unity;

namespace OutWit.Demo.Services
{
    public class NavigationManager : INavigationManager
    {
        #region Events

        public event LoadedEventHandler Loaded = delegate { };
        public event NavigationChangedEventHandler NavigationChanged = delegate { };

        #endregion

        #region Constructors

        public NavigationManager()
        {
            Current = null;
            NLoaded = 0;
        }

        #endregion

        #region INavigationManager

        public void Register<TNavigation>(string key) where TNavigation : INavigationControl, new()
        {
            var navigation = new TNavigation();

            navigation.Context.PropertyChanged += OnNavigationContextPropertyChanged;

            NavigationPanelRegion.Add(navigation, key);

            Register<TNavigation>();
        }

        public void Register<TMenuItem>(MenuRoots menuRoot) where TMenuItem : Control, new()
        {
            var menuItem = new TMenuItem();

            if (menuRoot == MenuRoots.File)
                HeaderMenuFileRegion?.Add(menuItem);

            if (menuRoot == MenuRoots.View)
                HeaderMenuViewRegion?.Add(menuItem);
        }

        public void Register<TView>()
        {
            ServiceLocator.Get.Container.RegisterType<object, TView>($"{typeof(TView)}");
        }

        public void NavigateAsync(INavigationControl navigationCtrl, string parameter = "")
        {
            Application.Current.Dispatcher.InvokeAsync(() => Navigate(navigationCtrl, parameter));
        }

        public void Navigate(INavigationControl navigationCtrl, string parameter = "")
        {
            if (Current == navigationCtrl && string.IsNullOrEmpty(parameter))
                return;

            if (Current != navigationCtrl)
            {
                Current?.Deselect();
                Current = navigationCtrl;
            }

            if (string.IsNullOrEmpty(parameter))
                MainPanelRegion.RequestNavigate($"{navigationCtrl.GetType()}");
            else
                MainPanelRegion.RequestNavigate($"{navigationCtrl.GetType()}", new NavigationParameters { { "mode", parameter } });

            NavigationChanged(Current);
        }

        public bool IsOnScreen<TView>()
        {
            return MainPanelRegion.ActiveViews.Any(x => x is TView);
        }

        public void NavigateAsync<TView>()
        {
            Application.Current.Dispatcher.InvokeAsync(Navigate<TView>);
        }

        public void Navigate<TView>()
        {
            MainPanelRegion.RequestNavigate($"{typeof(TView)}");
        }

        public void NavigateAsync(string key, string parameter)
        {
            Application.Current.Dispatcher.InvokeAsync(() => Navigate(key, parameter));
        }

        public void Navigate(string key, string parameter)
        {
            if (string.IsNullOrEmpty(key))
                return;

            if (!(NavigationPanelRegion.GetView(key) is INavigationControl control))
                return;

            Navigate(control, parameter);
        }

        public bool CanNavigate(string key, string parameter)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            if (!(NavigationPanelRegion.GetView(key) is INavigationControl control))
                return false;

            return control.Context.CanNavigate(parameter);
        }

        public bool CanNavigate(INavigationControl navigationCtrl, string parameter = "")
        {
            return navigationCtrl.Context.CanNavigate(parameter);
        }

        public void NavigateFirst()
        {
            var navigationControls = NavigationPanelRegion.Views.OfType<INavigationControl>().ToList();

            var defaultNavigationControl = navigationControls.FirstOrDefault(x => x.Context.IsDefault) ??
                                           navigationControls.FirstOrDefault();

            if (defaultNavigationControl == null)
                return;

            Navigate(defaultNavigationControl);
        }

        public void LockNavigation()
        {
            ServiceLocator.Get.WindowManager.LockNavigation();
        }

        public void UnlockNavigation()
        {
            ServiceLocator.Get.WindowManager.UnlockNavigation();
        }

        public void Refresh()
        {
            var control = GetActiveView();
            if (control == null)
                return;

            control.Visibility = Visibility.Hidden;
            control.Visibility = Visibility.Visible;
        }

        #endregion

        #region Functions

        private Rect GetLocation()
        {
            var control = GetActiveView();
            if (control == null || !(control.Parent is ContentControl contentControl))
                return new Rect();

            var location = contentControl.PointToScreen(new Point(0, 0));

            return new Rect(location, new Size(contentControl.ActualWidth, contentControl.ActualHeight));
        }
        private UserControl GetActiveView()
        {
            var view = MainPanelRegion.ActiveViews.SingleOrDefault();

            if (!(view is UserControl control))
                return null;

            return control;
        }

        #endregion

        #region Event Handlers

        private void OnNavigationContextPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.IsProperty((INavigationContext c) => c.IsLoaded) && ++NLoaded == NavigationPanelRegion.Views.Count())
                Loaded();
        }

        #endregion

        #region Properties

        private int NLoaded { get; set; }

        public INavigationControl Current { get; private set; }

        private IRegion MainPanelRegion => ServiceLocator.Get.RegionManager.Regions[Regions.MAIN_PANEL];
        private IRegion NavigationPanelRegion => ServiceLocator.Get.RegionManager.Regions[Regions.NAVIGATION_PANEL];
        private IRegion HeaderMenuFileRegion => ServiceLocator.Get.RegionManager.Regions[Regions.HEADER_MENU_FILE];
        private IRegion HeaderMenuViewRegion => ServiceLocator.Get.RegionManager.Regions[Regions.HEADER_MENU_VIEW];

        #endregion
    }
}
