using System;
using JbnLib.Mdl;

namespace JbnLib.Qc
{
    public class CommandController
    {
        public const string Command = "$controller";

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

        private MotionFlags _Type;
        public MotionFlags Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private float _Start;
        public float Start
        {
            get { return _Start; }
            set { _Start = value; }
        }

        private float _End;
        public float End
        {
            get { return _End; }
            set { _End = value; }
        }

        public CommandController()
        {
        }
        public CommandController(int index, string bone, MotionFlags type, float start, float end)
        {
            _Index = index;
            _Bone = bone;
            _Type = type;
            _Start = start;
            _End = end;
        }

        public new string ToString()
        {
            return String.Format("{0} {1} \"{2}\" {3} {4} {5}", Command, _Index, _Bone, _Type, _Start, _End);
        }
    }
}
