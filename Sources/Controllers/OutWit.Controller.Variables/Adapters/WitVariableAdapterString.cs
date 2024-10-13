using System;
using System.Linq;
using OutWit.Controller.Variables.Variables;
using OutWit.Engine.Shared.Utils;
using OutWit.Engine.Shared.Variables;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Variables.Adapters
{
    public class WitVariableAdapterString : WitVariableAdapter<WitVariableString>
    {
        public WitVariableAdapterString() :
            base(ServiceLocator.Get.ControllerManager)
        {
        }

        protected override WitVariableString DeserializeVariable(string name, string valueStr, IWitJob job)
        {
            if (string.IsNullOrEmpty(valueStr))
                return new WitVariableString(name, "");
            if (valueStr.First() == '"' && valueStr.Last() == '"')
                return new WitVariableString(name, valueStr.RemoveSymbols("\""));

            Manager.Deserialize($"{name}={valueStr};", job);
            return new WitVariableString(name, "");
        }

        protected override string SerializeVariableValue(WitVariableString variable)
        {
            if (string.IsNullOrEmpty(variable.Value))
                return "";
            return $"\"{variable.Value}\"";
        }

        protected override WitVariableString Clone(WitVariableString variable)
        {
            return new WitVariableString(variable.Name, variable.Value);
        }
    }
}
