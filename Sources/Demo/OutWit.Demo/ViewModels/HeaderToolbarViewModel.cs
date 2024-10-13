using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Windows;
using OutWit.Common.Aspects;
using OutWit.Common.Logging.Aspects;
using OutWit.Common.MVVM.Commands;
using OutWit.Common.MVVM.ViewModels;

namespace OutWit.Demo.ViewModels
{
    [Log]
    public class HeaderToolbarViewModel : ViewModelBase<ApplicationViewModel>
    {
        #region Constructors

        public HeaderToolbarViewModel(ApplicationViewModel applicationVm) :
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
           
        }

        private void InitCommands()
        {
            ExitCmd = new DelegateCommand(x => Exit());
            SettingsCmd = new DelegateCommand(x => Settings());
        }

        private void InitEvents()
        {
        }

        #endregion

        #region Functions

        private void Exit()
        {
            ServiceLocator.Get.WindowManager.Close();
        }

        private void Settings()
        {
            ApplicationVm.SettingsVm.Show();
        }

        private void UpdateStatus()
        {
        }

        #endregion

        #region InitEvents

    

        #endregion

        #region Properties
        

        #endregion

        #region Commands

        public Command ExitCmd { get; private set; }

        public Command SettingsCmd { get; private set; }

        #endregion
    }
}
