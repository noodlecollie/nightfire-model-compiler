using System;

namespace JbnLib.Qc
{
    public class CommandPivot
    {
        public const string Command = "$pivot";

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        private string _Bone;
        public string Bone
        {
            get { return _Bone; }
            set { _Bone = value; }
        }

        public new string ToString()
        {
            return String.Format("{0} {1} \"{2}\"", Command, _Index, _Bone);
        }
    }
}
