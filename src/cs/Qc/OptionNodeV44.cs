using System;

namespace JbnLib.Qc
{
    public class OptionNodeV44
    {
        public const string Option = "node";

        private string _EntryBone;
        public string EntryBone
        {
            get { return _EntryBone; }
            set { _EntryBone = value; }
        }

        public OptionNodeV44()
        {
        }
        public OptionNodeV44(string entrybone)
        {
            _EntryBone = entrybone;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", Option, _EntryBone);
        }
    }
}
