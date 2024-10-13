using OutWit.Common.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using OutWit.Common.Settings.Aspects;
using OutWit.Common.Settings.Configuration;
using OutWit.Demo.WitEngine.Interfaces;

namespace OutWit.Demo.WitEngine.Services
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class SettingsService : ConfigurationManager, ISettings
    {
        #region Constructors

        public SettingsService() :
            base(Assembly.GetExecutingAssembly(), true, 2, "Configuration")
        {
            General = new SettingsCollection(ServiceLocator.Get.Resources, this, nameof(General));
        }

        #endregion

        #region ISettings

        [Setting(nameof(General))]
        public string JobsFolder { get; set; }

        #endregion

        #region Properties

        public SettingsCollection General { get; }

        #endregion
    }
}
