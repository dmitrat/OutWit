using Microsoft.Extensions.Logging;
using OutWit.Common.Exceptions;
using OutWit.Controller.Variables.Activities;
using OutWit.Controller.Variables.Model;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Activities;
using OutWit.Engine.Shared.Interfaces;

namespace OutWit.Controller.Variables.Adapters
{
    public class WitActivityAdapterColor : WitActivityAdapterReturn<WitActivityColor>
    {
        #region Constructors

        public WitActivityAdapterColor() :
            base(ServiceLocator.Get.ProcessingManager, ServiceLocator.Get.Logger, ServiceLocator.Get.Resources)
        {
        } 

        #endregion

        #region Processing

        protected override void ProcessInner(WitActivityColor action, WitVariableCollection pool, ref string message)
        {
            pool[action.Color].Value = new WitColor(action.Red, action.Green, action.Blue);
        } 

        #endregion

        #region Srialization

        protected override string[] SerializeParameters(WitActivityColor activity)
        {
            return new[]
            {
                $"{activity.Color}",
                $"{activity.Red}",
                $"{activity.Green}",
                $"{activity.Blue}"
            };
        }

        protected override WitActivityColor DeserializeParameters(string[] parameters)
        {
            if (parameters.Length == 4)
                return new WitActivityColor(parameters[0], int.Parse(parameters[1]), int.Parse(parameters[2]), int.Parse(parameters[3]));

            throw new ExceptionOf<WitActivityColor>(Resources.IncorrectInput);
        }

        protected override WitActivityColor Clone(WitActivityColor activity)
        {
            return new WitActivityColor(activity.Color, activity.Red, activity.Green, activity.Blue);
        }

		#endregion

		#region Properties

		protected override string Description => Resources["ColorDescription"];
	    protected override string ErrorMessage => Resources["ColorErrorMessage"];

		#endregion



	}
}
