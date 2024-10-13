using OutWit.Common.Configuration;
using OutWit.Common.Interfaces;

namespace OutWit.Demo.Services
{
    public class ResourcesService : ResourcesMerged
    {
        public ResourcesService() :
            base(ServiceLocator.Get.ResourcesManager)
        {
        }
    }
}
