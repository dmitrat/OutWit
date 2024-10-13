using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Interfaces;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialTimer : WitActivityAdapterMultiOperators<WitActivitySpecialTimer>
    {
        #region Constructors

        public WitActivityAdapterSpecialTimer()
        {
        }

        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialTimer activity)
        {
            return new string[]
            {
                $"{activity.Interval}",
                $"{activity.Timeout}",
            };
        }

        protected override WitActivitySpecialTimer DeserializeParameters(string[] parameters)
        {
            if (parameters.Length == 1)
                return new WitActivitySpecialTimer(int.Parse(parameters[0]));

            if (parameters.Length == 2)
                return new WitActivitySpecialTimer(int.Parse(parameters[0]), long.Parse(parameters[1]));

            throw new ExceptionOf<WitActivitySpecialLoop>("Incorrect input");
        }

        protected override WitActivitySpecialTimer Clone(WitActivitySpecialTimer activity)
        {
            var result = new WitActivitySpecialTimer(activity.Interval, activity.Timeout);

            foreach (var act in activity.Activities)
                result.Activities.Add((IWitActivity)ControllerManager.Clone(act));

            foreach (var variable in activity.Variables)
                result.Variables.Add((IWitVariable)ControllerManager.Clone(variable));

            return result;
        }

        #endregion

        #region Processing

        protected override bool ProcessInner(WitActivitySpecialTimer activity, WitVariableCollection pool, out string message)
        {
            message = Description;

            Log.LogInformation(string.Format(Resources["MultiOperatorsStarted"], Description));

            var startTime = DateTime.Now;
            var milliseconds = 0;

            while (milliseconds < activity.Timeout)
            {
                foreach (var action in activity.Activities)
                {
                    if (ProcessingManager.IsCancelled)
                    {
                        Log.LogInformation(string.Format(Resources["MultiOperatorsCancelled"], Description));
                        return true;
                    }

                    if (!ControllerManager.Process(action, pool, out message))
                    {
                        Log.LogInformation(string.Format(Resources["MultiOperatorsFailed"], Description));
                        return false;
                    }
                }

                Thread.Sleep(activity.Interval);

                milliseconds = (int)(DateTime.Now - startTime).TotalMilliseconds;
            }

            Log.LogInformation(string.Format(Resources["MultiOperatorsTimeout"], Description));
            return true;
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialTimerDescription"];

		#endregion


	}
}
