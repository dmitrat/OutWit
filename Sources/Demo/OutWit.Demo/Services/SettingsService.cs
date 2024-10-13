using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Serilog.Events;
using OutWit.Common.Settings.Aspects;
using OutWit.Common.Settings.Configuration;
using OutWit.Common.Settings;
using OutWit.Demo.Interfaces;

namespace OutWit.Demo.Services
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class SettingsService : ConfigurationManager, ISettings
    {
        #region Constructors

        public SettingsService() :
            base(Assembly.GetExecutingAssembly(), true, 2, "Configuration")
        {
            General = new SettingsCollection(ServiceLocator.Get.Resources, this, nameof(General));
            AvailableLanguages = new SettingsCollection(ServiceLocator.Get.Resources, this, nameof(AvailableLanguages));
        }

        #endregion

        #region Functions

        public void Update()
        {
            General.Update();
        }

        public void Reset()
        {
            General.Reset();
        }

        #endregion

        #region ISettings

        [Setting(nameof(General))]
        public string ModuleFolder { get; }
        [Setting(nameof(General))]
        public bool ShowSplashScreen { get; }
        [Setting(nameof(General))]
        public TimeSpan SnackBarMessageDuration { get; }
        
        [Setting(nameof(General))]
        public bool ShowMaximized { get; set; }
        [Setting(nameof(General))]
        public bool IsNavigationBarFixed { get; set; }
        [Setting(nameof(General))]
        public string CurrentCulture { get; set; }
        [Setting(nameof(General))]
        public string CurrentTheme { get; set; }
        [Setting(nameof(General))]
        public LogEventLevel LogMinimumLevel { get; set; }

        #endregion

        #region Properties

        public SettingsCollection General { get; }
        public SettingsCollection AvailableLanguages { get; }

        #endregion
    }
}
