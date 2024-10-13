using System;
using OutWit.Common.Abstract;
using OutWit.Common.Values;

namespace OutWit.Controller.Variables.Model
{
    public class WitColor : ModelBase
    {
        #region Constructors

        public WitColor(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        #endregion

        #region Fnctions

        public override string ToString()
        {
            return $"{Red}, {Green}, {Blue}";
        }

        #endregion

        #region Model Base
        
        public override bool Is(ModelBase modelBase, double tolerance = DEFAULT_TOLERANCE)
        {
            if (!(modelBase is WitColor color))
                return false;

            return Red.Is(color.Red) &&
                   Green.Is(color.Green) &&
                   Blue.Is(color.Blue);
        }

        public override ModelBase Clone()
        {
            return new WitColor(Red, Green, Blue);
        } 
        
        #endregion

        #region Properties

        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }

        #endregion
    }
}
