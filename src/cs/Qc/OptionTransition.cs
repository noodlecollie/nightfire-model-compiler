using System;

namespace JbnLib.Qc
{
    public class OptionTransition
    {
        public const string Option = "transition";

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

        public OptionTransition()
        {
        }
        public OptionTransition(int entry, int exit)
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
