﻿using System;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using OutWit.Engine.Data.Jobs;
using OutWit.Engine.Services;
using OutWit.Engine.Shared.Interfaces;
using OutWit.Engine.Interfaces;
using OutWit.Common.MEF;
using System.Reflection;
using OutWit.Common.Configuration;
using OutWit.Common.Interfaces;
using OutWit.Common.Logging;
using OutWit.Common.Utils;
using OutWit.Engine.Properties;
using System.Collections.Generic;
using OutWit.Engine.Data.Status;

namespace OutWit.Engine
{
    public class WitEngine
    {
        #region Constants

        private const string DEFAULT_MODULE_PATH = "@Controllers";

        #endregion
        #region Static Fields

        private static volatile WitEngine m_instance = null;
        private static readonly object m_syncRoot = new Object();

        #endregion

        #region Events

        public event ProcessingProgressChangedEventHandler ProcessingProgressChanged = delegate { };

        public event ProcessingCompletedEventHandler ProcessingCompleted = delegate { };

        public event ProcessingPausedEventHandler ProcessingPaused = delegate { };

        public event ProcessingReturnValueEventHandler ProcessingReturnValue = delegate { };

	    public event ProcessingStartedEventHandler ProcessingStarted = delegate { };

		#endregion

		#region Constructors

		private WitEngine()
        {
            InitServices();
        } 

        #endregion

        #region Initialization

        private void InitServices()
        {
            ServiceLocator.Get.Register<IResourcesManager>(new ResourcesManager());
            ServiceLocator.Get.Register<IWitResources>(new ResourcesService());

            ControllerManager = new ControllerManager();
            ServiceLocator.Get.Register<IWitControllerManager>(ControllerManager);

            ProcessingManager = new ProcessingManager();
            ServiceLocator.Get.Register<IWitProcessingManager>(ProcessingManager);
            ProcessingManager.ProcessingProgressChanged += (val, max, message) => ProcessingProgressChanged(val, max, message);
            ProcessingManager.ProcessingCompleted += (status,  msg) => ProcessingCompleted(status, msg);
	        ProcessingManager.ProcessingStarted += n => ProcessingStarted(n);
            ProcessingManager.ProcessingPaused += message => ProcessingPaused(message);
            ProcessingManager.ProcessingReturnValue += v => ProcessingReturnValue(v);
        }

        public void Reload(IResourcesManager resourcesManager, ILogger logger = null, string moduleFolder = null)
        {
            if (string.IsNullOrEmpty(moduleFolder))
                moduleFolder = Assembly.GetExecutingAssembly().AssemblyDirectory().AppendPath(DEFAULT_MODULE_PATH);

            if (resourcesManager != null)
                resourcesManager.AddResourceDictionary(ServiceLocator.Get.Resources);

            ServiceLocator.Get.Register<ILogger>(logger ?? new SimpleLogger(nameof(WitEngine), LogLevel.Information));

            ControllerManager.Clear();
            Bootstrapper = new Bootstrapper<IWitController>(ServiceLocator.Get, moduleFolder, true);
            Bootstrapper.Run();
        }

        #endregion

        #region Functions

        public WitProcessingStatus Process(WitJob job, params object[] parameters)
        {
            job.UpdateParameters(parameters);

            var result = ProcessingManager.Process(job, true, false, out var message);
            
            return result;
        }

        public void ProcessAsync(WitJob job, params object[] parameters)
        {
            job.UpdateParameters(parameters);

            ProcessingManager.ProcessAsync(job);
        }

        public void CancelAsync()
        {
            ProcessingManager.CancelAsync();
        }

        public void ResumeAsync()
        {
            ProcessingManager.Resume();
        }

	    public void ResetCulture(string cultureName)
	    {
			ServiceLocator.Get.Resources.ResetCulture(cultureName);
	    }

		#endregion

		#region Properties

		private Bootstrapper<IWitController> Bootstrapper { get; set; }

        public IReadOnlyList<string> AvailableActivities => ControllerManager.AvailableActivities;
        
        public IReadOnlyList<string> AvailableVariables => ControllerManager.AvailableVariables;

        #endregion

        #region Services

        private ControllerManager ControllerManager { get; set; }
        private ProcessingManager ProcessingManager { get; set; }

        #endregion

        #region Static Properties

        public static WitEngine Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new WitEngine();
                    }
                }

                return m_instance;
            }
        }

        #endregion


    }
}
