using OutWit.Controller.Variables.Variables;
using OutWit.Engine.Interfaces;
using OutWit.Engine.Shared.Variables;

namespace OutWit.Controller.Variables.Adapters
{
    public class WitVariableAdapterInteger : WitVariableAdapter<WitVariableInteger>
    {
        public WitVariableAdapterInteger() : 
            base(ServiceLocator.Get.ControllerManager)
        {
        }

        protected override WitVariableInteger DeserializeVariable(string name, string valueStr, IWitJob job)
        {
            if (string.IsNullOrEmpty(valueStr))
                return new WitVariableInteger(name, 0);
            if (int.TryParse(valueStr, out var value))
                return new WitVariableInteger(name, value);

            Manager.Deserialize($"{name}={valueStr};", job);

            return new WitVariableInteger(name, 0);
        }

        protected override string SerializeVariableValue(WitVariableInteger variable)
        {
            return $"{variable.Value}";
        }

        protected override WitVariableInteger Clone(WitVariableInteger variable)
        {
            return new WitVariableInteger(variable.Name, variable.Value);
        }
    }
}
