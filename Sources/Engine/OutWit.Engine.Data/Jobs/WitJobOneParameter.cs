using OutWit.Engine.Data.Variables;

namespace OutWit.Engine.Data.Jobs
{
    public abstract class WitJobOneParameter<TInput> : WitJob
    {
        #region Constructors

        protected WitJobOneParameter(string name, WitVariable<TInput> parameter) : base(name)
        {
            
            InputKey = parameter.Name;

            Variables.Add(parameter);
        }

        #endregion

        #region Functions

        public override void UpdateParameters(params object[] parameters)
        {
            if (parameters.Length == 0)
                return;

            Input = (TInput)parameters[0];
        }

        #endregion

        #region Properties

        public TInput Input
        {
            get => (TInput)Variables[InputKey].Value;
            set => Variables[InputKey].Value = value;
        }
        
        public string InputKey { get;}

        #endregion
    }
}
