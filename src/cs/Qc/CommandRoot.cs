using System;

namespace JbnLib.Qc
{
    public class CommandRoot
    {
        public const string Command = "$root";

        private string _Bone;
        public string Bone
        {
            get { return _Bone; }
            set { _Bone = value; }
        }

        public CommandRoot()
        {
        }
        public CommandRoot(string bone)
        {
            _Bone = bone;
        }

        public new string ToString()
        {
            return String.Format("{0} \"{1}\"", Command, _Bone);
        }
    }
}
