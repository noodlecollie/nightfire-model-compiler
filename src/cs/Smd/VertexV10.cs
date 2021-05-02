using JbnLib.Mdl;
using JbnLib.Shared;

namespace JbnLib.Smd
{
    public class VertexV10
    {
        private sbyte _Bone;
        public sbyte Bone
        {
            get { return _Bone; }
            set { _Bone = value; }
        }

        private Point3D _Position;
        public Point3D Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        private Point3D _Normal;
        public Point3D Normal
        {
            get { return _Normal; }
            set { _Normal = value; }
        }

        private TextureCoordinate _TextureCoordinate;
        public TextureCoordinate TextureCoordinate
        {
            get { return _TextureCoordinate; }
            set { _TextureCoordinate = value; }
        }

        public VertexV10()
        {
            _Bone = 0;
            _Position = new Point3D();
            _Normal = new Point3D();
            _TextureCoordinate = new TextureCoordinate();
        }
        public VertexV10(sbyte bone, Point3D pos, Point3D norm, TextureCoordinate texcoord)
        {
            _Bone = bone;
            _Position = pos;
            _Normal = norm;
            _TextureCoordinate = texcoord;
        }

        public VertexV10 Flip()
        {
            _Normal = -_Normal;
            return this;
        }

        public new string ToString()
        {
            return _Bone.ToString().PadLeft(3) + " " +
                _Position.ToString() + " " +
                _Normal.ToString() + " " +
                _TextureCoordinate.ToString();
        }
    }
}
