using System;

namespace JbnLib.Smd
{
    public class BlendV11
    {
        private sbyte _Bone;
        public sbyte Bone
        {
            get { return _Bone; }
            set { _Bone = value; }
        }

        private float _Scale;
        public float Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }

        public BlendV11()
        {
            _Bone = -1;
            _Scale = 0.0000F;
        }
        public BlendV11(sbyte bone, float scale)
        {
            _Bone = bone;
            _Scale = scale;
        }

        public new string ToString()
        {
            return _Bone.ToString().PadLeft(3) + " " + String.Format("{0:f4}", _Scale).PadLeft(8);
        }
    }
}
