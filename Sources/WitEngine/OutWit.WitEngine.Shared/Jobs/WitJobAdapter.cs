using System.Collections.Generic;
using System.IO;
using OutWit.WitEngine.Data.Jobs;
using OutWit.WitEngine.Shared.Interfaces;
using OutWit.WitEngine.Shared.Utils;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Shared.Jobs
{
    public abstract class WitJobAdapter<TJob> : WitJobAdapterBase<TJob>
        where TJob : WitJob
    {
        #region Constructors

        protected WitJobAdapter(IWitControllerManager manager)
        {
            Manager = manager;
        }


        #endregion

        protected override string Serialize(TJob activity, string prefix)
        {
            var writer = new StringWriter();

            writer.WriteLine(StringOperations.WriteOperatorHeader($"Job:{activity.Name}", SerializeParameters(activity), ""));
            writer.WriteLine(StringOperations.WriteOperatorBody(GetVariables(activity), activity.Activities, "", Manager));

            return writer.ToString();
        }

        protected override void Deserialize(string activityStr, IWitJob job)
        {
            StringOperations.FindBody(activityStr, out string header, out string body);

            var jobName = StringOperations.ReadJobName(header);

            var operators = new List<string>();
            StringOperations.SplitBody(body, operators);

            var parameters = StringOperations.ReadOperatorParameters($"Job:{jobName}", header);

            var activity = DeserializeParameters(jobName, parameters);

            foreach (var item in operators)
                Manager.Deserialize(item, activity);

            job.AddActivity(activity);
        }

        #region Abstract

        protected abstract IEnumerable<IWitVariable> GetVariables(TJob activity);

        protected abstract string[] SerializeParameters(TJob activity);

        protected abstract TJob DeserializeParameters(string name, string[] parameters);

        #endregion

        #region Properties

        protected IWitControllerManager Manager { get; }

        #endregion
    }
}
