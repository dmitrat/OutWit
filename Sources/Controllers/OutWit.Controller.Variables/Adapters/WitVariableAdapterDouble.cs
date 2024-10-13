using System;
using OutWit.Controller.Variables.Variables;
using OutWit.Engine.Interfaces;
using OutWit.Engine.Shared.Variables;

namespace OutWit.Controller.Variables.Adapters
{
    public class WitVariableAdapterDouble : WitVariableAdapter<WitVariableDouble>
    {
        public WitVariableAdapterDouble() :
            base(ServiceLocator.Get.ControllerManager)
        {
        }

        protected override WitVariableDouble DeserializeVariable(string name, string valueStr, IWitJob job)
        {
            if (string.IsNullOrEmpty(valueStr))
                return new WitVariableDouble(name, 0.0);
            if(double.TryParse(valueStr, out var value))
                return new WitVariableDouble(name, value);
            
            Manager.Deserialize($"{name}={valueStr};", job);

            return new WitVariableDouble(name, 0.0);

        }

        protected override string SerializeVariableValue(WitVariableDouble variable)
        {
            return $"{variable.Value}";
        }

        protected override WitVariableDouble Clone(WitVariableDouble variable)
        {
            return new WitVariableDouble(variable.Name, variable.Value);
        }
    }
}
