using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.WitEngine.Data.Attributes;
using OutWit.WitEngine.Shared.Interfaces;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Shared.Variables
{
    public abstract class WitVariableAdapterBase<TVariable> : IWitSerializationAdapter
        where TVariable : IWitVariable
    {
        #region Constants

        protected const string SHIFT_PREFIX = "    ";

        #endregion

        #region Constructors

        static WitVariableAdapterBase()
        {
            VariableType = OperatorAttribute.GetOperatorType<VariableAttribute>(typeof(TVariable));
        }

        #endregion

        #region Functions

        protected abstract string Serialize(TVariable variable, string prefix);
        protected abstract void Deserialize(string variableStr, IWitJob job);

        protected abstract TVariable Clone(TVariable variable);

        #endregion

        #region IWMActivityAdapter


        string IWitSerializationAdapter.Serialize(IWitOperator activity, string prefix)
        {
            return Serialize(Cast(activity), prefix);
        }

        void IWitSerializationAdapter.Deserialize(string variableStr, IWitJob job)
        {
            Deserialize(variableStr, job);
        }

        IWitOperator IWitSerializationAdapter.Clone(IWitOperator variable)
        {
            return Clone(Cast(variable));
        }

        #endregion

        #region Cast

        private static TVariable Cast(IWitOperator action)
        {
            if (!(action is TVariable))
                throw new ExceptionOf<IWitVariable>("Wrong variable type");

            return (TVariable)action;
        }
        #endregion

        #region Properties

        protected static string VariableType { get; }

        #endregion
    }
}
