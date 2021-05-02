using JbnLib.Mdl;

namespace JbnLib.Qc
{
    public class OptionControl
    {
        private MotionFlags _Flags = MotionFlags.None;
        public MotionFlags Flags
        {
            get { return _Flags; }
            set { _Flags = value; }
        }

        public new string ToString()
        {
            return _Flags.ToString().Replace(",", "");
        }
    }
}
