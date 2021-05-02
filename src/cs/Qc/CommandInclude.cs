namespace JbnLib.Qc
{
    public class CommandInclude
    {
        public const string Command = "$include";

        private string _File;
        public string File
        {
            get { return _File; }
            set { _File = value; }
        }

        public CommandInclude()
        {
        }
        public CommandInclude(string file)
        {
            _File = file;
        }

        public new string ToString()
        {
            return Command + " \"" + _File + "\"";
        }
    }
}
