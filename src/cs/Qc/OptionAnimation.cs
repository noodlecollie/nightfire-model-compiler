using System;

namespace JbnLib.Qc
{
    public class OptionAnimation
    {
        public const string Option = "animation";

        private string _File;
        public string File
        {
            get { return _File; }
            set { _File = value; }
        }

        public OptionAnimation()
        {
        }
        public OptionAnimation(string file)
        {
            _File = file;
        }

        public new string ToString()
        {
            return String.Format("{0} \"{1}\"", Option, _File);
        }
    }
}
