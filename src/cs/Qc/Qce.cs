using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using JbnLib.Shared;
using JbnLib.Lang;

namespace JbnLib.Qc
{
    public class Qce
    {
        private FileTokenizer _Tokenizer;

        private CommandQceVersion _QceVersion;
        public CommandQceVersion QceVersion
        {
            get { return _QceVersion; }
            set { _QceVersion = value; }
        }

        private List<CommandReplaceActivity> _ReplaceActivityCollection;
        public List<CommandReplaceActivity> ReplaceActivityCollection
        {
            get { return _ReplaceActivityCollection; }
            set { _ReplaceActivityCollection = value; }
        }

        private List<CommandLodTemp> _LodTempCollection;
        public List<CommandLodTemp> LodTempCollection
        {
            get { return _LodTempCollection; }
            set { _LodTempCollection = value; }
        }

        private CommandLodFlags _LodFlags;
        public CommandLodFlags LodFlags
        {
            get { return _LodFlags; }
            set { _LodFlags = value; }
        }
        
        public Qce()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Clear();
        }

        public void Clear()
        {
            _QceVersion = null;
            _ReplaceActivityCollection = new List<CommandReplaceActivity>();
            _LodTempCollection = new List<CommandLodTemp>();
            _LodFlags = new CommandLodFlags();
        }
        public void Read(string file)
        {
            Clear();

            _Tokenizer = new FileTokenizer(file);

            try
            {
                while (!_Tokenizer.GetToken())
                {
                    switch (_Tokenizer.Token)
                    {
                        case CommandQceVersion.Command:
                            _Tokenizer.GetToken();
                            _QceVersion = new CommandQceVersion(_Tokenizer.Token);
                            break;
                        case CommandReplaceActivity.Command:
                            _Tokenizer.GetToken();
                            CommandReplaceActivity cra = new CommandReplaceActivity();
                            cra.SequenceName = _Tokenizer.Token;
                            _Tokenizer.GetToken();
                            cra.Activity = _Tokenizer.Token;
                            _ReplaceActivityCollection.Add(cra);
                            break;
                        case CommandLodTemp.Command:
                            _Tokenizer.GetToken();
                            CommandLodTemp lodtemp = new CommandLodTemp();
                            lodtemp.Index = Convert.ToInt32(_Tokenizer.Token);
                            _Tokenizer.GetToken();
                            byte count = Convert.ToByte(_Tokenizer.Token);
                            for (byte i = 0; i < count; i++)
                            {
                                _Tokenizer.GetToken();
                                lodtemp.Distances.Add(Convert.ToInt32(_Tokenizer.Token));
                            }
                            _LodTempCollection.Add(lodtemp);
                            break;
                        case CommandLodFlags.Command:
                            _Tokenizer.GetToken();
                            _LodFlags = new CommandLodFlags(Convert.ToInt32(_Tokenizer.Token));
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Messages.ThrowException("Qc.Qce.Read(string)", Messages.LINE + _Tokenizer.Line + ", " + Messages.TOKEN + _Tokenizer.Token + "\n" + e.Message);
            }
        }
        public void Write(string file)
        {
            StreamWriter sw = new StreamWriter(file);

            if (_QceVersion == null)
                Messages.ThrowException("Qc.Qce.Write(string)", Messages.QCEVERSION_NULL);
            sw.WriteLine(_QceVersion.ToString());
            sw.WriteLine();
            sw.Flush();

            foreach (CommandReplaceActivity item in _ReplaceActivityCollection)
                sw.WriteLine(item.ToString());
            sw.WriteLine();
            sw.Flush();

            if (_LodFlags.Value != 0)
                sw.WriteLine(_LodFlags.ToString());
            foreach (CommandLodTemp item in _LodTempCollection)
                sw.WriteLine(item.ToString());
            sw.WriteLine();
            sw.Flush();

            sw.Close();
        }
    }
}
