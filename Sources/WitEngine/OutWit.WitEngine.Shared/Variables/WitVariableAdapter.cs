using OutWit.Common;
using OutWit.Common.Exceptions;
using OutWit.WitEngine.Shared.Interfaces;
using OutWit.WitEngine.Shared.Utils;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Shared.Variables
{
    public abstract class WitVariableAdapter<TVariable> : WitVariableAdapterBase<TVariable>
        where TVariable : IWitVariable
    {
        protected WitVariableAdapter(IWitControllerManager manager)
        {
            Manager = manager;
        }

        protected override string Serialize(TVariable variable, string prefix)
        {
            var value = SerializeVariableValue(variable);

            if (string.IsNullOrEmpty(value))
                return $"{prefix}{variable.Type}:{variable.Name};";

            return $"{prefix}{variable.Type}:{variable.Name} = {SerializeVariableValue(variable)};";
        }

        protected override void Deserialize(string variableStr, IWitJob job)
        {
            variableStr = variableStr.Remove(0, variableStr.IndexOf(':') + 1).RemoveSymbols(";");

            var parts = variableStr.Split('=');

            if(parts.Length == 2)
                job.AddVariable(DeserializeVariable(parts[0], parts[1], job));
            else if(parts.Length == 1)
                job.AddVariable(DeserializeVariable(parts[0], "", job));
            else
                throw new ExceptionOf<TVariable>("Incorrect input");
        }

        protected abstract TVariable DeserializeVariable(string name, string valueStr, IWitJob job);
        protected abstract string SerializeVariableValue(TVariable variable);

        protected IWitControllerManager Manager { get; }
    }
}
