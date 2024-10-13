using System;
using System.Linq;
using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Activities;
using OutWit.Engine.Shared.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialReturn : WitActivityAdapterVoid<WitActivitySpecialReturn>
    {
        #region Constructors
        public WitActivityAdapterSpecialReturn() :
            base(ServiceLocator.Get.ProcessingManager, ServiceLocator.Get.Logger, ServiceLocator.Get.Resources)
        {
        }

        #endregion

        #region Processing

        protected override void ProcessInner(WitActivitySpecialReturn action, WitVariableCollection pool, ref string message)
        {
	        var values = new object[action.Value.Length];
	        for (int i = 0; i < action.Value.Length; i++)
	        {
		        var valueKey = action.Value[i];
		        if (pool.Contains(valueKey))
			        values[i] = pool[valueKey].Value;
				else
					values[i] = valueKey;
	        }

			

            ProcessingManager.Return(values);
        } 

        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialReturn activity)
        {
	        return activity.Value;
        }

        protected override WitActivitySpecialReturn DeserializeParameters(string[] parameters)
        {
            if (parameters.Length > 0)
                return new WitActivitySpecialReturn(parameters);

            throw new ExceptionOf<WitActivitySpecialReturn>(Resources.IncorrectInput);
        }

        protected override WitActivitySpecialReturn Clone(WitActivitySpecialReturn activity)
        {
            return new WitActivitySpecialReturn(activity.Value.ToArray());
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialReturnDescription"];
	    protected override string ErrorMessage => Resources["SpecialReturnErrorMessage"];

		#endregion


	}
}
