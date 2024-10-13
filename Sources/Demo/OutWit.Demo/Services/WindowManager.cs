using OutWit.Common.Controls.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OutWit.Demo.Views;

namespace OutWit.Demo.Services
{
    public class WindowManager : IWindowManager
    {
        #region Events

        public event WindowManagerEventHandler WindowStateChanged = delegate { };
        public event WindowManagerEventHandler WindowLoaded = delegate { };
        public event WindowManagerEventHandler WindowClosing = delegate { };
        public event WindowManagerEventHandler WindowTitleUpdated = delegate { };

        #endregion

        #region Constructors

        public WindowManager(MainWindow window)
        {
            Window = window;

            ResetTitle();
            InitEvents();
        }

        #endregion

        #region Initialization

        private void InitEvents()
        {
            Window.StateChanged += (_, __) => WindowStateChanged();
            Window.FullScreenStateChanged += (_, __) => WindowStateChanged();
            Window.Loaded += OnWindowLoaded;
            Window.ContentRendered += OnWindowContentRendered;
            Window.Closing += OnWindowClosing;
        }

        #endregion

        #region Functions

        public void Maximize()
        {
            Window.Maximize();
        }

        public void Minimize()
        {
            Window.Minimize();
        }

        public void Restore()
        {
            Window.Restore();
        }

        public void FullScreen()
        {
            Window.FullScreen();
        }

        public void Close()
        {
            Window.Close();
        }

        public void LockNavigation()
        {
            //Window.NavigationPanel.IsEnabled = false;

            if (Window.Header is FrameworkElement header)
                header.IsEnabled = false;
        }

        public void UnlockNavigation()
        {
            //Window.NavigationPanel.IsEnabled = true;

            if (Window.Header is FrameworkElement header)
                header.IsEnabled = true;
        }

        public void UpdateTitle(string title)
        {
            WindowTitle = title;
            WindowTitleUpdated();
        }

        public void ResetTitle()
        {
            UpdateTitle($"{ServiceLocator.Get.Resources["ApplicationHeader"]}");
        }

        #endregion

        #region Event Handlers

        private void OnWindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ServiceLocator.Get.NavigationManager.NavigateFirst();
        }

        private void OnWindowContentRendered(object sender, EventArgs e)
        {
            WindowLoaded();
        }

        private async void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            WindowClosing();
        }

        #endregion

        #region Properties

        public bool IsMaximized => Window.IsMaximized;
        public bool IsFullScreen => Window.IsFullScreen;

        public WindowState WindowState => Window.WindowState;
        
        public string WindowTitle { get; private set; }

        private MainWindow Window { get; }

        #endregion
    }
}
