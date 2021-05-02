using System;
using JbnLib.Shared;

namespace JbnLib.Qc
{
    public class CommandAttachment
    {
        public const string Command = "$attachment";

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Bone;
        public string Bone
        {
            get { return _Bone; }
            set { _Bone = value; }
        }

        private Point3D _Position = new Point3D();
        public Point3D Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        public CommandAttachment()
        {
        }
        public CommandAttachment(string name, string bone, Point3D position)
        {
            _Name = name;
            _Bone = bone;
            _Position = position;
        }

        public new string ToString()
        {
            return String.Format("{0} \"{1}\" \"{2}\" {3}", Command, _Name, _Bone, _Position.ToString());
        }
    }
}
