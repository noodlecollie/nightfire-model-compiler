using System;

namespace JbnLib.Qc
{
    public class CommandMirrorBone
    {
        public const string Command = "$mirrorbone";

        private string _Bone;
        public string Bone
        {
            get { return _Bone; }
            set { _Bone = value; }
        }

        public CommandMirrorBone()
        {

        }
        public CommandMirrorBone(string bone)
        {
            _Bone = bone;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Command, _Bone);
        }
    }
}
