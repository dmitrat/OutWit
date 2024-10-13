using OutWit.Engine.Data.Variables;

namespace OutWit.Engine.Data.Jobs
{
    public abstract class WitJobTwoParameters<TInput1, TInput2> : WitJob
    {
        #region Constructors

        protected WitJobTwoParameters(string name, WitVariable<TInput1> parameter1, WitVariable<TInput2> parameter2) : base(name)
        {
            Input1Key = parameter1.Name;
            Input2Key = parameter2.Name;

            Variables.Add(parameter1);
            Variables.Add(parameter2);
        }

        #endregion

        #region Functions

        public override void UpdateParameters(params object[] parameters)
        {
            if (parameters.Length != 2)
                return;

            Input1 = (TInput1)parameters[0];
            Input2 = (TInput2)parameters[1];
        }

        #endregion

        #region Properties

        public TInput1 Input1
        {
            get => (TInput1)Variables[Input1Key].Value;
            set => Variables[Input1Key].Value = value;
        }

        public TInput2 Input2
        {
            get => (TInput2)Variables[Input2Key].Value;
            set => Variables[Input2Key].Value = value;
        }

        public string Input1Key { get;}
        public string Input2Key { get; }

        #endregion
    }
}
