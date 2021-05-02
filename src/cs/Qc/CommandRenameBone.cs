using System;

namespace JbnLib.Qc
{
    public class CommandRenameBone
    {
        public const string Command = "$renamebone";

        private string _OldName;
        public string OldName
        {
            get { return _OldName; }
            set { _OldName = value; }
        }

        private string _NewName;
        public string NewName
        {
            get { return _NewName; }
            set { _NewName = value; }
        }

        public new string ToString()
        {
            return String.Format("{0} \"{1}\" \"{2}\"", Command, _OldName, _NewName);
        }
    }
}
