using System.Collections.Generic;

namespace JbnLib.Smd
{
    public class Triangle<T>
    {
        private string _Texture;
        public string Texture
        {
            get { return _Texture; }
            set { _Texture = value; }
        }

        private List<T> _Vertices = new List<T>();
        public List<T> Vertices
        {
            get { return _Vertices; }
            set { _Vertices = value; }
        }

        public Triangle()
        {
        }
        public Triangle(string texture, List<T> vertices)
        {
            _Texture = texture;
            _Vertices = vertices;
        }


        public void Flip()
        {
            T TempVert = _Vertices[0];
            _Vertices[0] = Vertices[2];
            _Vertices[2] = TempVert;
        }

    }
}
