using JbnLib.Shared;
using System;
using System.Collections.Generic;
using System.IO;

namespace JbnLib.Smd
{
    public class SmdFile
    {
        internal List<Node> _NodeCollection;
        public List<Node> NodeCollection
        {
            get { return _NodeCollection; }
            set { _NodeCollection = value; }
        }

        internal List<Frame> _TimeCollection;
        public List<Frame> TimeCollection
        {
            get { return _TimeCollection; }
            set { _TimeCollection = value; }
        }

        internal bool _Reference;
        internal FileTokenizer _Tokenizer;

        public const string STRING_VERSION_ONE = "version 1";
        public const string STRING_SKELETON = "skeleton";
        public const string STRING_TRIANGLES = "triangles";
        public const string STRING_NODES = "nodes";
        public const string STRING_TIME = "time";
        public const string STRING_END = "end";

        public bool HasNode(sbyte node)
        {
            foreach (Node item in _NodeCollection)
            {
                if (item.Index == node)
                    return true;
            }
            return false;
        }
        public void SortNodes()
        {
            Node[] nodes = new Node[_NodeCollection.Count];
            _NodeCollection.CopyTo(nodes);
            _NodeCollection.Clear();

            Array.Sort<Node>(nodes, new NodeComparer());

            foreach (Node node in nodes)
                _NodeCollection.Add(node);
        }

        /// <summary>
        /// Rotates this instance by x degrees.
        /// </summary>
        /// <param name="degrees">Amount to rotate relative to current rotation.</param>
        /// <returns>Number of rotations performed.</returns>
        public int Rotate(double degrees)
        {
            int output = 0;
            float use = Convert.ToSingle(StaticMethods.DegToRad(degrees));
            for (int t = 0; t < _TimeCollection.Count; t++)
            {
                for (int b = 0; b < _TimeCollection[t].Bones.Count; b++)
                {
                    if (_NodeCollection[_TimeCollection[t].Bones[b].Node].Parent == -1)
                    {
                        // Z Rotation
                        _TimeCollection[t].Bones[b].Rotation.Z += use;

                        // Get start quadrant.
                        StaticMethods.Quadrant quad = StaticMethods.GetQuadrant(_TimeCollection[t].Bones[b].Position.X, _TimeCollection[t].Bones[b].Position.Y);

                        // X, Y Position
                        double hypotenuse = Math.Sqrt(Math.Pow(_TimeCollection[t].Bones[b].Position.X, 2) + Math.Pow(_TimeCollection[t].Bones[b].Position.Y, 2));
                        double rad = Math.Atan(_TimeCollection[t].Bones[b].Position.Y / _TimeCollection[t].Bones[b].Position.X);
                        rad += use;
                        _TimeCollection[t].Bones[b].Position.X = Convert.ToSingle(Math.Cos(rad) * hypotenuse);
                        _TimeCollection[t].Bones[b].Position.Y = Convert.ToSingle(Math.Sin(rad) * hypotenuse);

                        // Correct for the -x quadrants
                        if ((quad == StaticMethods.Quadrant.II) || (quad == StaticMethods.Quadrant.III))
                        {
                            _TimeCollection[t].Bones[b].Position.X *= -1;
                            _TimeCollection[t].Bones[b].Position.Y *= -1;
                        }

                        output++;
                    }
                }
            }
            return output;
        }
        
        /// <summary>
        /// Checks if the given file contains a triangles section.
        /// </summary>
        /// <param name="file">The SMD file to scan.</param>
        /// <returns>True if it does, false if it doesn't.</returns>
        public static bool HasTriangles(string file)
        {
            StreamReader sr = new StreamReader(file);
            while (sr.Peek() != -1)
            {
                if (sr.ReadLine().Trim().ToLower() == STRING_TRIANGLES)
                {
                    sr.Close();
                    return true;
                }
            }
            sr.Close();
            return false;
        }

        /// <summary>
        /// Gets a list of nodes from a SMD file.  The name of the nodes are inserted into the index it represents.
        /// </summary>
        /// <param name="file">An SMD file.</param>
        /// <returns>An indexed string array of node names.</returns>
        public static string[] GetNodes(string file)
        {
            List<Node> nodes = new List<Node>();
            StreamReader sr = new StreamReader(file);
            while (sr.Peek() != -1)
            {
                string read = sr.ReadLine();
                if (read.Trim().ToLower() == STRING_NODES)
                {
                    read = sr.ReadLine();
                    while (read.Trim().ToLower() != STRING_END)
                    {
                        nodes.Add(new Node(read));
                        read = sr.ReadLine();
                    }
                    break;
                }
            }
            sr.Close();

            // Figure out how big of an array is needed.
            sbyte highest = -1;
            foreach (Node node in nodes)
            {
                if (node.Index >= highest)
                    highest = node.Index;
            }

            // Copy to array.
            string[] output = new string[highest + 1];
            foreach (Node node in nodes)
                output[node.Index] = node.Name;

            return output;
        }
        /// <summary>
        /// Reads the nodes section of an SMD file and returns the index of the node given.
        /// </summary>
        /// <param name="file">The SMD file to read.</param>
        /// <param name="node">The name of the node to get the index of.</param>
        /// <returns>The index of the node found or -1 if it was not found.</returns>
        public static sbyte GetNode(string file, string node)
        {
            string[] nodes = GetNodes(file);
            for (sbyte i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] == node)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// Reads the nodes section of an SMD file and returns the name of the node at a specific index.
        /// </summary>
        /// <param name="file">The SMD file to read.</param>
        /// <param name="node">The index of the node to return.</param>
        /// <returns>The index of the node found or "ERROR" if it was not found.</returns>
        public static string GetNode(string file, sbyte node)
        {
            string[] nodes = GetNodes(file);
            try { return nodes[node]; }
            catch { return "ERROR"; }
        }

        public static bool IsReference(string file)
        {
            bool output = false;
            StreamReader sr = new StreamReader(file);
            while (sr.Peek() != -1)
            {
                if (sr.ReadLine().Trim().ToLower() == STRING_TRIANGLES)
                {
                    output = true;
                    break;
                }
            }
            sr.Close();
            return output;
        }
    }
}
