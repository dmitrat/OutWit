using System;
using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Activities;
using OutWit.Engine.Shared.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialTest : WitActivityAdapterReturn<WitActivitySpecialTest>
    {
        #region Constructors
        public WitActivityAdapterSpecialTest() :
            base(ServiceLocator.Get.ProcessingManager, ServiceLocator.Get.Logger, ServiceLocator.Get.Resources)
        {
        }

        #endregion

        #region Processing

        protected override void ProcessInner(WitActivitySpecialTest action, WitVariableCollection pool, ref string message)
        {
            var time = DateTime.Now;

            Console.WriteLine(@"Test task started: {0}, time: {1}:{2}:{3}.{4}", action.Message, time.Hour, time.Minute, time.Second, time.Millisecond);

            if (action.ThrowException)
                throw new ExceptionOf<WitActivitySpecialTest>();

            System.Threading.Thread.Sleep(50);
        } 

        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialTest activity)
        {
            return new[]
            {
                $"{activity.Return}",
                $"\"{activity.Message}\"",
                $"{activity.ThrowException}"
            };
        }

        protected override WitActivitySpecialTest DeserializeParameters(string[] parameters)
        {
            if (parameters.Length == 3)
                return new WitActivitySpecialTest(parameters[0], parameters[1], bool.Parse(parameters[2]));

            if (parameters.Length == 2)
                return new WitActivitySpecialTest("", parameters[0], bool.Parse(parameters[1]));

            throw new ExceptionOf<WitActivitySpecialTest>(Resources.IncorrectInput);
        }

        protected override WitActivitySpecialTest Clone(WitActivitySpecialTest activity)
        {
            return new WitActivitySpecialTest(activity.Return, activity.Message, activity.ThrowException);
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialTestDescription"];
	    protected override string ErrorMessage => Resources["SpecialTestErrorMessage"];

		#endregion


	}
}
