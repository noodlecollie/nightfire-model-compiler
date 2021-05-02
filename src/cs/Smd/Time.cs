using JbnLib.Shared;

namespace JbnLib.Smd
{
    public class Time
    {
        private int _Node;
        public int Node
        {
            get { return _Node; }
            set { _Node = value; }
        }

        private Point3D _Position;
        public Point3D Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        private Point3D _Rotation;
        public Point3D Rotation
        {
            get { return _Rotation; }
            set { _Rotation = value; }
        }

        public Time()
        {
            _Node = 0;
            _Position = new Point3D();
            _Rotation = new Point3D();
        }
        public Time(int node, Point3D pos, Point3D rot)
        {
            _Node = node;
            _Position = pos;
            _Rotation = rot;
        }

        public new string ToString()
        {
            return _Node.ToString().PadLeft(3, ' ') + " " + _Position.ToString() + " " + _Rotation.ToString();
        }
    }
}
