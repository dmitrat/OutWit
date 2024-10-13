using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Activities;
using OutWit.Engine.Shared.Interfaces;
using OutWit.Engine.Shared.Utils;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public abstract class WitActivityAdapterMultiOperators<TActivity> : WitActivityAdapterBase<TActivity>
        where TActivity : IWitJob, IWitActivity
    {
        #region Constructors

        protected WitActivityAdapterMultiOperators() : 
	        base(ServiceLocator.Get.Resources)
        {
        }


        #endregion

        #region IWMSerializationAdapter

        protected override string Serialize(TActivity activity, string preffix)
        {
            var writer = new StringWriter();

            writer.WriteLine(StringOperations.WriteOperatorHeader(ActivityType, SerializeParameters(activity), preffix));
            writer.WriteLine(StringOperations.WriteOperatorBody(activity.Variables, activity.Activities, preffix, ControllerManager));

            return writer.ToString();
        }

        protected override void Deserialize(string activityStr, IWitJob job)
        {
            StringOperations.FindBody(activityStr, out string header, out string body);

            var operators = new List<string>();
            StringOperations.SplitBody(body, operators);

            var parameters = StringOperations.ReadOperatorParameters(ActivityType, header);

            var activity = DeserializeParameters(parameters);

            foreach (var item in operators)
                ControllerManager.Deserialize(item, activity);

            job.AddActivity(activity);

        }

        #endregion

        #region IWMProcessingAdapter

        protected override bool Process(TActivity activity, WitVariableCollection pool, out string message)
        {
	        Log.LogInformation(string.Format(Resources["MultiOperatorsStarted"], Description));

            var result =  ProcessInner(activity, pool.Join(activity.Variables), out message);

            if(result)
                Log.LogInformation(string.Format(Resources["MultiOperatorsCompleted"], Description));
            else
                Log.LogError(string.Format(Resources["MultiOperatorsFailed"], Description));

            return result;
        }

        #endregion

        #region Abstract
        
        protected abstract string[] SerializeParameters(TActivity activity);

        protected abstract TActivity DeserializeParameters(string[] parameters);

        protected abstract bool ProcessInner(TActivity action, WitVariableCollection pool, out string message);

        #endregion

        #region Properties

        protected IWitControllerManager ControllerManager => ServiceLocator.Get.ControllerManager;
        protected IWitProcessingManager ProcessingManager => ServiceLocator.Get.ProcessingManager;
        protected ILogger Log => ServiceLocator.Get.Logger;

        protected abstract string Description { get; }

        #endregion
    }
}
