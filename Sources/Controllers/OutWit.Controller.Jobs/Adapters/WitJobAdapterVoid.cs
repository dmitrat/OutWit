using System.Collections.Generic;
using OutWit.Controller.Jobs.Jobs;
using OutWit.Engine.Shared.Jobs;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Jobs.Adapters
{
    public class WitJobAdapterVoid : WitJobAdapter<WitJobVoid>
    {
        public WitJobAdapterVoid() :
            base(ServiceLocator.Get.ControllerManager)
        {
        }

        protected override IEnumerable<IWitVariable> GetVariables(WitJobVoid activity)
        {
            return activity.Variables;
        }

        protected override string[] SerializeParameters(WitJobVoid activity)
        {
            return new string[]{};
        }

        protected override WitJobVoid DeserializeParameters(string name, string[] parameters)
        {
            return new WitJobVoid(name);
        }


        protected override WitJobVoid Clone(WitJobVoid job)
        {

            var result = new WitJobVoid(job.Name);

            foreach (var activity in job.Activities)
                result.Activities.Add((IWitActivity)Manager.Clone(activity));

            foreach (var variable in job.Variables)
                result.Variables.Add((IWitVariable)Manager.Clone(variable));

            return result;
        }
    }
}
