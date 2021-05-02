namespace JbnLib.Qc
{
    public class OptionLoop
    {
        public const string Option = "loop";

        private bool _Value = false;
        public bool Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public OptionLoop()
        {
        }
        public OptionLoop(bool value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            if (_Value)
                return "loop";
            else
                return "";
        }
    }
}
