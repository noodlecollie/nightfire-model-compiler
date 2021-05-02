using System;
using JbnLib.Shared;

namespace JbnLib.Qc
{
    public class CommandHBox
    {
        public const string Command = "$hbox";

        private int _Group;
        public int Group
        {
            get { return _Group; }
            set { _Group = value; }
        }

        private string _Bone;
        public string Bone
        {
            get { return _Bone; }
            set { _Bone = value; }
        }

        private Point3D _Min = new Point3D();
        public Point3D Min
        {
            get { return _Min; }
            set { _Min = value; }
        }

        private Point3D _Max = new Point3D();
        public Point3D Max
        {
            get { return _Max; }
            set { _Max = value; }
        }

        public CommandHBox()
        {
        }
        public CommandHBox(int group, string bone, Point3D min, Point3D max)
        {
            _Group = group;
            _Bone = bone;
            _Min = min;
            _Max = max;
        }

        public new string ToString()
        {
            return String.Format("{0} {1} \"{2}\" {3} {4}", Command, _Group, _Bone, _Min.ToString(), Max.ToString());
        }
    }
}
