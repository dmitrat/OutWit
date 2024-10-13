using System.Reflection;
using Microsoft.Extensions.Logging;
using OutWit.Common.Configuration;
using OutWit.Common.Interfaces;
using OutWit.Controller.Special.Adapters;
using OutWit.Controller.Special.Properties;
using OutWit.Engine.Shared.Interfaces;
using OutWit.Common.Services.Interfaces;
using System.ComponentModel.Composition;

namespace OutWit.Controller.Special
{
    [Export(typeof(IWitController))]
    public class WitControllerSpecialModule : IWitController
    {
        public void Initialize(IServiceContainer container)
        {
            InitServices(container);
            RegisterAdapters(ServiceLocator.Get.ControllerManager);
        }

        private void InitServices(IServiceContainer container)
        {
            container.Resolve<IResourcesManager>().
                AddResourceDictionary(new ResourcesBase<Resources>(Assembly.GetExecutingAssembly()));

            ServiceLocator.Get.Register(container.Resolve<ILogger>());
            ServiceLocator.Get.Register(container.Resolve<IWitResources>());
            ServiceLocator.Get.Register(container.Resolve<IWitControllerManager>());
            ServiceLocator.Get.Register(container.Resolve<IWitProcessingManager>());
        }

        private void RegisterAdapters(IWitControllerManager controllers)
        {
            controllers.RegisterActivity(new WitActivityAdapterSpecialDelayed());
            controllers.RegisterActivity(new WitActivityAdapterSpecialLoop());
            controllers.RegisterActivity(new WitActivityAdapterSpecialParallel());
            controllers.RegisterActivity(new WitActivityAdapterSpecialSequential());
            controllers.RegisterActivity(new WitActivityAdapterSpecialTimer());
            controllers.RegisterActivity(new WitActivityAdapterSpecialTest());
            controllers.RegisterActivity(new WitActivityAdapterSpecialPause());
            controllers.RegisterActivity(new WitActivityAdapterSpecialReturn());
            controllers.RegisterActivity(new WitActivityAdapterSpecialConcatString());
            controllers.RegisterActivity(new WitActivityAdapterSpecialCopy());
            controllers.RegisterActivity(new WitActivityAdapterSpecialUniqueName());
            controllers.RegisterActivity(new WitActivityAdapterSpecialPath());
        }
    }
}
