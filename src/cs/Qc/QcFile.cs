using System.Collections.Generic;
using JbnLib.Shared;
using System.IO;
using JbnLib.Lang;
using System;

namespace JbnLib.Qc
{
    public class QcFile
    {
        internal List<CommandAttachment> _AttachmentCollection;
        public List<CommandAttachment> AttachmentCollection
        {
            get { return _AttachmentCollection; }
            set { _AttachmentCollection = value; }
        }

        internal CommandBBox _BBox;
        public CommandBBox BBox
        {
            get { return _BBox; }
            set { _BBox = value; }
        }

        internal List<CommandBody> _BodyCollection;
        public List<CommandBody> BodyCollection
        {
            get { return _BodyCollection; }
            set { _BodyCollection = value; }
        }

        internal List<CommandBodyGroup> _BodyGroupCollection;
        public List<CommandBodyGroup> BodyGroupCollection
        {
            get { return _BodyGroupCollection; }
            set { _BodyGroupCollection = value; }
        }

        internal CommandCBox _CBox;
        public CommandCBox CBox
        {
            get { return _CBox; }
            set { _CBox = value; }
        }

        internal CommandCd _Cd;
        public CommandCd Cd
        {
            get { return _Cd; }
            set { _Cd = value; }
        }

        internal CommandCdTexture _CdTexture;
        public CommandCdTexture CdTexture
        {
            get { return _CdTexture; }
            set { _CdTexture = value; }
        }

        internal List<CommandController> _ControllerCollection;
        public List<CommandController> ControllerCollection
        {
            get { return _ControllerCollection; }
            set { _ControllerCollection = value; }
        }

        internal CommandEyePosition _EyePosition;
        public CommandEyePosition EyePosition
        {
            get { return _EyePosition; }
            set { _EyePosition = value; }
        }

        internal CommandFlags _Flags;
        public CommandFlags Flags
        {
            get { return _Flags; }
            set { _Flags = value; }
        }

        internal CommandGamma _Gamma;
        public CommandGamma Gamma
        {
            get { return _Gamma; }
            set { _Gamma = value; }
        }

        internal List<CommandHBox> _HBoxCollection;
        public List<CommandHBox> HBoxCollection
        {
            get { return _HBoxCollection; }
            set { _HBoxCollection = value; }
        }

        internal CommandMirrorBone _MirrorBone;
        public CommandMirrorBone MirrorBone
        {
            get { return _MirrorBone; }
            set { _MirrorBone = value; }
        }

        internal CommandModelName _ModelName;
        public CommandModelName ModelName
        {
            get { return _ModelName; }
            set { _ModelName = value; }
        }

        internal CommandOrigin _Origin;
        public CommandOrigin Origin
        {
            get { return _Origin; }
            set { _Origin = value; }
        }

        internal CommandPivot _Pivot;
        public CommandPivot Pivot
        {
            get { return _Pivot; }
            set { _Pivot = value; }
        }

        internal CommandRenameBone _RenameBone;
        public CommandRenameBone RenameBone
        {
            get { return _RenameBone; }
            set { _RenameBone = value; }
        }

        internal CommandRoot _Root;
        public CommandRoot Root
        {
            get { return _Root; }
            set { _Root = value; }
        }

        internal CommandScale _Scale;
        public CommandScale Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }

        internal CommandTextureGroup _TextureGroup;
        public CommandTextureGroup TextureGroup
        {
            get { return _TextureGroup; }
            set { _TextureGroup = value; }
        }

        internal FileTokenizer _Tokenizer;

        internal List<OmittedCommand> _OmittedCommands;
        public List<OmittedCommand> OmittedCommands
        {
            get { return _OmittedCommands; }
            set { _OmittedCommands = value; }
        }

        public static string GetModelName(string file)
        {
            FileTokenizer Tokenizer = new FileTokenizer(file);
            while (!Tokenizer.GetToken())
            {
                if (Tokenizer.Token == CommandModelName.Command)
                {
                    Tokenizer.GetToken();
                    return Tokenizer.Token;
                }
            }
            return "";
        }

        public static void FixModelName(string file)
        {
            FileInfo fi = new FileInfo(file);
            if (fi.Exists)
            {
                if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\" + fi.Name + ".bak"))
                    File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\" + fi.Name + ".bak");
                File.Move(fi.FullName, Environment.GetEnvironmentVariable("TEMP") + "\\" + fi.Name + ".bak");
            }
            else
                Messages.ThrowException("Qc.QcFile.FixModelName(string)", Messages.FILE_NOT_FOUND + file);

            StreamReader sr = new StreamReader(Environment.GetEnvironmentVariable("TEMP") + "\\" + fi.Name + ".bak");
            StreamWriter sw = new StreamWriter(fi.FullName);
            sw.AutoFlush = true;
            while (sr.Peek() != -1)
            {
                string read = sr.ReadLine();
                if (read.StartsWith(CommandModelName.Command))
                {
                    if (read.IndexOf('"') == -1)
                    {
                        read = read.Replace(CommandModelName.Command + " ", "");
                        sw.WriteLine(CommandModelName.Command + " \"" + read + "\"");
                    }
                    else
                        sw.WriteLine(read);
                }
                else
                    sw.WriteLine(read);
            }
            sr.Close();
            sw.Flush();
            sw.Close();
            File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\" + fi.Name + ".bak");
        }
    }
}
