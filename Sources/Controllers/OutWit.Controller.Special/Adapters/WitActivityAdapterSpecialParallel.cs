using System.Threading.Tasks;
using OutWit.Common.Exceptions;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialParallel : WitActivityAdapterMultiOperators<WitActivitySpecialParallel>
    {
        #region Constructors

        public WitActivityAdapterSpecialParallel()
        {
        }

        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialParallel activity)
        {
            return new string[] { };
        }

        protected override WitActivitySpecialParallel DeserializeParameters(string[] parameters)
        {
            if (parameters.Length != 0)
                throw new ExceptionOf<WitActivitySpecialParallel>("Incorrect input");

            return new WitActivitySpecialParallel();
        }

        protected override WitActivitySpecialParallel Clone(WitActivitySpecialParallel activity)
        {
            var result = new WitActivitySpecialParallel();

            foreach (var act in activity.Activities)
                result.Activities.Add((IWitActivity)ControllerManager.Clone(act));

            foreach (var variable in activity.Variables)
                result.Variables.Add((IWitVariable)ControllerManager.Clone(variable));

            return result;
        }

        #endregion

        #region Processing

        protected override bool ProcessInner(WitActivitySpecialParallel activity, WitVariableCollection pool, out string message)
        {
            message = Description;
            string errorMessage = "";

            ProcessingManager.ReportProgress(Description);
            ProcessingManager.LockProgress();

            var result = Parallel.ForEach(activity.Activities, (a, loopState) =>
            {
                if (!ControllerManager.Process(a, pool, out var msg))
                {
                    errorMessage = msg;
                    loopState.Break();
                }

            });

            ProcessingManager.UnlockProgress();

            if (!result.IsCompleted)
            {
                message = errorMessage;
                return false;
            }

            return true;
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialParallelDescription"];

		#endregion


	}
}
