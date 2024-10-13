using OutWit.Engine.Data.Activities;
using OutWit.Engine.Data.Attributes;

namespace OutWit.Controller.Variables.Activities
{
    [Activity("WitColor")]
    public class WitActivityColor : WitActivity
    {
        public WitActivityColor(string color, int red, int green, int blue)
        {
            Color = color;
            Red = red;
            Green = green;
            Blue = blue;
        }

        public string Color { get; }
        public int Red { get;}
        public int Green { get; }
        public int Blue { get; }
    }
}
