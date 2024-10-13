using System;
using System.ComponentModel.Composition;
using System.Reflection;
using OutWit.Common.Configuration;
using OutWit.Common.Interfaces;
using OutWit.Common.Services.Interfaces;
using OutWit.Themes.Interfaces;

namespace OutWit.Themes.Dark
{
    [Export(typeof(IThemeModule))]
    public class DarkThemeModule : IThemeModule
    {
        public void Initialize(IServiceContainer container)
        {
            container.Resolve<IResourcesManager>()?.AddResourceDictionary(new ResourcesBase<Properties.Resources>(Assembly.GetExecutingAssembly()));

            container.Resolve<IThemeContainer>().Register(new DarkTheme());
        }

    }
}
