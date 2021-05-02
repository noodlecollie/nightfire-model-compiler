using System;
using JbnLib.Shared;

namespace JbnLib.Qc
{
    public class CommandBBox
    {
        public const string Command = "$bbox";

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

        public CommandBBox()
        {
        }
        public CommandBBox(Point3D min, Point3D max)
        {
            _Min = min;
            _Max = max;
        }

        public new string ToString()
        {
            return String.Format("{0} {1} {2}", Command, _Min.ToString(), _Max.ToString());
        }
    }
}
