using System;

namespace JbnLib.Qc
{
    public class CommandModel
    {
        public const string Command = "$model";

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
            set
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(value);
                if (fi.Extension.Length == 0)
                    _File = value + ".smd";
                else
                    _File = value;
            }
        }

        public CommandModel()
        {
        }
        public CommandModel(string name, string file)
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
