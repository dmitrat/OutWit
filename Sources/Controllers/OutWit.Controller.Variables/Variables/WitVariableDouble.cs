using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Variables;

namespace OutWit.Controller.Variables.Variables
{
    [Variable("Double")]
    public class WitVariableDouble : WitVariable<double>
    {
        public WitVariableDouble(string name) : base(name)
        {
        }

        public WitVariableDouble(string name, double value) : base(name, value)
        {
        }
    }
}
