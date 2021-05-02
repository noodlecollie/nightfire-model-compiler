using JbnLib.Shared;
using JbnLib.Mdl;
using JbnLib.Lang;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace JbnLib.Qc
{
    public class QcFileV10 : QcFile
    {
        private List<CommandSequenceV10> _SequenceCollection;
        public List<CommandSequenceV10> SequenceCollection
        {
            get { return _SequenceCollection; }
            set { _SequenceCollection = value; }
        }

        private CommandClipToTextures _ClipToTextures;
        public CommandClipToTextures ClipToTextures
        {
            get { return _ClipToTextures; }
            set { _ClipToTextures = value; }
        }

        private CommandExternalTextures _ExternalTextures;
        public CommandExternalTextures ExternalTextures
        {
            get { return _ExternalTextures; }
            set { _ExternalTextures = value; }
        }

        public QcFileV10()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Clear();
        }

        public void Clear()
        {
            _AttachmentCollection = new List<CommandAttachment>();
            _BBox = null;
            _BodyCollection = new List<CommandBody>();
            _BodyGroupCollection = new List<CommandBodyGroup>();
            _CBox = null;
            _Cd = null;
            _CdTexture = null;
            _ClipToTextures = null;
            _ControllerCollection = new List<CommandController>();
            _ExternalTextures = null;
            _EyePosition = null;
            _Flags = null;
            _Gamma = null;
            _HBoxCollection = new List<CommandHBox>();
            _MirrorBone = null;
            _ModelName = null;
            _Origin = null;
            _Pivot = null;
            _RenameBone = null;
            _Root = null;
            _Scale = null;
            _SequenceCollection = new List<CommandSequenceV10>();
            _TextureGroup = null;
            _OmittedCommands = new List<OmittedCommand>();
        }
        public void Read(string file)
        {
            Clear();

            FileInfo fi = new FileInfo(file);
            _Tokenizer = new FileTokenizer(file);
            bool eof = false;
            bool havetoken = false;
            while (!eof)
            {
                try
                {
                    if (!havetoken)
                        eof = _Tokenizer.GetToken();
                    havetoken = false;

                    switch (_Tokenizer.Token.ToLower())
                    {
                        #region " Attachment "
                        case CommandAttachment.Command:
                            CommandAttachment attach = new CommandAttachment();
                            _Tokenizer.GetToken();
                            attach.Name = _Tokenizer.Token;
                            _Tokenizer.GetToken();
                            attach.Bone = _Tokenizer.Token;
                            _Tokenizer.GetToken();
                            attach.Position.X = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            attach.Position.Y = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            attach.Position.Z = Convert.ToSingle(_Tokenizer.Token);
                            _AttachmentCollection.Add(attach);
                            break;
                        #endregion

                        #region " BBox "
                        case CommandBBox.Command:
                            _BBox = new CommandBBox();
                            _Tokenizer.GetToken();
                            _BBox.Min.X = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            _BBox.Min.Y = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            _BBox.Min.Z = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            _BBox.Max.X = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            _BBox.Max.Y = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            _BBox.Max.Z = Convert.ToSingle(_Tokenizer.Token);
                            break;
                        #endregion

                        #region " Body "
                        case CommandBody.Command:
                            CommandBody body = new CommandBody();
                            _Tokenizer.GetToken();
                            body.Name = _Tokenizer.Token;
                            _Tokenizer.GetToken();
                            body.File = _Tokenizer.Token;
                            StaticMethods.SmdFile = fi.DirectoryName + "\\" + body.File + ".smd";
                            _BodyCollection.Add(body);
                            break;
                        #endregion

                        #region " Body Group "
                        case CommandBodyGroup.Command:
                            CommandBodyGroup bodygroup = new CommandBodyGroup();
                            _Tokenizer.GetToken();
                            bodygroup.Name = _Tokenizer.Token;
                            _Tokenizer.GetToken();
                            if (_Tokenizer.Token == "{")
                            {
                                while (true)
                                {
                                    _Tokenizer.GetToken();
                                    if (_Tokenizer.Token != "}")
                                    {
                                        if (_Tokenizer.Token.ToLower() != "blank")
                                        {
                                            BodyGroupItem bgi = new BodyGroupItem();
                                            bgi.Name = _Tokenizer.Token;
                                            _Tokenizer.GetToken();
                                            bgi.File = _Tokenizer.Token;
                                            StaticMethods.SmdFile = fi.DirectoryName + "\\" + bgi.File + ".smd";
                                            bodygroup.BodyCollection.Add(bgi);
                                        }
                                        else
                                        {
                                            BodyGroupItem bgi = new BodyGroupItem();
                                            bgi.Name = _Tokenizer.Token;
                                            bodygroup.BodyCollection.Add(bgi);
                                        }
                                    }
                                    else
                                        break;
                                }
                            }
                            _BodyGroupCollection.Add(bodygroup);
                            break;
                        #endregion

                        #region " CBox "
                        case CommandCBox.Command:
                            _CBox = new CommandCBox();
                            _Tokenizer.GetToken();
                            _CBox.Min.X = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            _CBox.Min.Y = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            _CBox.Min.Z = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            _CBox.Max.X = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            _CBox.Max.Y = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            _CBox.Max.Z = Convert.ToSingle(_Tokenizer.Token);
                            break;
                        #endregion

                        #region " Cd "
                        case CommandCd.Command:
                            _Tokenizer.GetToken();
                            _Cd = new CommandCd(_Tokenizer.Token);
                            break;
                        #endregion

                        #region " Cd Texture "
                        case CommandCdTexture.Command:
                            _Tokenizer.GetToken();
                            _CdTexture = new CommandCdTexture(_Tokenizer.Token);
                            break;
                        #endregion

                        #region " Clip To Textures "
                        case CommandClipToTextures.Command:
                            _ClipToTextures = new CommandClipToTextures();
                            break;
                        #endregion

                        #region " Controller "
                        case CommandController.Command:
                            CommandController controller = new CommandController();
                            _Tokenizer.GetToken();
                            if (_Tokenizer.Token == "mouth")
                                controller.Index = 4;
                            else
                                controller.Index = Convert.ToInt32(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            controller.Bone = _Tokenizer.Token;
                            _Tokenizer.GetToken();
                            controller.Type = Enumerators.ToMotionFlags(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            controller.Start = Convert.ToInt32(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            controller.End = Convert.ToInt32(_Tokenizer.Token);
                            _ControllerCollection.Add(controller);
                            break;
                        #endregion

                        #region " External Textures "
                        case CommandExternalTextures.Command:
                            _ExternalTextures = new CommandExternalTextures();
                            break;
                        #endregion

                        #region " Eye Position "
                        case CommandEyePosition.Command:
                            CommandEyePosition eyeposition = new CommandEyePosition();
                            _Tokenizer.GetToken();
                            eyeposition.Value.X = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            eyeposition.Value.Y = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            eyeposition.Value.Z = Convert.ToSingle(_Tokenizer.Token);
                            _EyePosition = eyeposition;
                            break;
                        #endregion

                        #region " Flags "
                        case CommandFlags.Command:
                            _Tokenizer.GetToken();
                            _Flags = new CommandFlags((TypeFlag)Convert.ToInt32(_Tokenizer.Token));
                            break;
                        #endregion

                        #region " Gamma "
                        case CommandGamma.Command:
                            _Tokenizer.GetToken();
                            _Gamma = new CommandGamma(Convert.ToSingle(_Tokenizer.Token));
                            break;
                        #endregion

                        #region " HBox "
                        case CommandHBox.Command:
                            CommandHBox hbox = new CommandHBox();
                            _Tokenizer.GetToken();
                            hbox.Group = Convert.ToInt32(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            hbox.Bone = _Tokenizer.Token;
                            _Tokenizer.GetToken();
                            hbox.Min.X = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            hbox.Min.Y = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            hbox.Min.Z = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            hbox.Max.X = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            hbox.Max.Y = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            hbox.Max.Z = Convert.ToSingle(_Tokenizer.Token);
                            _HBoxCollection.Add(hbox);
                            break;
                        #endregion

                        #region " Mirror Bone "
                        case CommandMirrorBone.Command:
                            _Tokenizer.GetToken();
                            _MirrorBone = new CommandMirrorBone(_Tokenizer.Token);
                            break;
                        #endregion

                        #region " Model Name "
                        case CommandModelName.Command:
                            _Tokenizer.GetToken();
                            _ModelName = new CommandModelName(_Tokenizer.Token);
                            break;
                        #endregion

                        #region " Origin "
                        case CommandOrigin.Command:
                            CommandOrigin origin = new CommandOrigin();
                            _Tokenizer.GetToken();
                            origin.Value.X = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            origin.Value.Y = Convert.ToSingle(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            origin.Value.Z = Convert.ToSingle(_Tokenizer.Token);
                            _Origin = origin;
                            break;
                        #endregion

                        #region " Pivot "
                        case CommandPivot.Command:
                            CommandPivot pivot = new CommandPivot();
                            _Tokenizer.GetToken();
                            pivot.Index = Convert.ToInt32(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            pivot.Bone = _Tokenizer.Token;
                            _Pivot = pivot;
                            break;
                        #endregion

                        #region " Rename Bone "
                        case CommandRenameBone.Command:
                            CommandRenameBone renamebone = new CommandRenameBone();
                            _Tokenizer.GetToken();
                            renamebone.OldName = _Tokenizer.Token;
                            _Tokenizer.GetToken();
                            renamebone.NewName = _Tokenizer.Token;
                            break;
                        #endregion

                        #region " Root "
                        case CommandRoot.Command:
                            _Tokenizer.GetToken();
                            _Root = new CommandRoot(_Tokenizer.Token);
                            break;
                        #endregion

                        #region " Scale "
                        case CommandScale.Command:
                            _Tokenizer.GetToken();
                            _Scale = new CommandScale(Convert.ToSingle(_Tokenizer.Token));
                            break;
                        #endregion

                        #region " Sequence "
                        case CommandSequenceV10.Command:
                            CommandSequenceV10 sequence = new CommandSequenceV10();
                            _Tokenizer.GetToken();
                            sequence.Name = _Tokenizer.Token;
                            int seqdepth = 0;
                            while (!_Tokenizer.GetToken())
                            {
                                // Ran into another command, get out.
                                if (_Tokenizer.Token.StartsWith("$"))
                                {
                                    havetoken = true;
                                    break;
                                }

                                switch (_Tokenizer.Token.ToLower())
                                {
                                    #region " Depth "
                                    case "{":
                                        seqdepth++;
                                        break;
                                    case "}":
                                        seqdepth--;
                                        break;
                                    #endregion

                                    #region " Animation "
                                    case OptionAnimation.Option:
                                        _Tokenizer.GetToken();
                                        sequence.Animation = new OptionAnimation(_Tokenizer.Token);
                                        break;
                                    #endregion

                                    #region " Blend "
                                    case OptionBlend.Option:
                                        OptionBlend blend = new OptionBlend();
                                        _Tokenizer.GetToken();
                                        blend.Type = Enumerators.ToMotionFlags(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        blend.Start = Convert.ToSingle(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        blend.End = Convert.ToSingle(_Tokenizer.Token);
                                        sequence.BlendCollection.Add(blend);
                                        break;
                                    #endregion

                                    #region " Event "
                                    case OptionEvent.Option:
                                        OptionEvent @event = new OptionEvent();
                                        _Tokenizer.GetToken();
                                        @event.EventValue = Convert.ToInt32(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        @event.Frame = Convert.ToInt32(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        @event.Options = _Tokenizer.Token;
                                        sequence.EventCollection.Add(@event);
                                        break;
                                    #endregion

                                    #region " Fps "
                                    case OptionFps.Option:
                                        _Tokenizer.GetToken();
                                        sequence.Fps = new OptionFps(Convert.ToSingle(_Tokenizer.Token));
                                        break;
                                    #endregion

                                    #region " Frame "
                                    case OptionFrame.Option:
                                        OptionFrame frame = new OptionFrame();
                                        _Tokenizer.GetToken();
                                        frame.Start = Convert.ToInt32(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        frame.End = Convert.ToInt32(_Tokenizer.Token);
                                        sequence.Frame = frame;
                                        break;
                                    #endregion

                                    #region " Loop "
                                    case OptionLoop.Option:
                                        sequence.Loop = new OptionLoop(true);
                                        break;
                                    #endregion

                                    #region " Node "
                                    case OptionNodeV10.Option:
                                        _Tokenizer.GetToken();
                                        sequence.Node = new OptionNodeV10(Convert.ToSByte(_Tokenizer.Token));
                                        break;
                                    #endregion

                                    #region " Pivot "
                                    case OptionPivot.Option:
                                        OptionPivot pivot2 = new OptionPivot();
                                        _Tokenizer.GetToken();
                                        pivot2.Index = Convert.ToInt32(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        pivot2.Start = Convert.ToInt32(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        pivot2.End = Convert.ToInt32(_Tokenizer.Token);
                                        sequence.PivotCollection.Add(pivot2);
                                        break;
                                    #endregion

                                    #region " Rotate "
                                    case OptionRotate.Option:
                                        _Tokenizer.GetToken();
                                        sequence.Rotate = new OptionRotate(Convert.ToInt32(_Tokenizer.Token));
                                        break;
                                    #endregion

                                    #region " RTransition "
                                    case OptionRTransition.Option:
                                        OptionRTransition rtransition = new OptionRTransition();
                                        _Tokenizer.GetToken();
                                        rtransition.EntryBone = Convert.ToInt32(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        rtransition.ExitBone = Convert.ToInt32(_Tokenizer.Token);
                                        sequence.RTransition = rtransition;
                                        break;
                                    #endregion

                                    #region " Scale "
                                    case OptionScale.Option:
                                        _Tokenizer.GetToken();
                                        sequence.Scale = new OptionScale(Convert.ToSingle(_Tokenizer.Token));
                                        break;
                                    #endregion

                                    #region " Transition "
                                    case OptionTransition.Option:
                                        OptionTransition transition = new OptionTransition();
                                        _Tokenizer.GetToken();
                                        transition.EntryBone = Convert.ToInt32(_Tokenizer.Token);
                                        _Tokenizer.GetToken();
                                        transition.ExitBone = Convert.ToInt32(_Tokenizer.Token);
                                        sequence.Transition = transition;
                                        break;
                                    #endregion

                                    #region " Control, Activity, and SMD "
                                    default:
                                        MotionFlags controltemp = Enumerators.ToMotionFlags(_Tokenizer.Token);
                                        ActivityV10 activitytemp;
                                        try { activitytemp = (ActivityV10)Enum.Parse(typeof(ActivityV10), _Tokenizer.Token); }
                                        catch (ArgumentException) { activitytemp = ActivityV10.ACT_INVALID; }

                                        if (controltemp != MotionFlags.Invalid)
                                            sequence.Control.Flags |= controltemp;
                                        else if (activitytemp != ActivityV10.ACT_INVALID)
                                        {
                                            OptionActivityV10 activity = new OptionActivityV10();
                                            activity.Activity = activitytemp;
                                            _Tokenizer.GetToken();
                                            activity.ActivityWeight = Convert.ToSingle(_Tokenizer.Token);
                                            sequence.Activity = activity;
                                        }
                                        else
                                        {
                                            StaticMethods.SmdFile = fi.DirectoryName + "\\" + _Tokenizer.Token + ".smd";
                                            sequence.FileCollection.Add(_Tokenizer.Token);
                                        }
                                        break;
                                    #endregion
                                }
                            }
                            _SequenceCollection.Add(sequence);
                            break;
                        #endregion

                        #region " Texture Group "
                        case CommandTextureGroup.Command:
                            CommandTextureGroup texturegroup = new CommandTextureGroup();
                            _Tokenizer.GetToken();
                            texturegroup.Name = _Tokenizer.Token;
                            int depth = 0;
                            List<string> references = new List<string>();

                            while (!_Tokenizer.GetToken())
                            {
                                if (_Tokenizer.Token == "{")
                                    depth++;
                                else if (_Tokenizer.Token == "}")
                                {
                                    depth--;
                                    if (depth == 0)
                                        break;
                                    texturegroup.SkinCollection.Add(references);
                                    references = new List<string>();
                                }
                                else if (depth == 2)
                                {
                                    references.Add(_Tokenizer.Token);
                                }
                            }
                            break;
                        #endregion

                        #region " Default "
                        default:
                            if (_Tokenizer.Token.StartsWith("$"))
                                _OmittedCommands.Add(new OmittedCommand(_Tokenizer.Line, _Tokenizer.Token));
                            break;
                        #endregion
                    }
                }
                catch (Exception e)
                {
                    Messages.ThrowException("Qc.QcFileV10.Read(string)", e.Message + " (" + Messages.LINE + _Tokenizer.Line + ", " + Messages.TOKEN + _Tokenizer.Token + ")");
                }
            }
        }
        public void Write(string file)
        {
            StreamWriter sw = new StreamWriter(file);

            #region " File System Related "
            if (_Cd != null)
                sw.WriteLine(_Cd.ToString());

            if (_CdTexture != null)
                sw.WriteLine(_CdTexture.ToString());

            if (_ClipToTextures != null)
                sw.WriteLine(_ClipToTextures.ToString());

            if (_ExternalTextures != null)
                sw.WriteLine(_ExternalTextures.ToString());

            if (_ModelName != null)
                sw.WriteLine(_ModelName.ToString());
            sw.WriteLine();
            sw.Flush();
            #endregion

            #region " Bone Related "
            if (_Root != null)
                sw.WriteLine(_Root.ToString());

            if (_Pivot != null)
                sw.WriteLine(_Pivot.ToString());

            if (_MirrorBone != null)
                sw.WriteLine(_MirrorBone.ToString());

            if (_RenameBone != null)
                sw.WriteLine(_RenameBone.ToString());
            sw.WriteLine();
            sw.Flush();
            #endregion

            #region " Model Related "
            if (_Gamma != null)
                sw.WriteLine(_Gamma.ToString());

            if (_Scale != null)
                sw.WriteLine(_Scale.ToString());

            if (_Flags != null)
                sw.WriteLine(_Flags.ToString());

            if (_Origin != null)
                sw.WriteLine(_Origin.ToString());

            if (_EyePosition != null)
                sw.WriteLine(_EyePosition.ToString());

            if (_BBox != null)
                sw.WriteLine(_BBox.ToString());

            if (_CBox != null)
                sw.WriteLine(_CBox.ToString());
            sw.WriteLine();
            sw.Flush();
            #endregion

            #region " Textures "
            if (_TextureGroup != null)
                sw.WriteLine(_TextureGroup.ToString());
            sw.WriteLine();
            sw.Flush();
            #endregion

            #region " Bodies "
            foreach (CommandBody body in _BodyCollection)
                sw.WriteLine(body.ToString());
            sw.WriteLine();
            sw.Flush();

            foreach (CommandBodyGroup bodygroup in _BodyGroupCollection)
                sw.WriteLine(bodygroup.ToString());
            sw.WriteLine();
            sw.Flush();
            #endregion

            #region " Attachments "
            foreach (CommandAttachment attachment in _AttachmentCollection)
                sw.WriteLine(attachment.ToString());
            sw.WriteLine();
            sw.Flush();
            #endregion

            #region " Controllers "
            foreach (CommandController controller in _ControllerCollection)
                sw.WriteLine(controller.ToString());
            sw.WriteLine();
            sw.Flush();
            #endregion

            #region " Hit Groups "
            foreach (CommandHBox hbox in _HBoxCollection)
                sw.WriteLine(hbox.ToString());
            sw.WriteLine();
            sw.Flush();
            #endregion

            #region " Sequences "
            foreach (CommandSequenceV10 sequence in _SequenceCollection)
            {
                sw.WriteLine(sequence.ToString());
                sw.Flush();
            }
            #endregion

            sw.Close();
        }

        public QcFileV11 ToV11()
        {
            QcFileV11 output = new QcFileV11();

            output.AttachmentCollection = _AttachmentCollection;
            output.BBox = _BBox;
            output.BodyCollection = _BodyCollection;
            output.BodyGroupCollection = _BodyGroupCollection;
            output.CBox = _CBox;
            output.Cd = _Cd;
            output.CdTexture = _CdTexture;
            output.ControllerCollection = _ControllerCollection;
            output.EyePosition = _EyePosition;
            output.Flags = _Flags;
            output.Gamma = _Gamma;
            output.HBoxCollection = _HBoxCollection;
            output.MirrorBone = _MirrorBone;
            output.ModelName = _ModelName;
            output.Origin = _Origin;
            output.Pivot = _Pivot;
            output.RenameBone = _RenameBone;
            output.Root = _Root;
            output.Scale = _Scale;
            foreach (CommandSequenceV10 seq in _SequenceCollection)
                output.SequenceCollection.Add(seq.ToV11());
            output.TextureGroup = _TextureGroup;

            return output;
        }
        public QcFileV44 ToV44()
        {
            QcFileV44 output = new QcFileV44();

            output.Clear();
            output.AttachmentCollection = _AttachmentCollection;
            output.BBox = _BBox;
            output.BodyCollection = _BodyCollection;
            output.BodyGroupCollection = _BodyGroupCollection;
            output.ControllerCollection = _ControllerCollection;
            output.EyePosition = _EyePosition;
            output.Flags = _Flags;
            output.Gamma = _Gamma;
            output.HBoxCollection = _HBoxCollection;
            output.ModelName = _ModelName;
            output.Origin = _Origin;
            foreach (CommandSequenceV10 seq in _SequenceCollection)
                output.SequenceCollection.Add(seq.ToV44());
            output.TextureGroup = _TextureGroup;

            return output;
        }
    }
}
