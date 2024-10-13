using OutWit.Controller.Variables.Model;
using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Variables;

namespace OutWit.Controller.Variables.Variables
{
    [Variable("Color")]
    public class WitVariableColor : WitVariable<WitColor>
    {
        public WitVariableColor(string name) : base(name)
        {
        }

        #region Functions

        public override string ToString()
        {
            var value = Value?.ToString() ?? "NULL";
            return $"{Name} = {value}";
        }

        #endregion
    }
}
