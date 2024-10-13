using OutWit.Controller.Variables.Model;
using OutWit.Controller.Variables.Variables;
using OutWit.Engine.Interfaces;
using OutWit.Engine.Shared.Variables;

namespace OutWit.Controller.Variables.Adapters
{
    public class WitVariableAdapterColor : WitVariableAdapter<WitVariableColor>
    {
        public WitVariableAdapterColor() : 
            base(ServiceLocator.Get.ControllerManager)
        {
        }

        protected override WitVariableColor DeserializeVariable(string name, string valueStr, IWitJob job)
        {
            if (string.IsNullOrEmpty(valueStr) || valueStr == "NULL")
                return new WitVariableColor(name);

            Manager.Deserialize($"{name}={valueStr};", job);

            return new WitVariableColor(name);
        }

        protected override string SerializeVariableValue(WitVariableColor variable)
        {
            return "NULL";
        }

        protected override WitVariableColor Clone(WitVariableColor variable)
        {
            return new WitVariableColor(variable.Name)
            {
                Value = variable.Value == null
                    ? null
                    : new WitColor(variable.Value.Red, variable.Value.Green, variable.Value.Blue)
            };
        }
    }
}
