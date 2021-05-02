using System;

namespace JbnLib.Qc
{
    public class CommandModelName
    {
        public const string Command = "$modelname";

        private string _Value;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public CommandModelName()
        {
        }
        public CommandModelName(string value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return String.Format("{0} \"{1}\"", Command, _Value);
        }
    }
}
