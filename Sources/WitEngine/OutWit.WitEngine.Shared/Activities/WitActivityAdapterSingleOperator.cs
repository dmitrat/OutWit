using System;
using System.IO;
using Microsoft.Extensions.Logging;
using OutWit.Common.Interfaces;
using OutWit.WitEngine.Data.Variables;
using OutWit.WitEngine.Shared.Interfaces;
using OutWit.WitEngine.Shared.Utils;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Shared.Activities
{
    public abstract class WitActivityAdapterSingleOperator<TActivity> : WitActivityAdapterBase<TActivity>
        where TActivity : IWitActivity
    {
        #region Constructors

        protected WitActivityAdapterSingleOperator(IWitProcessingManager processingManager, ILogger logger, IWitResources resources) : 
	        base(resources)
        {
            ProcessingManager = processingManager;
            Logger = logger;
        } 

        #endregion

        #region IWMSerializationAdapter

        protected override void Deserialize(string activityStr, IWitJob job)
        {
            job.AddActivity(DeserializeParameters(StringOperations.ReadOperatorParameters(ActivityType, activityStr)));
        }

        #endregion

        #region IWMProcessingAdapter

        protected override bool Process(TActivity action, WitVariableCollection pool, out string message)
        {
            message = ErrorMessage;

            try
            {
                Logger?.LogInformation(Description);

                ProcessingManager.ReportProgress(Description);

            #if DEBUG

                var start = DateTime.Now;
                Console.WriteLine(@"Task started: {0}, time: {1}:{2}:{3}.{4}", Description, start.Hour, start.Minute, start.Second, start.Millisecond);

            #endif

            #if MOCK
                System.Threading.Thread.Sleep(100);
            #else
                ProcessInner(action, pool, ref message);
            #endif

            }
            catch (Exception e)
            {
                Logger?.LogError(e, message);

            #if DEBUG

                var time = DateTime.Now;
                Console.WriteLine(@"Task failed: {0}, time: {1}:{2}:{3}.{4}", message, time.Hour, time.Minute, time.Second, time.Millisecond);

            #endif

                return false;
            }

        #if DEBUG

            var end = DateTime.Now;
            Console.WriteLine(@"Task ended: {0}, time: {1}:{2}:{3}.{4}", Description, end.Hour, end.Minute, end.Second, end.Millisecond);

        #endif

            message = "OK";
            return true;
        }

        #endregion

        #region Functions

        protected abstract void ProcessInner(TActivity action, WitVariableCollection pool, ref string message);

        protected abstract string[] SerializeParameters(TActivity activity);

        protected abstract TActivity DeserializeParameters(string[] parameters);

        #endregion

        #region Properties

        protected IWitProcessingManager ProcessingManager { get; }

        protected ILogger Logger { get; }

        protected abstract string Description { get; }

        protected abstract string ErrorMessage { get; }


        #endregion
    }
}
