using JbnLib.Mdl;
using JbnLib.Shared;

namespace JbnLib.Smd
{
    public class VertexV11
    {
        private BlendV11[] _Blends;
        public BlendV11[] Blends
        {
            get { return _Blends; }
            set { _Blends = value; }
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

        public VertexV11()
        {
            _Blends = new BlendV11[] { new BlendV11(), new BlendV11(), new BlendV11(), new BlendV11() }; 
            _Position = new Point3D();
            _Normal = new Point3D();
            _TextureCoordinate = new TextureCoordinate();
        }
        public VertexV11(BlendV11[] blends, Point3D pos, Point3D norm, TextureCoordinate texcoord)
        {
            _Blends = blends;
            _Position = pos;
            _Normal = norm;
            _TextureCoordinate = texcoord;
        }

        public VertexV11 Flip()
        {
            _Normal = -_Normal;
            return this;
        }

        public new string ToString()
        {
            string output = "$blended " + _Blends.Length.ToString().PadLeft(3);
            for (byte i = 0; i < _Blends.Length; i++)
                output += " " + _Blends[i].ToString();
            return output + " " + _Position.ToString() + " " + _Normal.ToString() + " " + _TextureCoordinate.ToString();
        }
    }
}
