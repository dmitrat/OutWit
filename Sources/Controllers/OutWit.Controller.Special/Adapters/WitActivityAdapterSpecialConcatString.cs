using System;
using System.Collections.Generic;
using System.Linq;
using OutWit.Common.Exceptions;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Activities;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialConcatString : WitActivityAdapterReturn<WitActivitySpecialConcatString>
    {
        #region Constructors
        public WitActivityAdapterSpecialConcatString() :
            base(ServiceLocator.Get.ProcessingManager, ServiceLocator.Get.Logger, ServiceLocator.Get.Resources)
        {
        }

        #endregion

        #region Processing

        protected override void ProcessInner(WitActivitySpecialConcatString action, WitVariableCollection pool, ref string message)
        {
	        var resultStr = "";

	        foreach (var value in action.StringParts.Select(pool.GetString))
		        resultStr += value;

	        pool[action.Return].Value = resultStr;

        } 

        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialConcatString activity)
        {
	        var result = new List<string> {$"{activity.Return}"};

			result.AddRange(activity.StringParts);

	        return result.ToArray();
        }

        protected override WitActivitySpecialConcatString DeserializeParameters(string[] parameters)
        {
            if (parameters.Length >= 2 )
                return new WitActivitySpecialConcatString(parameters[0], parameters.Skip(1).ToArray());

            throw new ExceptionOf<WitActivitySpecialConcatString>(Resources.IncorrectInput);
        }

        protected override WitActivitySpecialConcatString Clone(WitActivitySpecialConcatString activity)
        {
            return new WitActivitySpecialConcatString(activity.Return, activity.StringParts.ToArray());
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialConcatStringDescription"];
	    protected override string ErrorMessage => Resources["SpecialConcatStringErrorMessage"];

		#endregion


	}
}
