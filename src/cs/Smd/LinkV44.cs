using System;

namespace JbnLib.Smd
{
    public class LinkV44
    {
        private sbyte _Bone;
        public sbyte Bone
        {
            get { return _Bone; }
            set { _Bone = value; }
        }

        private float _Weight;
        public float Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }

        public LinkV44()
        {
            _Bone = -1;
            _Weight = 0.0000F;
        }
        public LinkV44(sbyte bone, float weight)
        {
            _Bone = bone;
            _Weight = weight;
        }

        public new string ToString()
        {
            return _Bone.ToString() + " " + String.Format("{0:f6}", _Weight);
        }
    }
}
