using System;
using System.ComponentModel.Composition;
using System.Reflection;
using Microsoft.Extensions.Logging;
using OutWit.Common.Configuration;
using OutWit.Common.Interfaces;
using OutWit.Common.Services.Interfaces;
using OutWit.Controller.Jobs.Adapters;
using OutWit.Controller.Jobs.Properties;
using OutWit.Engine.Shared.Interfaces;

namespace OutWit.Controller.Jobs
{
    [Export(typeof(IWitController))]
    public class WitControllerJobsModule : IWitController
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
            controllers.RegisterJob(new WitJobAdapterVoid());
            controllers.RegisterJob(new WitJobAdapterString());
            controllers.RegisterJob(new WitJobAdapterStringString());
        }

    }
}
