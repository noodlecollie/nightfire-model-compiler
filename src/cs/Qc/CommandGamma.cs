using System;

namespace JbnLib.Qc
{
    public class CommandGamma
    {
        public const string Command = "$gamma";

        private float _Value;
        public float Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public CommandGamma()
        {
        }
        public CommandGamma(float value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Command, _Value);
        }
    }
}
