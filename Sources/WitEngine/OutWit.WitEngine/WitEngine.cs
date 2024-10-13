using System;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using OutWit.WitEngine.Data.Jobs;
using OutWit.WitEngine.Services;
using OutWit.WitEngine.Shared.Interfaces;
using OutWit.WitEngine.Interfaces;
using OutWit.Common.MEF;
using System.Reflection;
using OutWit.Common.Configuration;
using OutWit.Common.Interfaces;
using OutWit.Common.Utils;
using OutWit.WitEngine.Properties;

namespace OutWit.WitEngine
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
            ServiceLocator.Get.Register<IResources>(new ResourcesMerged(ServiceLocator.Get.ResourcesManager));

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

        public void Reload(ILogger logger = null, string moduleFolder = null)
        {
            if (string.IsNullOrEmpty(moduleFolder))
                moduleFolder = Assembly.GetExecutingAssembly().AssemblyDirectory().AppendPath(DEFAULT_MODULE_PATH);

            if (logger != null)
                ServiceLocator.Get.Register<ILogger>(logger);

            //ModuleManager.Clear();

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

            ProcessingManager.ProcessAssync(job);
        }

        public void CancelAsync()
        {
            ProcessingManager.CancelAsync();
        }

	    public void Abort()
	    {
			ProcessingManager.Abort();
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
