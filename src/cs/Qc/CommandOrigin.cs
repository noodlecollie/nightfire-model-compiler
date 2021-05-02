using System;
using JbnLib.Shared;

namespace JbnLib.Qc
{
    public class CommandOrigin
    {
        public const string Command = "$origin";

        private Point3D _Value = new Point3D();
        public Point3D Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public CommandOrigin()
        {
        }
        public CommandOrigin(Point3D value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Command, _Value.ToString());
        }
    }
}
