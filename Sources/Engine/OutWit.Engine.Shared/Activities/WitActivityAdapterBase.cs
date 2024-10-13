using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.Common.Interfaces;
using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Interfaces;
using OutWit.Engine.Interfaces;

namespace OutWit.Engine.Shared.Activities
{
    public abstract class WitActivityAdapterBase<TActivity> : IWitSerializationAdapter, IWitProcessingAdapter
        where TActivity : IWitActivity
    {
        #region Constants

        protected const string SHIFT_PREFIX = "    ";

        #endregion

        #region Constructors

        static WitActivityAdapterBase()
        {
            ActivityType = OperatorAttribute.GetOperatorType<ActivityAttribute>(typeof(TActivity));
        }

	    protected WitActivityAdapterBase(IWitResources resources)
	    {
		    Resources = resources;
	    }

        #endregion

        #region Functions

        protected abstract string Serialize(TActivity activity, string prefix);
        protected abstract void Deserialize(string activityStr, IWitJob job); 
        protected abstract bool Process(TActivity activity, WitVariableCollection pool, out string message);

        protected abstract TActivity Clone(TActivity activity);

        #endregion

        #region IWMSerializationAdapter


        string IWitSerializationAdapter.Serialize(IWitOperator activity, string prefix)
        {
            return Serialize(Cast(activity), prefix);
        }

        void IWitSerializationAdapter.Deserialize(string activityStr, IWitJob job)
        {
            Deserialize(activityStr, job);
        }

         IWitOperator IWitSerializationAdapter.Clone(IWitOperator activity)
         {
             return Clone(Cast(activity));
         }

        #endregion

        #region IWMProcessingAdapter


        bool  IWitProcessingAdapter.Process(IWitActivity activity, WitVariableCollection pool, out string message)
        {
            return Process(Cast(activity), pool, out message);
        }

        #endregion

        #region Cast

        private TActivity Cast(IWitOperator action)
        {
            if (!(action is TActivity))
                throw new ExceptionOf<IWitActivity>(Resources.WrongActivityType);

            return (TActivity)action;
        }
        #endregion

        #region Properties

        protected static string ActivityType { get; }

	    protected IWitResources Resources { get; }

		#endregion
	}
}
