using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using JbnLib.Lang;
using JbnLib.Shared;

namespace JbnLib.Smd
{
    public class SmdFileV44 : SmdFile
    {
        private List<Triangle<VertexV44>> _TriangleCollection;
        public List<Triangle<VertexV44>> TriangleCollection
        {
            get { return _TriangleCollection; }
            set
            {
                if (_Reference)
                    _TriangleCollection = value;
                else
                    Messages.ThrowException("Smd.SmdFileV44.TriangleCollection", Messages.ANIMATION_TRIS);
            }
        }

        public SmdFileV44(SmdType type)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            switch (type)
            {
                case SmdType.Animation:
                    _Reference = false;
                    break;
                default:
                    _Reference = true;
                    break;
            }

            Clear();
        }

        public void Clear()
        {
            _NodeCollection = new List<Node>();
            _TimeCollection = new List<Frame>();
            _TriangleCollection = new List<Triangle<VertexV44>>();
        }
        public void Read(string file)
        {
            Clear();

            _Tokenizer = new FileTokenizer(file);
            while (!_Tokenizer.GetToken())
            {
                switch (_Tokenizer.Token)
                {
                    #region " Version "
                    case "version":
                        _Tokenizer.GetToken();
                        if (Convert.ToInt32(_Tokenizer.Token) != 1)
                            Messages.ThrowException("Smd.SmdFileV44.Read(string)", Messages.VERSION_ONE);
                        break;
                    #endregion

                    #region " Nodes "
                    case STRING_NODES:
                        while (!_Tokenizer.GetToken())
                        {
                            if (_Tokenizer.Token == STRING_END)
                                break;

                            Node node = new Node();
                            node.Index = Convert.ToSByte(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            node.Name = _Tokenizer.Token;
                            _Tokenizer.GetToken();
                            node.Parent = Convert.ToSByte(_Tokenizer.Token);
                            _NodeCollection.Add(node);
                        }
                        break;
                    #endregion

                    #region " Skeleton "
                    case STRING_SKELETON:
                        _Tokenizer.GetToken();
                        while (_Tokenizer.Token == STRING_TIME)
                        {
                            _Tokenizer.GetToken();
                            Frame frame = new Frame();
                            frame.FrameValue = Convert.ToInt32(_Tokenizer.Token);
                            try
                            {
                                while (!_Tokenizer.GetToken())
                                {
                                    Time time = new Time();
                                    time.Node = Convert.ToInt32(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    time.Position.X = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    time.Position.Y = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    time.Position.Z = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    time.Rotation.X = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    time.Rotation.Y = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    time.Rotation.Z = Convert.ToSingle(_Tokenizer.Token);
                                    frame.Bones.Add(time);
                                }
                            }
                            catch
                            {
                                _TimeCollection.Add(frame);
                            }
                        }
                        break;
                    #endregion

                    #region " Triangles "
                    case STRING_TRIANGLES:
                        if (_Reference)
                        {
                            while (!_Tokenizer.GetToken())
                            {
                                if (_Tokenizer.Token == STRING_END)
                                    break;

                                Triangle<VertexV44> triangle = new Triangle<VertexV44>();
                                triangle.Texture = _Tokenizer.Token;

                                // See if we got a space in the texture name.
                                long line = _Tokenizer.Line;
                                _Tokenizer.GetToken();
                                while (_Tokenizer.Line == line)
                                {
                                    triangle.Texture += " " + _Tokenizer.Token;
                                    _Tokenizer.GetToken();
                                }

                                triangle.Texture = StaticMethods.StripExtension(triangle.Texture);

                                for (int i = 0; i < 3; i++)
                                {
                                    VertexV44 vertex = new VertexV44();

                                    // Bone
                                    vertex.Bone = Convert.ToSByte(_Tokenizer.Token);

                                    // Position
                                    _Tokenizer.GetToken();
                                    vertex.Position.X = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    vertex.Position.Y = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    vertex.Position.Z = Convert.ToSingle(_Tokenizer.Token);

                                    // Normal
                                    _Tokenizer.GetToken();
                                    vertex.Normal.X = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    vertex.Normal.Y = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    vertex.Normal.Z = Convert.ToSingle(_Tokenizer.Token);

                                    // Texture Coordinates
                                    _Tokenizer.GetToken();
                                    vertex.TextureCoordinate.U = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    vertex.TextureCoordinate.V = Convert.ToSingle(_Tokenizer.Token);

                                    // Links
                                    _Tokenizer.GetToken();
                                    int links = Convert.ToInt32(_Tokenizer.Token);
                                    for (int l = 0; l < links; l++)
                                    {
                                        LinkV44 link = new LinkV44();
                                        _Tokenizer.GetToken();
                                        link.Bone = Convert.ToSByte(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        link.Weight = Convert.ToSingle(_Tokenizer.Token);
                                        vertex.LinkCollection.Add(link);
                                    }

                                    // Get token for next vertex; don't on last
                                    if (i != 2)
                                        _Tokenizer.GetToken();

                                    triangle.Vertices.Add(vertex);
                                }
                                _TriangleCollection.Add(triangle);
                            }
                        }
                        break;
                    #endregion
                }
            }
        }
        public void Write(string file)
        {
            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine(STRING_VERSION_ONE);
            sw.WriteLine(STRING_NODES);
            foreach (Node node in _NodeCollection)
                sw.WriteLine(node.ToString());
            sw.WriteLine(STRING_END);
            sw.Flush();

            sw.WriteLine(STRING_SKELETON);
            foreach (Frame time in _TimeCollection)
                sw.WriteLine(time.ToString());
            sw.WriteLine(STRING_END);
            sw.Flush();

            if (_Reference)
            {
                sw.WriteLine(STRING_TRIANGLES);
                foreach (Triangle<VertexV44> tri in _TriangleCollection)
                {
                    sw.WriteLine(tri.Texture + ".bmp");
                    foreach (VertexV44 vert in tri.Vertices)
                        sw.WriteLine(vert.ToString());
                    sw.Flush();
                }
                sw.WriteLine(STRING_END);
                sw.Flush();
            }

            sw.Close();
        }

        public SmdFileV10 ToV10()
        {
            SmdFileV10 output;
            if (_Reference)
                output = new SmdFileV10(SmdType.Reference);
            else
                output = new SmdFileV10(SmdType.Animation);

            output.NodeCollection = _NodeCollection;
            output.TimeCollection = _TimeCollection;
            foreach (Triangle<VertexV44> triangle in _TriangleCollection)
            {
                Triangle<VertexV10> newtriangle = new Triangle<VertexV10>();
                newtriangle.Texture = triangle.Texture;
                foreach (VertexV44 vertex in triangle.Vertices)
                    newtriangle.Vertices.Add(new VertexV10(vertex.Bone, vertex.Position, vertex.Normal, vertex.TextureCoordinate));
                output.TriangleCollection.Add(newtriangle);
            }

            return output;
        }
        public SmdFileV11 ToV11()
        {
            SmdFileV11 output;
            if (_Reference)
                output = new SmdFileV11(SmdType.Reference);
            else
                output = new SmdFileV11(SmdType.Animation);

            output.NodeCollection = _NodeCollection;
            output.TimeCollection = _TimeCollection;
            foreach (Triangle<VertexV44> triangle in _TriangleCollection)
            {
                Triangle<VertexV11> newtriangle = new Triangle<VertexV11>();
                newtriangle.Texture = triangle.Texture;
                foreach (VertexV44 vertex in triangle.Vertices)
                {
                    BlendV11[] blends = new BlendV11[4];
                    for (int i = 0; i < blends.Length; i++)
                    {
                        if (i > vertex.LinkCollection.Count - 1)
                            blends[i] = new BlendV11();
                        else
                            blends[i] = new BlendV11(vertex.LinkCollection[i].Bone, vertex.LinkCollection[i].Weight);
                    }
                    newtriangle.Vertices.Add(new VertexV11(blends, vertex.Position, vertex.Normal, vertex.TextureCoordinate));
                }
                output.TriangleCollection.Add(newtriangle);
            }

            return output;
        }
        public string[] GetTextures()
        {
            string[] temp = new string[_TriangleCollection.Count];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = _TriangleCollection[i].Texture;
            return StaticMethods.EliminateDuplicates(temp);
        }
    }
}
