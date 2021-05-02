using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using JbnLib.Lang;
using JbnLib.Shared;

namespace JbnLib.Smd
{
    public class SmdFileV11 : SmdFile
    {
        private List<Triangle<VertexV11>> _TriangleCollection;
        public List<Triangle<VertexV11>> TriangleCollection
        {
            get { return _TriangleCollection; }
            set
            {
                if (_Reference)
                    _TriangleCollection = value;
                else
                    Messages.ThrowException("Smd.SmdFileV11.TriangleCollection", Messages.ANIMATION_TRIS);
            }
        }

        public SmdFileV11(SmdType type)
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
            _TriangleCollection = new List<Triangle<VertexV11>>();
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
                            Messages.ThrowException("Smd.SmdFileV11.Read(string)", Messages.VERSION_ONE);
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

                                Triangle<VertexV11> triangle = new Triangle<VertexV11>();
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
                                    VertexV11 vertex = new VertexV11();
                                    if (_Tokenizer.Token != "$blended")
                                        Messages.ThrowException("Smd.SmdFileV11.Read(string)", Messages.INVALID_TRIS);

                                    // Blends
                                    _Tokenizer.GetToken();
                                    vertex.Blends = new BlendV11[Convert.ToInt32(_Tokenizer.Token)];

                                    for (byte j = 0; j < vertex.Blends.Length; j++)
                                    {
                                        _Tokenizer.GetToken();
                                        sbyte bone = Convert.ToSByte(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        vertex.Blends[j] = new BlendV11(bone, Convert.ToSingle(_Tokenizer.Token));
                                    }

                                    // Position
                                    _Tokenizer.GetToken();
                                    vertex.Position.X = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    vertex.Position.Y = Convert.ToSingle(_Tokenizer.Token);
                                    _Tokenizer.GetToken();
                                    vertex.Position.Z = Convert.ToSingle(_Tokenizer.Token);

                                    // Normals
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
                foreach (Triangle<VertexV11> tri in _TriangleCollection)
                {
                    sw.WriteLine(tri.Texture + ".png");
                    foreach (VertexV11 vert in tri.Vertices)
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
            foreach (Triangle<VertexV11> triangle in _TriangleCollection)
            {
                Triangle<VertexV10> newtriangle = new Triangle<VertexV10>();
                newtriangle.Texture = triangle.Texture;
                foreach (VertexV11 vertex in triangle.Vertices)
                    newtriangle.Vertices.Add(new VertexV10(vertex.Blends[0].Bone, vertex.Position, vertex.Normal, vertex.TextureCoordinate));
                output.TriangleCollection.Add(newtriangle);
            }

            return output;
        }
        public SmdFileV44 ToV44()
        {
            SmdFileV44 output;
            if (_Reference)
                output = new SmdFileV44(SmdType.Reference);
            else
                output = new SmdFileV44(SmdType.Animation);

            output.NodeCollection = _NodeCollection;
            output.TimeCollection = _TimeCollection;
            foreach (Triangle<VertexV11> triangle in _TriangleCollection)
            {
                Triangle<VertexV44> newtriangle = new Triangle<VertexV44>();
                newtriangle.Texture = triangle.Texture;
                foreach (VertexV11 vertex in triangle.Vertices)
                {
                    List<LinkV44> links = new List<LinkV44>();
                    for (int i = 0; i < vertex.Blends.Length; i++)
                    {
                        if (vertex.Blends[i].Bone != -1)
                            links.Add(new LinkV44(vertex.Blends[i].Bone, vertex.Blends[i].Scale));
                    }
                    newtriangle.Vertices.Add(new VertexV44(vertex.Blends[0].Bone, vertex.Position, vertex.Normal, vertex.TextureCoordinate, links));
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
