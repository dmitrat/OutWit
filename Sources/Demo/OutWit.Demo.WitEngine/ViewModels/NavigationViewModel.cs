using OutWit.Common.Aspects;
using OutWit.Common.MVVM.Commands;
using OutWit.Common.MVVM.ViewModels;
using OutWit.Common.Prism.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutWit.Common.Aspects.Utils;

namespace OutWit.Demo.WitEngine.ViewModels
{
    public class NavigationViewModel : ViewModelBase<ApplicationViewModel>, INavigationContext
    {
        #region Constructors

        public NavigationViewModel(ApplicationViewModel applicationVm) :
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
            IsJobEditor = true;
            IsEnabled = false;
            IsDefault = false;
        }

        private void InitEvents()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void InitCommands()
        {
            JobEditorCmd = new DelegateCommand(x => JobEditor());
            LoadedCmd = new DelegateCommand(x => Loaded());
        }

        #endregion

        #region Functions

        public void Navigate(INavigationParameters parameters)
        {
            var modeStr = (parameters["mode"] is string str) ? str : "";

            if (string.IsNullOrEmpty(modeStr) || !Enum.TryParse(modeStr, out Modes mode))
                mode = Modes.JobEditor;

            Mode = mode;

            Navigate();
        }

        public bool CanNavigate(string parameter)
        {
            return IsEnabled;
        }

        public void Navigate()
        {
            switch (Mode)
            {
                case Modes.JobEditor:
                    JobEditor();
                    break;
            }
        }

        private void UpdateMode()
        {
            IsJobEditor = Mode == Modes.JobEditor;
        }

        private void Loaded()
        {
            IsLoaded = true;
        }
        #endregion

        #region Command Functions

        private void JobEditor()
        {
            Mode = Modes.JobEditor;

            ApplicationVm.JobEditorVm.Navigate();
        }

        #endregion

        #region Event Handlers

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.IsProperty((NavigationViewModel vm) => vm.Mode))
                UpdateMode();

        }

        #endregion

        #region INavigationContext

        [Notify]
        public bool IsSelected { get; set; }

        [Notify]
        public bool IsEnabled { get; set; }

        [Notify]
        public bool IsVisible { get; set; }

        [Notify]
        public bool IsLoaded { get; set; }

        [Notify]
        public bool IsHidden { get; set; }

        [Notify]
        public bool IsDefault { get; private set; }

        #endregion

        #region Properties

        [Notify]
        public Modes Mode { get; private set; }

        [Notify]
        public bool IsJobEditor { get; private set; }

        #endregion

        #region Commands

        public Command JobEditorCmd { get; private set; }

        public Command LoadedCmd { get; private set; }

        #endregion
    }

    public enum Modes
    {
        JobEditor = 0
    }
}
