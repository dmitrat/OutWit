using System.Collections.Generic;
using System.Linq;
using OutWit.Controller.Jobs.Jobs;
using OutWit.Engine.Data.Jobs;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Jobs;
using OutWit.Engine.Shared.Utils;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Jobs.Adapters
{
    public class WitJobAdapterString : WitJobAdapter<WitJobString>
    {
        public WitJobAdapterString() :
            base(ServiceLocator.Get.ControllerManager)
        {
        }

        protected override IEnumerable<IWitVariable> GetVariables(WitJobString activity)
        {
            return activity.Variables.Where(x => x.Name != activity.InputKey);
        }

        protected override string[] SerializeParameters(WitJobString activity)
        {
            return new string[]
            {
                Manager.Serialize(activity.Variables[activity.InputKey], "").RemoveSymbols(";")

            };
        }

        protected override WitJobString DeserializeParameters(string name, string[] parameters)
        {
            var tempJob = new WitJobBase();
            Manager.Deserialize(parameters[0], tempJob);
            return new WitJobString(name, tempJob.Variables.OfType<WitVariable<string>>().Single());
        }


        protected override WitJobString Clone(WitJobString job)
        {
            var inputVariable = job.Variables[job.InputKey];

            var result = new WitJobString(job.Name, (WitVariable<string>)Manager.Clone(inputVariable));

            foreach (var activity in job.Activities)
                result.Activities.Add((IWitActivity)Manager.Clone(activity));

            result.Variables = new WitVariableCollection();

            foreach (var variable in job.Variables)
                result.Variables.Add((IWitVariable)Manager.Clone(variable));

            return result;
        }
    }
}
