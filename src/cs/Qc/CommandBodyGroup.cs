using System;
using System.Collections.Generic;

namespace JbnLib.Qc
{
    public class CommandBodyGroup
    {
        public const string Command = "$bodygroup";

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private List<BodyGroupItem> _BodyCollection = new List<BodyGroupItem>();
        public List<BodyGroupItem> BodyCollection
        {
            get { return _BodyCollection; }
            set { _BodyCollection = value; }
        }

        public CommandBodyGroup()
        {
        }
        public CommandBodyGroup(string name)
        {
            _Name = name;
        }

        public new string ToString()
        {
            string output = Command + " \"" + _Name + "\"\r\n{\r\n";
            foreach (BodyGroupItem bgi in _BodyCollection)
                output += "\t" + bgi.ToString() + "\r\n";
            return output + "}";
        }
    }

    public class BodyGroupItem
    {
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

        public BodyGroupItem()
        {
        }
        public BodyGroupItem(string name)
        {
            _Name = name;
        }
        public BodyGroupItem(string name, string file)
        {
            _Name = name;
            _File = file;
        }

        public new string ToString()
        {
            switch (_Name)
            {
                case "studio":
                    return "studio \"" + _File + "\"";
                case "blank":
                    return "blank";
                default:
                    return String.Format("\"{0}\" \"{1}\"", _Name, _File);
            }
        }
    }
}
