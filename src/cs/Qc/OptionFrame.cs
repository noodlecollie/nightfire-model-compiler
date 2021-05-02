using System;

namespace JbnLib.Qc
{
    public class OptionFrame
    {
        public const string Option = "frame";

        private int _Start;
        public int Start
        {
            get { return _Start; }
            set { _Start = value; }
        }

        private int _End;
        public int End
        {
            get { return _End; }
            set { _End = value; }
        }

        public new string ToString()
        {
            return String.Format("{0} {1} {2}", Option, _Start, _End);
        }
    }
}
