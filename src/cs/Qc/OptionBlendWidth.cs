using System;

namespace JbnLib.Qc
{
    public class OptionBlendWidth
    {
        public const string Option = "blendwidth";

        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public OptionBlendWidth()
        {
        }
        public OptionBlendWidth(int value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Option, _Value);
        }
    }
}
