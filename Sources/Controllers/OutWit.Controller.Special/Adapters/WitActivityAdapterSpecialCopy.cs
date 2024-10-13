using System;
using System.IO;
using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.Common.Utils;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Activities;
using OutWit.Engine.Shared.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialCopy : WitActivityAdapterVoid<WitActivitySpecialCopy>
    {
        #region Constructors
        public WitActivityAdapterSpecialCopy() :
            base(ServiceLocator.Get.ProcessingManager, ServiceLocator.Get.Logger, ServiceLocator.Get.Resources)
        {
        }

        #endregion

        #region Processing

        protected override void ProcessInner(WitActivitySpecialCopy action, WitVariableCollection pool, ref string message)
        {
	        var from = pool.GetString(action.From);
	        var to = pool.GetString(action.To);

			if(Path.GetDirectoryName(to).CheckFolder())
				File.Copy(from, to, true);
        } 

        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialCopy activity)
        {
            return new[]
            {
                $"{activity.From}",
                $"{activity.To}"
            };
        }

        protected override WitActivitySpecialCopy DeserializeParameters(string[] parameters)
        {

            if (parameters.Length == 2)
                return new WitActivitySpecialCopy(parameters[0], parameters[1]);

            throw new ExceptionOf<WitActivitySpecialCopy>(Resources.IncorrectInput);
        }

        protected override WitActivitySpecialCopy Clone(WitActivitySpecialCopy activity)
        {
            return new WitActivitySpecialCopy(activity.From, activity.To);
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialCopyDescription"];
	    protected override string ErrorMessage => Resources["SpecialCopyErrorMessage"];

		#endregion


	}
}
