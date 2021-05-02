using System;

namespace JbnLib.Qc
{
    public class CommandCdTexture
    {
        public const string Command = "$cdtexture";

        private string _Directory;
        public string Directory
        {
            get { return _Directory; }
            set { _Directory = value; }
        }

        public CommandCdTexture()
        {
        }
        public CommandCdTexture(string directory)
        {
            _Directory = directory;
        }

        public new string ToString()
        {
            return String.Format("{0} \"{1}\"", Command, _Directory);
        }
    }
}
