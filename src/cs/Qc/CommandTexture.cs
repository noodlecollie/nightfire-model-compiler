using System;

namespace JbnLib.Qc
{
    public class CommandTexture
    {
        public const string Command = "$texture";

        private string _Texture;
        public string Texture
        {
            get { return _Texture; }
            set { _Texture = value; }
        }

        private string _Material;
        public string Material
        {
            get { return _Material; }
            set { _Material = value; }
        }

        public CommandTexture()
        {
        }
        public CommandTexture(string texture, string material)
        {
            _Texture = texture;
            _Material = material;
        }

        public new string ToString()
        {
            return String.Format("{0} \"{1}\" \"{2}\"", Command, _Texture, _Material);
        }
    }
}
