using System;

namespace JbnLib.Qc
{
    public class CommandCd
    {
        public const string Command = "$cd";

        private string _Directory;
        public string Directory
        {
            get { return _Directory; }
            set { _Directory = value; }
        }

        public CommandCd()
        {
        }
        public CommandCd(string directory)
        {
            _Directory = directory;
        }

        public new string ToString()
        {
            return String.Format("{0} \"{1}\"", Command, _Directory);
        }
    }
}
