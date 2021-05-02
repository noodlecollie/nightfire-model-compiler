using System;

namespace JbnLib.Qc
{
    public class OptionNodeV10
    {
        public const string Option = "node";

        private sbyte _EntryBone;
        public sbyte EntryBone
        {
            get { return _EntryBone; }
            set { _EntryBone = value; }
        }

        public OptionNodeV10()
        {
        }
        public OptionNodeV10(sbyte entrybone)
        {
            _EntryBone = entrybone;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Option, _EntryBone);
        }
    }
}
