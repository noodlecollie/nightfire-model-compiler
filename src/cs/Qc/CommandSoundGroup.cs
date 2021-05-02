using System.Collections.Generic;

namespace JbnLib.Qc
{
    public class CommandSoundGroup
    {
        public const string Command = "$soundgroup";

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private List<string> _Sounds = new List<string>();
        public List<string> Sounds
        {
            get { return _Sounds; }
            set { _Sounds = value; }
        }

        public new string ToString()
        {
            string output = Command + " \"" + _Name + "\"\r\n{\r\n";
            foreach (string sound in _Sounds)
                output += "\t\"" + sound + "\"\r\n";
            return output += "}";
        }
    }
}
