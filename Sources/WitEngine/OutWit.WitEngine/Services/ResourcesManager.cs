using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using OutWit.Common.Configuration;
using OutWit.Common.Interfaces;
using OutWit.WitEngine.Properties;

namespace OutWit.WitEngine.Services
{
    public class ResourcesManager : IResourcesManager
    {
        #region Constructors

        public ResourcesManager()
        {
            Resources = new ObservableCollection<IResources>();

            AddResourceDictionary(new ResourcesBase<Resources>(Assembly.GetExecutingAssembly()));
        }

        #endregion

        #region IResourceManager

        public void AddResourceDictionary(IResources resources)
        {
            Resources.Add(resources);
        }

        public void ResetCulture(string cultureName)
        {
            foreach (var resource in Resources)
                resource.ResetCulture(cultureName);
        }

        #endregion

        #region IEnumerable

        public IEnumerator<IResources> GetEnumerator()
        {
            return Resources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Properties

        private ObservableCollection<IResources> Resources { get; }

        #endregion

    }
}
