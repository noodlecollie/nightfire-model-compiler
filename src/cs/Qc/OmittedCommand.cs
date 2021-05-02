namespace JbnLib.Qc
{
    public class OmittedCommand
    {
        private long _Line;
        public long Line
        {
            get { return _Line; }
        }

        private string _Command;
        public string Command
        {
            get { return _Command; }
        }

        public OmittedCommand(long line, string command)
        {
            _Line = line;
            _Command = command;
        }

        public new string ToString()
        {
            return _Line + ", " + _Command;
        }
    }
}
