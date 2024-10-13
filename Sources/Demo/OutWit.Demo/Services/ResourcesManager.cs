using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using OutWit.Common.Configuration;
using System.Reflection;
using OutWit.Common.Interfaces;
using OutWit.Demo.Properties;

namespace OutWit.Demo.Services
{
    public class ResourcesManager : IResourcesManager
    {
        #region Constructors

        public ResourcesManager()
        {
            Resources = new ObservableCollection<IResources>();
            
            InitDefaults();
            InitEvents();
        }

        #endregion

        #region Initialization

        private void InitDefaults()
        {
            AddResourceDictionary(new ResourcesBase<Resources>(Assembly.GetExecutingAssembly()));
        }

        private void InitEvents()
        {

            Resources.CollectionChanged += OnCollectionChanged;
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

        #region Event Handlers

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems == null)
                return;
            
            foreach (var item in e.NewItems.OfType<IResources>())
                item.ResetCulture(ServiceLocator.Get.Settings.CurrentCulture);
        }

        #endregion

        #region Properties

        private ObservableCollection<IResources> Resources { get; }

        #endregion


    }
}
