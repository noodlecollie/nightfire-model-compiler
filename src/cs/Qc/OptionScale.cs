using System;

namespace JbnLib.Qc
{
    public class OptionScale
    {
        public const string Option = "scale";

        private float _Value;
        public float Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public OptionScale()
        {
        }
        public OptionScale(float value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Option, _Value);
        }
    }
}
