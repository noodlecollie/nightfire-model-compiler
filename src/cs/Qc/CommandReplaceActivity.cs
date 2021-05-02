using System;

namespace JbnLib.Qc
{
    public class CommandReplaceActivity
    {
        public const string Command = "$replaceactivity";

        private string _SequenceName;
        public string SequenceName
        {
            get { return _SequenceName; }
            set { _SequenceName = value; }
        }

        private string _Activity;
        public string Activity
        {
            get { return _Activity; }
            set { _Activity = value; }
        }

        public CommandReplaceActivity()
        {
        }
        public CommandReplaceActivity(string sequencename, string activity)
        {
            _SequenceName = sequencename;
            _Activity = activity;
        }

        public new string ToString()
        {
            return String.Format("{0} \"{1}\" {2}", Command, _SequenceName, _Activity);
        }
    }
}
