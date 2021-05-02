using System;
using System.Collections.Generic;
using System.Text;
using JbnLib.Mdl;
using JbnLib.Smd;
using JbnLib.Shared;

namespace JbnLib.Qc
{
    public class CommandSequenceV11
    {
        public const string Command = "$sequence";

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private OptionActivityV11 _Activity;
        public OptionActivityV11 Activity
        {
            get { return _Activity; }
            set { _Activity = value; }
        }

        private OptionAnimation _Animation;
        public OptionAnimation Animation
        {
            get { return _Animation; }
            set { _Animation = value; }
        }

        private List<OptionBlend> _BlendCollection = new List<OptionBlend>();
        public List<OptionBlend> BlendCollection
        {
            get { return _BlendCollection; }
            set { _BlendCollection = value; }
        }

        private OptionControl _Control = new OptionControl();
        public OptionControl Control
        {
            get { return _Control; }
            set { _Control = value; }
        }

        private List<OptionEvent> _EventCollection = new List<OptionEvent>();
        public List<OptionEvent> EventCollection
        {
            get { return _EventCollection; }
            set { _EventCollection = value; }
        }

        private OptionFps _Fps;
        public OptionFps Fps
        {
            get { return _Fps; }
            set { _Fps = value; }
        }

        private List<string> _FileCollection = new List<string>();
        public List<string> FileCollection
        {
            get { return _FileCollection; }
            set { _FileCollection = value; }
        }

        private OptionFrame _Frame;
        public OptionFrame Frame
        {
            get { return _Frame; }
            set { _Frame = value; }
        }

        private OptionLoop _Loop;
        public OptionLoop Loop
        {
            get { return _Loop; }
            set { _Loop = value; }
        }

        private OptionNodeV10 _Node;
        public OptionNodeV10 Node
        {
            get { return _Node; }
            set { _Node = value; }
        }

        private List<OptionPivot> _PivotCollection = new List<OptionPivot>();
        public List<OptionPivot> PivotCollection
        {
            get { return _PivotCollection; }
            set { _PivotCollection = value; }
        }

        private OptionRotate _Rotate;
        public OptionRotate Rotate
        {
            get { return _Rotate; }
            set { _Rotate = value; }
        }

        private OptionRTransition _RTransition;
        public OptionRTransition RTransition
        {
            get { return _RTransition; }
            set { _RTransition = value; }
        }

        private OptionScale _Scale;
        public OptionScale Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }

        private OptionTransition _Transition;
        public OptionTransition Transition
        {
            get { return _Transition; }
            set { _Transition = value; }
        }

        public new string ToString()
        {
            string output = Command + " \"" + _Name + "\"";
            foreach (string file in _FileCollection)
                output += " \"" + file + "\"";
            if (_Activity != null)
                output += " " + _Activity.ToString();
            if (_Animation != null)
                output += " " + _Animation.ToString();
            foreach (OptionBlend blend in _BlendCollection)
                output += " " + blend.ToString();
            if (_Control.Flags != MotionFlags.None)
                output += " " + _Control.ToString();
            if (_Fps != null)
                output += " " + _Fps.ToString();
            if (_Frame != null)
                output += " " + _Frame.ToString();
            if (_Loop != null)
                output += " " + _Loop.ToString();
            if (_Node != null)
                output += " " + _Node.ToString();
            if (_Rotate != null)
                output += " " + _Rotate.ToString();
            if (_RTransition != null)
                output += " " + _RTransition.ToString();
            if (_Scale != null)
                output += " " + _Scale.ToString();
            if (_Transition != null)
                output += " " + _Transition.ToString();
            if (_PivotCollection.Count > 0)
            {
                foreach (OptionPivot pivot in _PivotCollection)
                    output += " " + pivot.ToString();
            }
            if (_EventCollection.Count > 0)
            {
                output += " {\r\n";
                foreach (OptionEvent @event in _EventCollection)
                    output += "\t" + @event.ToString() + "\r\n";
                output += "}";
            }
            return output;
        }

        public CommandSequenceV10 ToV10()
        {
            CommandSequenceV10 output = new CommandSequenceV10();
            if (_Activity != null)
                output.Activity = new OptionActivityV10((ActivityV10)Enumerators.ConvertActivity(typeof(ActivityV11), typeof(ActivityV10), _Activity.Activity), _Activity.ActivityWeight);
            output.Animation = _Animation;
            output.BlendCollection = _BlendCollection;
            output.Control = _Control;
            output.EventCollection = _EventCollection;
            output.FileCollection = _FileCollection;
            output.Fps = _Fps;
            output.Frame = _Frame;
            output.Loop = _Loop;
            output.Name = _Name;
            output.Node = _Node;
            output.PivotCollection = _PivotCollection;
            output.Rotate = _Rotate;
            output.RTransition = _RTransition;
            output.Scale = _Scale;
            output.Transition = _Transition;
            return output;
        }
        public CommandSequenceV44 ToV44()
        {
            CommandSequenceV44 output = new CommandSequenceV44();

            if (_Activity != null)
                output.Activity = new OptionActivityV44((ActivityV44)Enumerators.ConvertActivity(typeof(ActivityV11), typeof(ActivityV44), _Activity.Activity), _Activity.ActivityWeight);
            output.Animation = _Animation;
            output.BlendCollection = _BlendCollection;
            output.Control = _Control;
            output.EventCollection = _EventCollection;
            output.FileCollection = _FileCollection;
            output.Fps = _Fps;
            output.Frame = _Frame;
            output.Loop = _Loop;
            output.Name = _Name;
            if (_Node != null)
            {
                if (StaticMethods.SmdFile.Length == 0)
                    output.Node = new OptionNodeV44(_Node.EntryBone.ToString());
                else
                    output.Node = new OptionNodeV44(SmdFile.GetNode(StaticMethods.SmdFile, _Node.EntryBone));
            }
            output.PivotCollection = _PivotCollection;
            output.Rotate = _Rotate;
            output.RTransition = _RTransition;
            output.Scale = _Scale;
            output.Transition = _Transition;

            return output;
        }
    }
}
