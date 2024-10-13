using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Interfaces;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialDelayed : WitActivityAdapterMultiOperators<WitActivitySpecialDelayed>
    {
        #region Constructors

        public WitActivityAdapterSpecialDelayed()
        {
        }

        #endregion

        #region Processing

        protected override bool ProcessInner(WitActivitySpecialDelayed activity, WitVariableCollection pool, out string message)
        {
            message = Description;

            System.Threading.Thread.Sleep(activity.Delay);

            foreach (IWitActivity action in activity.Activities)
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

        protected override string[] SerializeParameters(WitActivitySpecialDelayed activity)
        {
            return new string[] { $"{activity.Delay}" };
        }

        protected override WitActivitySpecialDelayed DeserializeParameters(string[] parameters)
        {
            if (parameters.Length != 1)
                throw new ExceptionOf<WitActivitySpecialDelayed>(Resources.IncorrectInput);

            return new WitActivitySpecialDelayed(int.Parse(parameters[0]));
        }

        protected override WitActivitySpecialDelayed Clone(WitActivitySpecialDelayed activity)
        {
            var result = new WitActivitySpecialDelayed(activity.Delay);

            foreach (var act in activity.Activities)
                result.Activities.Add((IWitActivity)ControllerManager.Clone(act));

            foreach (var variable in activity.Variables)
                result.Variables.Add((IWitVariable)ControllerManager.Clone(variable));
            
            return result;
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialDelayedDescription"];

		#endregion


	}
}
