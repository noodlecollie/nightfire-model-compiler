using System.Collections.Generic;

namespace JbnLib.Smd
{
    public class NodeComparer : IComparer<Node>
    {
        public int Compare(Node a, Node b)
        {
            if (a.Index > b.Index)
                return 1;
            else if (a.Index < b.Index)
                return -1;
            else
                return 0;
        }
    }
}
