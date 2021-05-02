using System.Collections.Generic;

namespace JbnLib.Qc
{
    public class CommandLodTemp
    {
        public const string Command = "$lodtemp";

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        private List<int> _Distances = new List<int>();
        public List<int> Distances
        {
            get { return _Distances; }
            set { _Distances = value; }
        }

        public CommandLodTemp()
        {
        }
        public CommandLodTemp(int index, params int[] distances)
        {
            _Index = index;
            foreach (int distance in distances)
                _Distances.Add(distance);
        }
        public CommandLodTemp(int index, List<int> distances)
        {
            _Index = index;
            _Distances = distances;
        }

        public new string ToString()
        {
            string output = Command + " " + _Index + " " + _Distances.Count;
            for (int i = 0; i < _Distances.Count; i++)
                output += " " + _Distances[i];
            return output;
        }
    }
}
