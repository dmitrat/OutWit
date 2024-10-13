using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutWit.Engine.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class VariableAttribute : OperatorAttribute
    {
        public VariableAttribute(string type) : base(type)
        {
        }
    }
}
