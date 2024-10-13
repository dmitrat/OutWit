using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.WitEngine.Shared.Interfaces;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Shared.Jobs
{
    public abstract class WitJobAdapterBase<TJob> : IWitSerializationAdapter
        where TJob : IWitJob
    {
        #region Constants

        protected const string SHIFT_PREFIX = "    ";

        #endregion

        #region Functions

        protected abstract string Serialize(TJob activity, string prefix);
        protected abstract void Deserialize(string activityStr, IWitJob job);

        protected abstract TJob Clone(TJob job);

        #endregion

        #region IWMActivityAdapter


        string IWitSerializationAdapter.Serialize(IWitOperator activity, string prefix)
        {
            return Serialize(Cast(activity), prefix);
        }

        void IWitSerializationAdapter.Deserialize(string activityStr, IWitJob job)
        {
            Deserialize(activityStr, job);
        }

        IWitOperator IWitSerializationAdapter.Clone(IWitOperator job)
        {
            return Clone(Cast(job));
        }

        #endregion

        #region Cast

        private static TJob Cast(IWitOperator action)
        {
            if (!(action is TJob))
                throw new ExceptionOf<IWitVariable>("Wrong variable type");

            return (TJob)action;
        }
        #endregion
    }
}
