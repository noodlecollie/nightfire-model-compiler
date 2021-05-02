namespace JbnLib.Qc
{
    public class CommandAnimation
    {
        public const string Command = "$animation";

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _File;
        public string File
        {
            get { return _File; }
            set
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(value);
                if (fi.Extension.Length == 0)
                    _File = value + ".smd";
                else
                    _File = value;
            }
        }

        private OptionFps _Fps;
        public OptionFps Fps
        {
            get { return _Fps; }
            set { _Fps = value; }
        }

        private OptionLoop _Loop;
        public OptionLoop Loop
        {
            get { return _Loop; }
            set { _Loop = value; }
        }

        public new string ToString()
        {
            string output = Command + " \"" + _Name + "\" \"" + _File + "\"";
            if (_Fps != null)
                output += " " + _Fps.ToString();
            if (_Loop != null)
                output += " " + _Loop.ToString();
            return output;
        }
    }
}
