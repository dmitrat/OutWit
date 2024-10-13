﻿using OutWit.WitEngine.Data.Variables;

namespace OutWit.WitEngine.Data.Jobs
{
    public abstract class WitJobThreeParameters<TInput1, TInput2, TInput3> : WitJob
    {
        #region Constructors

        protected WitJobThreeParameters(string name, WitVariable<TInput1> parameter1, WitVariable<TInput2> parameter2, WitVariable<TInput3> parameter3) : base(name)
        {
            Input1Key = parameter1.Name;
            Input2Key = parameter2.Name;
            Input3Key = parameter3.Name;

			Variables.Add(parameter1);
            Variables.Add(parameter2);
            Variables.Add(parameter3);
		}

        #endregion

        #region Functions

        public override void UpdateParameters(params object[] parameters)
        {
            if (parameters.Length != 3)
                return;

            Input1 = (TInput1)parameters[0];
            Input2 = (TInput2)parameters[1];
            Input3 = (TInput3)parameters[2];
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

	    public TInput3 Input3
	    {
		    get => (TInput3)Variables[Input3Key].Value;
            set => Variables[Input3Key].Value = value;
        }

		public string Input1Key { get;}
        public string Input2Key { get; }
        public string Input3Key { get; }

		#endregion
	}
}