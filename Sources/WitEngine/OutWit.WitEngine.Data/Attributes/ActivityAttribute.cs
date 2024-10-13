using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutWit.WitEngine.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ActivityAttribute : OperatorAttribute
    {
        public ActivityAttribute(string type) : base(type)
        {
        }
    }
}
