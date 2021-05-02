using JbnLib.Mdl;
using System;

namespace JbnLib.Qc
{
    public class OptionBlend
    {
        public const string Option = "blend";

        private MotionFlags _Type;
        public MotionFlags Type
        {
            get { return _Type; }
            set { _Type = value; }
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

        public OptionBlend()
        {
        }
        public OptionBlend(MotionFlags type, float start, float end)
        {
            _Type = type;
            _Start = start;
            _End = end;
        }

        public new string ToString()
        {
            return String.Format("{0} {1} {2} {3}", Option, _Type.ToString(), _Start, _End);
        }
    }
}
