using System.Collections.Generic;
using JbnLib.Mdl;
using JbnLib.Shared;

namespace JbnLib.Smd
{
    public class VertexV44
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

        private List<LinkV44> _LinkCollection;
        public List<LinkV44> LinkCollection
        {
            get { return _LinkCollection; }
            set { _LinkCollection = value; }
        }

        public VertexV44()
        {
            _Bone = 0;
            _Position = new Point3D();
            _Normal = new Point3D();
            _TextureCoordinate = new TextureCoordinate();
            _LinkCollection = new List<LinkV44>();
        }
        public VertexV44(sbyte bone, Point3D pos, Point3D norm, TextureCoordinate texcoord, List<LinkV44> links)
        {
            _Bone = bone;
            _Position = pos;
            _Normal = norm;
            _TextureCoordinate = texcoord;
            _LinkCollection = links;
        }

        public VertexV44 Flip()
        {
            _Normal = -_Normal;
            return this;
        }

        public new string ToString()
        {
            string temp = _Bone.ToString().PadLeft(3) + " " +
                _Position.ToString() + " " +
                _Normal.ToString() + " " +
                _TextureCoordinate.ToString() + " " + _LinkCollection.Count.ToString();
            foreach (LinkV44 link in _LinkCollection)
                temp += " " + link.ToString();
            return temp;
        }
    }
}
