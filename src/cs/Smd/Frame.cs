using System.Collections.Generic;

namespace JbnLib.Smd
{
    public class Frame
    {
        private int _FrameValue;
        public int FrameValue
        {
            get { return _FrameValue; }
            set { _FrameValue = value; }
        }

        private List<Time> _Bones;
        public List<Time> Bones
        {
            get { return _Bones; }
            set { _Bones = value; }
        }

        public Frame()
        {
            _Bones = new List<Time>();
        }
        public Frame(int framevalue, List<Time> bones)
        {
            _FrameValue = framevalue;
            _Bones = bones;
        }

        public new string ToString()
        {
            string output = "time " + _FrameValue;
            foreach (Time bone in _Bones)
                output += "\r\n" + bone.ToString();
            return output;
        }
    }
}
