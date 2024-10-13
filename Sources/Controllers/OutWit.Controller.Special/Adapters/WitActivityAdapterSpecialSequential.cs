using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialSequential : WitActivityAdapterMultiOperators<WitActivitySpecialSequential>
    {
        #region Constructors

        public WitActivityAdapterSpecialSequential()
        {
        }

        #endregion

        #region Processing

        protected override bool ProcessInner(WitActivitySpecialSequential activity, WitVariableCollection pool, out string message)
        {
            message = Description;

            foreach (var action in activity.Activities)
            {
                if (ProcessingManager.IsCancelled)
                    return true;

                if (!ControllerManager.Process(action, pool, out message))
                {
                    return false;
                }
            }

            return true;
        }



        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialSequential activity)
        {
            return new string[] { };
        }

        protected override WitActivitySpecialSequential DeserializeParameters(string[] parameters)
        {
            if (parameters.Length != 0)
                throw new ExceptionOf<WitActivitySpecialSequential>(Resources.IncorrectInput);

            return new WitActivitySpecialSequential();
        }

        protected override WitActivitySpecialSequential Clone(WitActivitySpecialSequential activity)
        {
            var result = new WitActivitySpecialSequential();

            foreach (var act in activity.Activities)
                result.Activities.Add((IWitActivity)ControllerManager.Clone(act));

            foreach (var variable in activity.Variables)
                result.Variables.Add((IWitVariable)ControllerManager.Clone(variable));

            return result;
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialSequentialDescription"];

		#endregion



	}
}
