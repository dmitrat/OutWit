using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OutWit.Common.Exceptions;
using OutWit.Engine;
using OutWit.Engine.Data.Interfaces;
using OutWit.Engine.Data.Jobs;
using OutWit.Engine.Data.Status;
using OutWit.Engine.Utils;

namespace OutWit.Demo.Services
{
    public class EngineManager : IWItEngineManager
    {
        #region Constructors
        
        public EngineManager()
        {
            WitEngine.Instance.Reload(ServiceLocator.Get.ResourcesManager, ServiceLocator.Get.LogManager.LoggerFactory.CreateLogger(nameof(WitEngine)));
            
            InitDefaults();
            InitEvents();
        }

        #endregion

        #region Initialization

        private void InitDefaults()
        {
            EngineStatus = WitEngineStatus.Idle;
            ProcessingStatus = WitProcessingStatus.Completed;
            Output = "";
            Message = "";

            ResetEvent = new AutoResetEvent(false);
        }

        private void InitEvents()
        {
            WitEngine.Instance.ProcessingProgressChanged += OnProcessingProgressChanged;
            WitEngine.Instance.ProcessingCompleted += OnProcessingCompleted;
            WitEngine.Instance.ProcessingPaused += OnProcessingPaused;
            WitEngine.Instance.ProcessingReturnValue += OnProcessingReturnValue;
            WitEngine.Instance.ProcessingStarted += OnProcessingStarted;
        }

        #endregion


        #region Functions

        public WitJob Compile(string jobString)
        {
            return jobString.Deserialize();
        }

        public WitProcessingStatus RunFast(WitJob job)
        {
            if (EngineStatus != WitEngineStatus.Idle)
                throw new ExceptionOf<WitEngine>("Engine is busy");

            ResetEvent.Reset();
            WitEngine.Instance.ProcessAsync(job);

            ResetEvent.WaitOne();

            return ProcessingStatus;
        } 
        
        #endregion

        #region Event Handlers

        private void OnProcessingStarted(string jobName)
        {
            EngineStatus = WitEngineStatus.InProgress;
        }

        private void OnProcessingReturnValue(object[] value)
        {
            Return = value;
        }

        private void OnProcessingPaused(string message)
        {
            EngineStatus = WitEngineStatus.Paused;
        }

        private void OnProcessingCompleted(WitProcessingStatus status, string message)
        {
            EngineStatus = WitEngineStatus.Idle;
            ProcessingStatus = status;
            
            Message = message;
            
            ResetEvent.Set();
        }

        private void OnProcessingProgressChanged(int value, int maximum, string message)
        {
            
        }

        #endregion

        #region Properties
        
        private AutoResetEvent ResetEvent { get; set; }

        public WitProcessingStatus ProcessingStatus { get; private set; }
        
        public WitEngineStatus EngineStatus { get; private set; }

        public IReadOnlyList<object> Return { get; private set; }

        public string Output { get; private set; }
        
        public string Message { get; private set; }

        public IReadOnlyList<string> AvailableActivities => WitEngine.Instance.AvailableActivities;
        
        public IReadOnlyList<string> AvailableVariables => WitEngine.Instance.AvailableVariables;

        #endregion
    }
}
