using System;
using System.Collections.Generic;
using System.Text;
using OutWit.Common.Aspects;
using OutWit.Common.Logging.Aspects;
using OutWit.Common.MVVM.Commands;
using OutWit.Common.MVVM.Utils;
using OutWit.Common.MVVM.ViewModels;
using OutWit.Common.Utils;

namespace OutWit.Demo.ViewModels
{
    [Log]
    public class HeaderPanelViewModel : ViewModelBase<ApplicationViewModel>
    {
        #region Constructors

        public HeaderPanelViewModel(ApplicationViewModel applicationVm) :
            base(applicationVm)
        {
            InitDefaults();
            InitCommands();
            InitEvents();
        }

        #endregion

        #region Initialization

        private void InitDefaults()
        {
            IsNavigationBarFixed = ServiceLocator.Get.Settings.IsNavigationBarFixed;

            ResetHeader();
        }

        private void InitCommands()
        {
            ToggleFullScreenCmd = new DelegateCommand(x=>ToggleFullScreen());
        }

        private void InitEvents()
        {
            ServiceLocator.Get.WindowManager.WindowStateChanged += OnWindowStateChanged;
            ServiceLocator.Get.WindowManager.WindowTitleUpdated += ResetHeader;
            PropertyChanged += OnPropertyChanged;
        }

        #endregion

        #region Functions

        private void ToggleFullScreen()
        {
            if(ServiceLocator.Get.WindowManager.IsFullScreen)
                ServiceLocator.Get.WindowManager.Restore();

            else
                ServiceLocator.Get.WindowManager.FullScreen();
        }

        private void ResetHeader()
        {
            Header = ServiceLocator.Get.WindowManager.WindowTitle;
        }

        #endregion

        #region Event Handlers

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.IsProperty((HeaderPanelViewModel vm) => vm.IsNavigationBarFixed) && !IsFullScreen)
            {
                ServiceLocator.Get.Settings.IsNavigationBarFixed = IsNavigationBarFixed;
                ServiceLocator.Get.Settings.General.Update();
                int i = 0;
            }
        }

        private void OnWindowStateChanged()
        {
            IsFullScreen = ServiceLocator.Get.WindowManager.IsFullScreen;
            IsMaximized = ServiceLocator.Get.WindowManager.IsMaximized;

            if (IsFullScreen)
                IsNavigationBarFixed = false;
            else
                IsNavigationBarFixed = ServiceLocator.Get.Settings.IsNavigationBarFixed;
            
        }

        #endregion

        #region Properties

        [Notify]
        public bool IsNavigationBarFixed { get; set; }

        [Notify]
        public bool IsFullScreen { get; private set; }

        [Notify]
        public bool IsMaximized { get; private set; }

        [Notify]
        public string Header { get; private set; }


        #endregion

        #region Commands

        public Command ToggleFullScreenCmd { get; private set; }

        #endregion
    }
}
