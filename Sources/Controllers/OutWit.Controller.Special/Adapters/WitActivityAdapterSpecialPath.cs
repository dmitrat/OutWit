using System;
using System.Collections.Generic;
using System.Linq;
using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.Common.Utils;
using OutWit.Controller.Special.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Activities;
using OutWit.Engine.Shared.Interfaces;

namespace OutWit.Controller.Special.Adapters
{
    public class WitActivityAdapterSpecialPath : WitActivityAdapterReturn<WitActivitySpecialPath>
    {
        #region Constructors
        public WitActivityAdapterSpecialPath() :
            base(ServiceLocator.Get.ProcessingManager, ServiceLocator.Get.Logger, ServiceLocator.Get.Resources)
        {
        }

        #endregion

        #region Processing

        protected override void ProcessInner(WitActivitySpecialPath action, WitVariableCollection pool, ref string message)
        {
	        var path = pool.GetString(action.PathParts[0]);

	        for (int i = 1; i < action.PathParts.Length; i++)
		        path = path.AppendPath(pool.GetString(action.PathParts[i]));

	        pool[action.Return].Value = path;

        } 

        #endregion

        #region Serialization

        protected override string[] SerializeParameters(WitActivitySpecialPath activity)
        {
	        var result = new List<string> {$"{activity.Return}"};

			result.AddRange(activity.PathParts);

	        return result.ToArray();
        }

        protected override WitActivitySpecialPath DeserializeParameters(string[] parameters)
        {
            if (parameters.Length >= 2 )
                return new WitActivitySpecialPath(parameters[0], parameters.Skip(1).ToArray());

            throw new ExceptionOf<WitActivitySpecialPath>(Resources.IncorrectInput);
        }

        protected override WitActivitySpecialPath Clone(WitActivitySpecialPath activity)
        {
            return new WitActivitySpecialPath(activity.Return, activity.PathParts.ToArray());
        }

		#endregion

		#region Properties

		protected override string Description => Resources["SpecialPathDescription"];
	    protected override string ErrorMessage => Resources["SpecialPathErrorMessage"];

		#endregion


	}
}
