using System;
using System.Collections.Generic;
using System.Linq;
using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Activities;
using OutWit.Engine.Shared.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialUniqueName : WitActivityAdapterReturn<WitActivitySpecialUniqueName>
    {
        #region Constructors
        public WitActivityAdapterSpecialUniqueName() :
            base(ServiceLocator.Get.ProcessingManager, ServiceLocator.Get.Logger, ServiceLocator.Get.Resources)
        {
        }

        #endregion

        #region Processing

        protected override void ProcessInner(WitActivitySpecialUniqueName action, WitVariableCollection pool, ref string message)
        {
	        pool[action.Return].Value = $"{DateTime.Now.Ticks}{action.Extension}";
        } 

        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialUniqueName activity)
        {
	        return new[]
	        {
		        $"{activity.Return}",
		        $"\"{activity.Extension}\""
	        };
		}

        protected override WitActivitySpecialUniqueName DeserializeParameters(string[] parameters)
        {
            if (parameters.Length == 1 )
                return new WitActivitySpecialUniqueName(parameters[0]);

	        if (parameters.Length == 2)
		        return new WitActivitySpecialUniqueName(parameters[0], parameters[1]);

			throw new ExceptionOf<WitActivitySpecialUniqueName>(Resources.IncorrectInput);
        }

        protected override WitActivitySpecialUniqueName Clone(WitActivitySpecialUniqueName activity)
        {
            return new WitActivitySpecialUniqueName(activity.Return, activity.Extension);
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialUniqueNameDescription"];
	    protected override string ErrorMessage => Resources["SpecialUniqueNameErrorMessage"];

		#endregion


	}
}
