using System;

namespace JbnLib.Qc
{
    public class CommandQceVersion
    {
        public const string Command = "$qceversion";

        public QceGame Game;
        public int QcVersion;
        public int QceVersionMajor;
        public int QceVersionMinor;

        public CommandQceVersion(string data)
        {
            string[] parts = data.Split('.');
            Game = (QceGame)Convert.ToInt32(parts[0]);
            QcVersion = Convert.ToInt32(parts[1]);
            QceVersionMajor = Convert.ToInt32(parts[2]);
            QceVersionMinor = Convert.ToInt32(parts[3]);
        }
        public CommandQceVersion(QceGame game, int version, int major, int minor)
        {
            Game = game;
            QcVersion = version;
            QceVersionMajor = major;
            QceVersionMinor = minor;
        }

        public new string ToString()
        {
            return Command + " " + (int)Game + "." + QcVersion + "." + QceVersionMajor + "." + QceVersionMinor;
        }
    }

    public enum QceGame : int 
    {
        JamesBond007Nightfire = 1,
        HalfLife = 2,
        HalfLife2 = 3
    }
}
