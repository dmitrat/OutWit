using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OutWit.Engine.Data.Interfaces;
using OutWit.Engine.Data.Jobs;
using OutWit.Engine.Data.Status;
using OutWit.Engine.Shared.Interfaces;
using OutWit.Engine.Utils;

namespace OutWit.Engine.Services
{
    public class ProcessingManager : IWitProcessingManager
    {
        #region Classes

        private class ProcessingResult
        {
            public ProcessingResult(WitProcessingStatus status, string message)
            {
                Status = status;
                Message = message;
            }

            public WitProcessingStatus Status { get; }
            public string Message { get; }
            
        }

        #endregion

        #region Events

        public event ProcessingProgressChangedEventHandler ProcessingProgressChanged = delegate { };

        public event ProcessingCompletedEventHandler ProcessingCompleted = delegate { };

        public event ProcessingPausedEventHandler ProcessingPaused = delegate { };

        public event ProcessingReturnValueEventHandler ProcessingReturnValue = delegate { };

        public event ProcessingStartedEventHandler ProcessingStarted = delegate { };

		#endregion

		#region Constructors

		public ProcessingManager()
        {

        }

        #endregion

        #region Functions

        public WitProcessingStatus Process(WitJob job, bool lockProgress, bool logActivities, out string message)
        {
	        if (!lockProgress)
		        Resume();

			IsProgressLocked = lockProgress;

            if(logActivities)
                ServiceLocator.Get.Logger?.LogInformation("Processing started");

	        ProcessingStarted(job.Name);

            foreach (var action in job.Activities)
            {
                if (IsCancelled)
                {
                    message = "Processing cancelled";

                    if (logActivities)
                        ServiceLocator.Get.Logger?.LogInformation("Processing cancelled");

                    IsProgressLocked = false;

                    return WitProcessingStatus.Cancelled;
                }

                if (!action.Process(job.Variables, out message))
                {
                    message = $"Processing failed: {message}";
                    if (logActivities)
                        ServiceLocator.Get.Logger?.LogInformation("Processing failed");
                    IsProgressLocked = false;
                    return WitProcessingStatus.Failed;
                }
            }

            message = "Processing completed";
            if (logActivities)
                ServiceLocator.Get.Logger?.LogInformation("Processing completed");
            IsProgressLocked = false;

            return WitProcessingStatus.Completed;
        }

        public void ProcessAsync(WitJob job)
        {
            Maximum = job.StagesCount;
            Value = 0;

            CancellationToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                var status = Process(job, false, true, out var message);

                if (!IsCancelled)
                {
                    ProcessingCompleted(status, message);
                }
                else
                {
                    ProcessingCompleted(WitProcessingStatus.Cancelled, "");
                }

            }, CancellationToken.Token);
        }

        public void CancelAsync()
        {
            CancellationToken?.Cancel(false);
        }

        #endregion

        #region IWitProcessingManager

        public void Resume()
        {
            IsPaused = false;
        }

        public void Pause(string message)
        {
            IsPaused = true;

            ProcessingPaused(message);
        }

        public void ReportProgress(string message)
        {
            if(IsProgressLocked)
                return;

            ProcessingProgressChanged(++Value, Maximum, message);
        }

        public void Return(object[] value)
        {
            ProcessingReturnValue(value);
        }

        public void LockProgress()
        {
            IsProgressLocked = true;
        }

        public void UnlockProgress()
        {
            IsProgressLocked = false;
        }

        #endregion

        #region Properties

        private CancellationTokenSource CancellationToken { get; set; }

        public bool IsCancelled => CancellationToken?.IsCancellationRequested == true;

        public bool IsPaused { get; private set; }

        private bool IsProgressLocked { get; set; }

        private int Maximum { get; set; }

        private int Value { get; set; }

        #endregion
    }

    public delegate void ProcessingProgressChangedEventHandler(int value, int maximum, string message);
    public delegate void ProcessingReturnValueEventHandler(object[] value);
    public delegate void ProcessingCompletedEventHandler(WitProcessingStatus status, string message);
    public delegate void ProcessingPausedEventHandler(string message);
    public delegate void ProcessingStartedEventHandler(string jobName);



}
