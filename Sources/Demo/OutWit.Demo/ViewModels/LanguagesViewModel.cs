using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using OutWit.Common.Aspects;
using OutWit.Common.Logging.Aspects;
using OutWit.Common.MVVM.Commands;
using OutWit.Common.MVVM.ViewModels;
using OutWit.Common.Settings.Interfaces;
using OutWit.Common.Settings.Values;
using OutWit.Common.Utils;

namespace OutWit.Demo.ViewModels
{
    [Log]
    public class LanguagesViewModel : ViewModelBase<ApplicationViewModel>
    {
        #region Constructors

        public LanguagesViewModel(ApplicationViewModel applicationVm) :
            base(applicationVm)
        {
            InitDefaults();
            InitEvents();
        }

        #endregion

        #region Initialization

        private void InitDefaults()
        {
            AvailableLanguages = ServiceLocator.Get.Settings.AvailableLanguages;

            SelectedLanguage = AvailableLanguages.SingleOrDefault(x => x.UserValue.Equals(ServiceLocator.Get.Settings.CurrentCulture)) ??
                               AvailableLanguages.FirstOrDefault();

        }

        private void InitEvents()
        {
            this.PropertyChanged += OnPropertyChanged;

        }

        #endregion

        #region Functions

        #endregion

        #region Events Handlers

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.IsProperty((LanguagesViewModel vm) => vm.SelectedLanguage) && SelectedLanguage != null)
            {
                ServiceLocator.Get.Settings.CurrentCulture = (string)SelectedLanguage.UserValue;
                ServiceLocator.Get.Settings.General.Update();
                ApplicationVm.RefreshVisual();
            }
        }

        #endregion

        #region Properties

        public IEnumerable<ISettingsValue> AvailableLanguages { get; private set; }

        [Notify]
        public ISettingsValue SelectedLanguage { get; set; } 

        #endregion
    }
}
