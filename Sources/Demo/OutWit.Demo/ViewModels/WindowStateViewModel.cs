using System;
using System.Windows;
using OutWit.Common.Controls.Interfaces;
using OutWit.Common.MVVM.ViewModels;
using OutWit.Demo.Interfaces;

namespace OutWit.Demo.ViewModels
{
    public class WindowStateViewModel : ViewModelBase<ApplicationViewModel>
    {
        #region Constructors

        public WindowStateViewModel(ApplicationViewModel applicationVm) :
            base(applicationVm)
        {
           InitDefaults(); 
           InitEvents();
        }

        #endregion

        #region Initialization

        private void InitDefaults()
        {
            if (Settings.ShowMaximized)
                WindowManager.Maximize();
        }

        private void InitEvents()
        {
            WindowManager.WindowStateChanged += OnWindowStateChanged;
        }

        private void OnWindowStateChanged()
        {
            Settings.ShowMaximized = WindowManager.WindowState == WindowState.Maximized;
            Settings.Save();
        }

        #endregion

        #region Event Handlers

        private ISettings Settings => ServiceLocator.Get.Settings;

        private IWindowManager WindowManager => ServiceLocator.Get.WindowManager;

        #endregion


    }
}
