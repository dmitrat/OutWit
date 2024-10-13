using System;
using System.Threading;
using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Activities;
using OutWit.Engine.Shared.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialPause : WitActivityAdapterVoid<WitActivitySpecialPause>
    {
        #region Constructors

        public WitActivityAdapterSpecialPause() :
            base(ServiceLocator.Get.ProcessingManager, ServiceLocator.Get.Logger, ServiceLocator.Get.Resources)
        {
        }

        #endregion

        #region Processing

        protected override void ProcessInner(WitActivitySpecialPause action, WitVariableCollection pool, ref string message)
        {
            var startTime = DateTime.Now;
            var milliseconds = 0;

            ProcessingManager.Pause(action.Message);

	        Thread.Sleep(10);

			while (milliseconds < action.Timeout)
            {
                if (ProcessingManager.IsCancelled || !ProcessingManager.IsPaused)
                    return;

                Thread.Sleep(50);

                milliseconds = (int)(DateTime.Now - startTime).TotalMilliseconds;
            }

            ProcessingManager.Resume();
        } 

        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialPause activity)
        {
            return new[]
            {
                $"\"{activity.Message}\"",
                $"{activity.Timeout}",
            };
        }

        protected override WitActivitySpecialPause DeserializeParameters(string[] parameters)
        {
            if (parameters.Length == 1)
                return new WitActivitySpecialPause(parameters[0]);

            if (parameters.Length == 2)
                return new WitActivitySpecialPause(parameters[0], long.Parse(parameters[1]));

            throw new ExceptionOf<WitActivitySpecialPause>(Resources.IncorrectInput);
        }

        protected override WitActivitySpecialPause Clone(WitActivitySpecialPause activity)
        {
            return new WitActivitySpecialPause(activity.Message, activity.Timeout);
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialPauseDescription"];
	    protected override string ErrorMessage => Resources["SpecialPauseErrorMessage"];

		#endregion


	}
}
