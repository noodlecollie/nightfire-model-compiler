using System;
using JbnLib.Mdl;

namespace JbnLib.Qc
{
    public class CommandFlags
    {
        public const string Command = "$flags";

        private TypeFlag _Value;
        public TypeFlag Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public CommandFlags()
        {
        }
        public CommandFlags(TypeFlag value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return String.Format("{0} {1} ", Command, (int)_Value);
        }
    }
}
