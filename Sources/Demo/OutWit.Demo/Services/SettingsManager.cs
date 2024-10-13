using System;
using System.Collections;
using System.Collections.Generic;
using OutWit.Common.Settings.Interfaces;
using OutWit.Common.Settings;

namespace OutWit.Demo.Services
{
    public class SettingsManager : ISettingsManager
    {
        #region events

        public event SettingsManagerEventHandler SettingsUpdated = delegate { };

        #endregion

        #region Fields

        private readonly List<SettingsCollection> m_collections;
        private readonly List<IConfigurationManager> m_configuration;

        #endregion

        #region Constructors

        public SettingsManager()
        {
            m_collections = new List<SettingsCollection>();
            m_configuration = new List<IConfigurationManager>();
        }

        #endregion

        #region ISettingsManager


        public void AddCollection(SettingsCollection collection)
        {
            m_collections.Add(collection);
        }

        public void AddConfiguration(IConfigurationManager configuration)
        {
            m_configuration.Add(configuration);
        }

        public void ResetUserConfiguration()
        {
            foreach (var configuration in m_configuration)
                configuration.ResetUserConfiguration();
        }

        public void MergeUserConfiguration()
        {
            foreach (var configuration in m_configuration)
                configuration.ResetUserConfiguration();
        }

        public void Rebuild()
        {
            foreach (var collection in this)
                collection.Rebuild();
        }

        public void Reset()
        {
            foreach (var collection in this)
                collection.Reset();

            SettingsUpdated();

        }

        public void Update()
        {
            foreach (var collection in this)
                collection.Update();

            SettingsUpdated();
        }

        #endregion

        #region IEnumerable

        public IEnumerator<SettingsCollection> GetEnumerator()
        {
            return m_collections.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Properties

        public IReadOnlyList<SettingsCollection> Collections => m_collections;

        #endregion

    }
}
