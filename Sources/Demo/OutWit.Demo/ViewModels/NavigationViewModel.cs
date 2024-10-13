using System;
using System.Collections.Generic;
using System.Text;
using OutWit.Common.Aspects;
using OutWit.Common.Logging.Aspects;
using OutWit.Common.MVVM.Commands;
using OutWit.Common.MVVM.ViewModels;
using OutWit.Common.Prism.Interfaces;

namespace OutWit.Demo.ViewModels
{
    [Log]
    public class NavigationViewModel : ViewModelBase<ApplicationViewModel>
    {
        #region Constructors

        public NavigationViewModel(ApplicationViewModel applicationVm) :
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
            IsExpanded = ServiceLocator.Get.Settings.IsNavigationBarFixed;
        }

        private void InitCommands()
        {
            NavigationPanelCollapsingCmd = new DelegateCommand(x => NavigationPanelCollapsing());
            NavigationPanelExpandingCmd = new DelegateCommand(x => NavigationPanelExpanding());
        }

        private void InitEvents()
        {
            PropertyChanged += OnPropertyChanged;
            ServiceLocator.Get.NavigationManager.NavigationChanged += OnNavigationChanged;
        }

        #endregion

        #region Functions

        private void NavigationPanelCollapsing()
        {
            SelectedItem?.Collapse();
            IsExpanded = false;
        }

        private void NavigationPanelExpanding()
        {
            SelectedItem?.Expand();
            IsExpanded = true;
        }
        #endregion

        #region Event Handlers

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (SelectedItem != null && ServiceLocator.Get.NavigationManager.CanNavigate(SelectedItem))
                ServiceLocator.Get.NavigationManager.Navigate(SelectedItem);
            else if(SelectedItem != ServiceLocator.Get.NavigationManager.Current)
                SelectedItem = ServiceLocator.Get.NavigationManager.Current;
        }

        private void OnNavigationChanged(INavigationControl sender)
        {
            if (SelectedItem == sender)
                return;

            SelectedItem = sender;
            if (!IsExpanded)
                SelectedItem.Collapse();
        }

        #endregion

        #region Properties

        [Notify]
        public INavigationControl SelectedItem { get; set; }

        private bool IsExpanded { get; set; }

        #endregion

        #region Commands

        public Command NavigationPanelCollapsingCmd { get; set; }
        public Command NavigationPanelExpandingCmd { get; set; }

        #endregion
    }
}
