namespace JbnLib.Qc
{
    public class CommandLodFlags
    {
        public const string Command = "$lodflags";
        
        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public CommandLodFlags()
        {
            _Value = 0;
        }
        public CommandLodFlags(int value)
        {
            _Value = value;
        }

        public new string ToString()
        {
            return Command + " " + _Value;
        }
    }
}
