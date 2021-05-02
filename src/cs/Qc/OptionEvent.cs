using System;

namespace JbnLib.Qc
{
    public class OptionEvent
    {
        public const string Option = "event";

        private int _EventValue;
        public int EventValue
        {
            get { return _EventValue; }
            set { _EventValue = value; }
        }

        private int _Frame;
        public int Frame
        {
            get { return _Frame; }
            set { _Frame = value; }
        }

        private string _Options;
        public string Options
        {
            get { return _Options; }
            set { _Options = value; }
        }

        public OptionEvent()
        {
        }
        public OptionEvent(int value, int frame, string options)
        {
            _EventValue = value;
            _Frame = frame;
            _Options = options;
        }

        public new string ToString()
        {
            return String.Format("{0} {1} {2} \"{3}\"", Option, _EventValue, _Frame, _Options);
        }
    }
}
