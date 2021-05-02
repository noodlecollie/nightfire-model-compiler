using System;
using JbnLib.Mdl;

namespace JbnLib.Qc
{
    public class CommandEyePosition
    {
        public const string Command = "$eyeposition";

        private EyePosition _Value = new EyePosition();
        public EyePosition Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public CommandEyePosition()
        {
        }
        public CommandEyePosition(EyePosition value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Command, _Value.ToString());
        }
    }
}
