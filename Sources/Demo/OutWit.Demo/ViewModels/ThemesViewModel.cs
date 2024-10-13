using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using OutWit.Common.Aspects;
using OutWit.Common.Aspects.Utils;
using OutWit.Common.Logging.Aspects;
using OutWit.Common.MVVM.ViewModels;
using OutWit.Themes.Interfaces;
using OutWit.Themes.Utils;

namespace OutWit.Demo.ViewModels
{
    [Log]
    public class ThemesViewModel : ViewModelBase<ApplicationViewModel>
    {
        #region Constructors

        public ThemesViewModel(ApplicationViewModel applicationVm) :
            base(applicationVm)
        {
            InitDefaults();
            InitEvents();
        }

        #endregion

        #region Initialization

        private void InitDefaults()
        {
            Reset();

        }

        private void InitEvents()
        {
            this.PropertyChanged += OnPropertyChanged;
            ServiceLocator.Get.ThemeContainer.CurrentThemeChanged += OnCurrentThemeChanged;
        }

        #endregion

        #region Functions

        public void Reset()
        {
            AvailableThemes = Themes.Selector.Get.ThemeContainer.AllThemes.ToArray();

            SelectedTheme = AvailableThemes.Contains(ServiceLocator.Get.Settings.CurrentTheme)
                ? ServiceLocator.Get.Settings.CurrentTheme
                : AvailableThemes.FirstOrDefault();

            Themes.Selector.Get.ThemeContainer.SetCurrentTheme(SelectedTheme);

            OnCurrentThemeChanged(ServiceLocator.Get.ThemeContainer.Current);
        }


        #endregion

        #region Events Handlers

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.IsProperty((ThemesViewModel vm) => vm.SelectedTheme) && !string.IsNullOrEmpty(SelectedTheme))
            {
                Themes.Selector.Get.ThemeContainer.SetCurrentTheme(SelectedTheme);
                ServiceLocator.Get.Settings.CurrentTheme = SelectedTheme;
                ServiceLocator.Get.Settings.General.Update();
            }
        }

        private void OnCurrentThemeChanged(ITheme currentTheme)
        {
            Application.Current.SetTheme(currentTheme);
        }

        #endregion

        #region Properties

        public IEnumerable<string> AvailableThemes { get; private set; }

        [Notify]
        public string SelectedTheme { get; set; }

        #endregion
    }
}
