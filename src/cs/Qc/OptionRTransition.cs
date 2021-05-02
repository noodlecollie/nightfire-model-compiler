using System;

namespace JbnLib.Qc
{
    public class OptionRTransition
    {
        public const string Option = "rtransition";

        private int _EntryBone;
        public int EntryBone
        {
            get { return _EntryBone; }
            set { _EntryBone = value; }
        }

        private int _ExitBone;
        public int ExitBone
        {
            get { return _ExitBone; }
            set { _ExitBone = value; }
        }

        public OptionRTransition()
        {
        }
        public OptionRTransition(int entry, int exit)
        {
            _EntryBone = entry;
            _ExitBone = exit;
        }

        public new string ToString()
        {
            return String.Format("{0} {1} {2}", Option, _EntryBone, _ExitBone);
        }
    }
}
