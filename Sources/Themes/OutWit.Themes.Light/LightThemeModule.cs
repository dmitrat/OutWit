using System;
using System.ComponentModel.Composition;
using OutWit.Common.Configuration;
using OutWit.Common.Interfaces;
using System.Reflection;
using OutWit.Common.Services.Interfaces;
using OutWit.Themes.Interfaces;

namespace OutWit.Themes.Light
{
    [Export(typeof(IThemeModule))]
    public class LightThemeModule : IThemeModule
    {
        public void Initialize(IServiceContainer container)
        {
            container.Resolve<IResourcesManager>()?.AddResourceDictionary(new ResourcesBase<Properties.Resources>(Assembly.GetExecutingAssembly()));

            container.Resolve<IThemeContainer>().Register(new LightTheme());
        }
    }
}
