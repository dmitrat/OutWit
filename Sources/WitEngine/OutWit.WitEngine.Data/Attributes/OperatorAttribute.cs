using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OutWit.WitEngine.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class OperatorAttribute : System.Attribute
    {
        protected OperatorAttribute(string type)
        {
            Type = type;
        }

        public string Type { get; }

        public static string GetOperatorType<TOperator>(Type type)
            where TOperator : OperatorAttribute
        {
            var typeInfo = type.GetTypeInfo();

            var activityAttribute = typeInfo.GetCustomAttributes().OfType<TOperator>().Single();

            return activityAttribute.Type;
        }
    }
}
