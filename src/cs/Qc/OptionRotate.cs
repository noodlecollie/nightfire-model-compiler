using System;

namespace JbnLib.Qc
{
    public class OptionRotate
    {
        public const string Option = "rotate";

        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public OptionRotate()
        {
        }
        public OptionRotate(int value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Option, _Value);
        }
    }
}
