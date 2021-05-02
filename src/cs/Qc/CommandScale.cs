using System;

namespace JbnLib.Qc
{
    public class CommandScale
    {
        public const string Command = "$scale";

        private float _Value;
        public float Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public CommandScale()
        {
        }
        public CommandScale(float value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Command, _Value);
        }
    }
}
