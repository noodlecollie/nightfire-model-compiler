using System.Collections.Generic;

namespace JbnLib.Qc
{
    public class CommandTextureGroup
    {
        public const string Command = "$texturegroup";

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private List<List<string>> _SkinCollection = new List<List<string>>();
        public List<List<string>> SkinCollection
        {
            get { return _SkinCollection; }
            set { _SkinCollection = value; }
        }

        public new string ToString()
        {
            string output = Command + " " + _Name + "\r\n{\r\n";
            foreach (List<string> family in _SkinCollection)
            {
                output += "\t{ ";
                foreach (string reference in family)
                    output += reference + ", ";
                output = output.TrimEnd(' ', ',') + " }\r\n";
            }
            return output + "}";
        }
    }
}
