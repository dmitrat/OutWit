using Microsoft.Extensions.Logging;
using OutWit.Common.Exceptions;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialLoop : WitActivityAdapterMultiOperators<WitActivitySpecialLoop>
    {
        #region Constructors

        public WitActivityAdapterSpecialLoop()
        {
        }

        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialLoop activity)
        {
            return new string[] { $"{activity.IterationsCount}" };
        }

        protected override WitActivitySpecialLoop DeserializeParameters(string[] parameters)
        {
            if (parameters.Length != 1)
                throw new ExceptionOf<WitActivitySpecialLoop>("Incorrect input");

            return new WitActivitySpecialLoop(int.Parse(parameters[0]));
        }

        protected override WitActivitySpecialLoop Clone(WitActivitySpecialLoop activity)
        {
            var result = new WitActivitySpecialLoop(activity.IterationsCount);

            foreach (var act in activity.Activities)
                result.Activities.Add((IWitActivity)ControllerManager.Clone(act));

            foreach (var variable in activity.Variables)
                result.Variables.Add((IWitVariable)ControllerManager.Clone(variable));

            return result;
        }

        #endregion

        #region Processing

        protected override bool ProcessInner(WitActivitySpecialLoop activity, WitVariableCollection pool, out string message)
        {
            message = Description;

            for (int i = 0; i < activity.IterationsCount; i++)
            {
                Log.LogInformation(string.Format(Resources["SpecialLoopIterationStarted"], i));
                foreach (var action in activity.Activities)
                {
                    if (ProcessingManager.IsCancelled)
                        return true;

                    if (!ControllerManager.Process(action, pool, out message))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

		#endregion


		#region Properties

		protected override string Description => Resources["SpecialLoopDescription"];

		#endregion
	}
}
