using System;
using System.Runtime.Serialization;
using OutWit.WitEngine.Data.Attributes;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Data.Variables
{
    public abstract class WitVariable<TValue> : IWitVariable
    {
        #region Constructors

        private WitVariable() : 
            this("", default(TValue))
        {
        }

        protected WitVariable(string name) : 
            this(name, default(TValue))
        {
        }


        protected WitVariable(string name,  TValue value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region Functions

        public override string ToString()
        {
            return $"{Name} = {Value}";
        }

	    #endregion

        #region Properties

        public TValue Value { get; set; }

        public string Type => OperatorAttribute.GetOperatorType<VariableAttribute>(GetType());
        
        public string Name { get;}

        #endregion

        #region IWMVariable

        object IWitVariable.Value
        {
            get => Value;
            set => Value = (TValue)value;
        } 

        #endregion
    }
}
