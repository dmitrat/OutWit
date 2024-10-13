using Microsoft.Extensions.Logging;
using OutWit.Common.Configuration;
using OutWit.Common.Interfaces;
using OutWit.Common.Services.Interfaces;
using OutWit.Controller.Variables.Adapters;
using OutWit.Controller.Variables.Properties;
using OutWit.Engine.Shared.Interfaces;
using System.ComponentModel.Composition;
using System.Reflection;

namespace OutWit.Controller.Variables
{
    [Export(typeof(IWitController))]
    public class WitControllerVariablesModule : IWitController
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
            controllers.RegisterVariable(new WitVariableAdapterDouble());
            controllers.RegisterVariable(new WitVariableAdapterInteger());
            controllers.RegisterVariable(new WitVariableAdapterColor());
            controllers.RegisterVariable(new WitVariableAdapterString());

            controllers.RegisterActivity(new WitActivityAdapterColor());
        }
    }
}
