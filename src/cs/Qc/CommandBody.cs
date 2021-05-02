using System;

namespace JbnLib.Qc
{
    public class CommandBody
    {
        public const string Command = "$body";

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _File;
        public string File
        {
            get { return _File; }
            set { _File = value; }
        }

        public CommandBody()
        {
        }
        public CommandBody(string name, string file)
        {
            _Name = name;
            _File = file;
        }

        public new string ToString()
        {
            return String.Format("{0} \"{1}\" \"{2}\"", Command, _Name, _File);
        }
    }
}
