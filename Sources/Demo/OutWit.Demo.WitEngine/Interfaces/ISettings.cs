using OutWit.Common.Settings;
using OutWit.Common.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutWit.Demo.WitEngine.Interfaces
{
    public interface ISettings : IConfigurationManager
    {
        string JobsFolder { get; set; }

        SettingsCollection General { get; }
    }
}
