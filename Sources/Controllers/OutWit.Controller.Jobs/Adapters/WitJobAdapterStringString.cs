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
    public class WitJobAdapterStringString : WitJobAdapter<WmJobStringString>
    {
        public WitJobAdapterStringString() :
            base(ServiceLocator.Get.ControllerManager)
        {
        }

        protected override IEnumerable<IWitVariable> GetVariables(WmJobStringString activity)
        {
            return activity.Variables.Where(x => x.Name != activity.Input1Key && x.Name != activity.Input2Key);
        }

        protected override string[] SerializeParameters(WmJobStringString activity)
        {
            return new string[]
            {
                Manager.Serialize(activity.Variables[activity.Input1Key], "").RemoveSymbols(";"),
                Manager.Serialize(activity.Variables[activity.Input2Key], "").RemoveSymbols(";")

            };
        }

        protected override WmJobStringString DeserializeParameters(string name, string[] parameters)
        {
            var tempJob = new WitJobBase();
            Manager.Deserialize(parameters[0], tempJob);
            var variable1 = tempJob.Variables.OfType<WitVariable<string>>().Single();

            tempJob = new WitJobBase();
            Manager.Deserialize(parameters[1], tempJob);
            var variable2 = tempJob.Variables.OfType<WitVariable<string>>().Single();

            return new WmJobStringString(name, variable1, variable2);
        }


        protected override WmJobStringString Clone(WmJobStringString job)
        {
            var inputVariable1 = job.Variables[job.Input1Key];
            var inputVariable2 = job.Variables[job.Input2Key];

            var result = new WmJobStringString(job.Name, (WitVariable<string>)Manager.Clone(inputVariable1), (WitVariable<string>)Manager.Clone(inputVariable2));

            foreach (var activity in job.Activities)
                result.Activities.Add((IWitActivity)Manager.Clone(activity));

            result.Variables = new WitVariableCollection();

            foreach (var variable in job.Variables)
                result.Variables.Add((IWitVariable)Manager.Clone(variable));

            return result;
        }
    }
}
