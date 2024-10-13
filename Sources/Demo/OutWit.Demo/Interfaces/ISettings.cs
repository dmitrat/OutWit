using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutWit.Common.Settings;
using OutWit.Common.Settings.Interfaces;
using Serilog.Events;

namespace OutWit.Demo.Interfaces
{
    public interface ISettings : IConfigurationManager
    {
        string ModuleFolder { get; }

        bool ShowSplashScreen { get; }

        TimeSpan SnackBarMessageDuration { get; }

        bool ShowMaximized { get; set; }

        bool IsNavigationBarFixed { get; set; }
        
        string CurrentCulture { get; set; }
        
        string CurrentTheme { get; set; }

        LogEventLevel LogMinimumLevel { get; set; }

        SettingsCollection General { get; }
        
        SettingsCollection AvailableLanguages { get; }
    }
}
