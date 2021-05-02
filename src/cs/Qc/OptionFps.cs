using System;

namespace JbnLib.Qc
{
    public class OptionFps
    {
        public const string Option = "fps";

        private float _Value;
        public float Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public OptionFps()
        {
        }
        public OptionFps(float value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Option, _Value);
        }
    }
}
