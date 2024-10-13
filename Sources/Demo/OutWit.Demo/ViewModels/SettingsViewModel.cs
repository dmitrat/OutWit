using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using OutWit.Common.Aspects;
using OutWit.Common.MVVM.Commands;
using OutWit.Common.MVVM.ViewModels;
using OutWit.Common.Settings;
using OutWit.Demo.Utils;
using OutWit.Demo.Views;

namespace OutWit.Demo.ViewModels
{
    public class SettingsViewModel : ViewModelBase<ApplicationViewModel>
    {
        #region Constructors

        public SettingsViewModel(ApplicationViewModel applicationVm) :
            base(applicationVm)
        {
            InitDefaults();
            InitEvents();
            InitCommands();
        }

        #endregion

        #region Initialization

        private void InitDefaults()
        {
            Collections = ServiceLocator.Get.SettingsManager.Collections?.OrderBy(x=>x.Priority).Where(collection=>collection.Any(value=>!value.UserHidden)).ToList();
            SelectedCollection = Collections?.FirstOrDefault();
        }

        private void InitEvents()
        {
        }

        private void InitCommands()
        {
            RestoreDefaultSettingsCmd = new DelegateCommand(x => RestoreDefaultSettings());
            SaveSettingsCmd = new DelegateCommand(x => SaveSettings());
            CloseWindowAndRollbackSettingsCmd = new DelegateCommand(x => CloseWindowAndRollbackSettings());
            CloseWindowAndSaveSettingsCmd = new DelegateCommand(x => CloseWindowAndSaveSettings());
        }

        #endregion

        #region Functions

        public void Show()
        {
            InitDefaults();

            Popup = new SettingsPopup{DataContext = this};

            Popup.KeyDown += OnPopupKeyDown;

            ServiceLocator.Get.PopupManager.ShowPrompt<bool>(Popup);
        }

        #endregion

        #region Command Windows

        private async void RestoreDefaultSettings()
        {
            this.Check(() => ServiceLocator.Get.PopupManager.CloseDialog(false));

            var option = await ServiceLocator.Get.PopupManager.ShowPrompt<YesNoOptions>(new RestoreSettingsPopup());

            if (option == YesNoOptions.YesOption)
            {
                this.Check(() => ServiceLocator.Get.SettingsManager.ResetUserConfiguration());
                this.Check(() => ServiceLocator.Get.SettingsManager.Rebuild());
            }
            else if (option == YesNoOptions.NoOption)
                Show();
        }

        private void SaveSettings()
        {
            this.Check(() => ServiceLocator.Get.SettingsManager.Update());
        }

        private void CloseWindowAndRollbackSettings()
        {
            this.Check(() => ServiceLocator.Get.PopupManager.CloseDialog(false));
            this.Check(() => ServiceLocator.Get.SettingsManager.Reset());
        }

        private void CloseWindowAndSaveSettings()
        {
            this.Check(() =>ServiceLocator.Get.PopupManager.CloseDialog(true));
            this.Check(() => ServiceLocator.Get.SettingsManager.Update());
        }

        #endregion

        #region Event Handlers

        private void OnPopupKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
                CloseWindowAndRollbackSettings();

        } 

        #endregion

        #region Properties

        private SettingsPopup Popup { get; set; }

        [Notify]
        public IReadOnlyList<SettingsCollection> Collections { get; private set; }

        [Notify]
        public SettingsCollection SelectedCollection { get; set; }

        #endregion

        #region Commands

        public Command RestoreDefaultSettingsCmd { get; private set; }

        public Command SaveSettingsCmd { get; private set; }

        public Command CloseWindowAndRollbackSettingsCmd { get; private set; }

        public Command CloseWindowAndSaveSettingsCmd { get; private set; }

        #endregion

    }
}
