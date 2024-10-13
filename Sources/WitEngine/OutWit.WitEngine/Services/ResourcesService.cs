using System;
using OutWit.Common.Configuration;
using OutWit.WitEngine.Shared.Interfaces;

namespace OutWit.WitEngine.Services
{
    public class ResourcesService : ResourcesMerged, IWitResources
    {
        public ResourcesService() :
            base(ServiceLocator.Get.ResourcesManager)
        {
        }

        public string IncorrectInput => this[nameof(Properties.Resources.IncorrectInput)];

        public string WrongActivityType => this[nameof(Properties.Resources.WrongActivityType)];

        public string UndefinedOperator => this[nameof(Properties.Resources.UndefinedOperator)];

        public string UndefinedActivity => this[nameof(Properties.Resources.UndefinedActivity)];
    }
}
