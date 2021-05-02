using System;

namespace JbnLib.Qc
{
    public class OptionPivot
    {
        public const string Option = "pivot";

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        private float _Start;
        public float Start
        {
            get { return _Start; }
            set { _Start = value; }
        }

        private float _End;
        public float End
        {
            get { return _End; }
            set { _End = value; }
        }

        public OptionPivot()
        {
        }
        public OptionPivot(int index, float start, float end)
        {
            _Index = index;
            _Start = start;
            _End = end;
        }

        public new string ToString()
        {
            return String.Format("{0} {1} {2} {3}", Option, _Index, _Start, _End);
        }
    }
}
