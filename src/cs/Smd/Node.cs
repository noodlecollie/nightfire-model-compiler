using System;

namespace JbnLib.Smd
{
    public class Node
    {
        private sbyte _Index;
        public sbyte Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private sbyte _Parent;
        public sbyte Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        public Node()
        {
        }
        public Node(string line)
        {
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _Index = Convert.ToSByte(parts[0]);
            _Name = parts[1];
            for (int i = 2; i < parts.Length - 1; i++)
                _Name += " " + parts[i];
            _Name = _Name.Trim('"');
            _Parent = Convert.ToSByte(parts[parts.Length - 1]);
        }
        public Node(sbyte index, string name, sbyte parent)
        {
            _Index = index;
            _Name = name;
            _Parent = parent;
        }

        public new string ToString()
        {
            return _Index.ToString().PadLeft(3) + " \"" + _Name + "\" " + _Parent.ToString().PadLeft(3);
        }
    }
}
