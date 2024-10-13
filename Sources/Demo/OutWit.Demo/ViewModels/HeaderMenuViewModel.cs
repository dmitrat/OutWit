using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using OutWit.Common.Controls.Menu;
using OutWit.Common.Logging.Aspects;
using OutWit.Common.MVVM.Commands;
using OutWit.Common.MVVM.ViewModels;
using OutWit.Common.Prism;
using OutWit.Common.Prism.Interfaces;
using OutWit.Demo.Views;
using Prism.Navigation.Regions;

namespace OutWit.Demo.ViewModels
{
    [Log]
    public class HeaderMenuViewModel : ViewModelBase<ApplicationViewModel>
    {
        #region Constructors

        public HeaderMenuViewModel(ApplicationViewModel applicationVm) :
            base(applicationVm)
        {
            InitDefaults();
            InitCommands();
        }

        #endregion

        #region Initialization

        private void InitDefaults()
        {

        }

        private void InitCommands()
        {
            RegisterMenuFileRegionCmd = new DelegateCommand(x => RegisterMenuFileRegion(x as PrismMenuItem));
        }

        #endregion

        #region Functions

        private void RegisterMenuFileRegion(PrismMenuItem view)
        {
            RegisterMenuRegion(view, Regions.HEADER_MENU_FILE);
            RegisterMenuFileItems();
        }

        private void RegisterMenuRegion(PrismMenuItem view, string regionName)
        {
            if (view == null)
                return;

            var region = new Region();

            ServiceLocator.Get.RegionManager.Regions.Add(regionName, region);

            var regionBinding = new Binding("Views") {Source = region};

            view.SetBinding(PrismMenuItem.DynamicItemsProperty, regionBinding);
        }

        private void RegisterMenuFileItems()
        {
            ServiceLocator.Get.NavigationManager.Register<HeaderMenuFileSettings>(MenuRoots.File);

            ServiceLocator.Get.NavigationManager.Register<HeaderMenuFileExitSeparator>(MenuRoots.File);
            ServiceLocator.Get.NavigationManager.Register<HeaderMenuFileExit>(MenuRoots.File);
        }

        #endregion

        #region Commands

        public Command RegisterMenuFileRegionCmd { get; private set; }

        #endregion
    }

    public delegate void HeaderMenuEventHandler();
}
