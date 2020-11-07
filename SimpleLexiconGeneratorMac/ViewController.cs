using System;
using System.IO;
using AppKit;
using Foundation;
using System.Collections.Generic;

namespace SimpleLexiconGeneratorMac
{
    public partial class ViewController : NSViewController
    {
        private Backend BE = new Backend();
        private bool Initialized = false;
        private bool PlayingFlag = false;
        public ViewController(IntPtr handle) : base(handle)
        {
            InitBackend();            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (BE != null)
            {
                Label1Show.StringValue = BE.Column1String;
                Label2Show.StringValue = BE.Column2String;
                Label3Show.StringValue = BE.Column3String;
                Label4Show.StringValue = BE.Column4String;
            }
            
            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }        

        private void InitBackend()
        {
            BE.Stop();
            Initialized = false;
            PlayingFlag = false;
            try
            {
                string currentPath = Directory.GetCurrentDirectory().Replace("SLG.app/Contents/Resources", "");
                BE.InputAudioFolderPath = Path.Combine(currentPath, "Audio/");
                BE.InputTextFilePath = Path.Combine(currentPath, "Transcript.txt");
                BE.Init();
                ViewDidLoad();
                Sanity.Requires(File.Exists(BE.InputTextFilePath), "Missing text file.");
                Sanity.Requires(Directory.Exists(BE.InputAudioFolderPath), "Missing audio folder.");
            }
            catch
            {
                return;
            }
            Initialized = true;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            var dataSource = new AudioDataSource(BE.AudioNames);
            AudioTable.DataSource = dataSource;
            AudioTable.Delegate = new AudioViewDelegate(dataSource);            
        }

        partial void BtnLoadClick(NSButton sender)
        {
            if (!Initialized)
                InitBackend();
            GeneralAction(() =>
            {
                BE.Stop();
                SaveCurrent();
                LoadCurrent();
            });
        }

        partial void BtnPlayClick(NSButton sender)
        {
            if (!Initialized)
                return;
            GeneralAction(() =>
            {
                if (PlayingFlag)
                    BE.Stop();
                else
                    BE.Play();
                PlayingFlag = !PlayingFlag;
            });
        }

        partial void BtnSaveClick(NSButton sender)
        {
            GeneralAction(() =>
            {
                BE.SaveAllData();
            });
        }

        private void SaveCurrent()
        {
            BE.SaveCurrentData(TextTransliterationShow.StringValue,
                TextHighGermanShow.StringValue,
                LabelContextShow.StringValue,
                TextLexiconShow.StringValue);
        }

        private void LoadCurrent()
        {
            int index = (int)AudioTable.SelectedRow;
            BE.SetIndex(index);
            LabelContextShow.StringValue = BE.CurrentLine.Context;
            TextHighGermanShow.StringValue = BE.CurrentLine.HighGerman;
            TextTransliterationShow.StringValue = BE.CurrentLine.Transliteration;
            TextLexiconShow.StringValue = BE.CurrentLine.Lexicon;
        }

        private void GeneralAction(Action action)
        {
            if (!Initialized)
                return;
            try
            {
                action.Invoke();
            }
            catch(Exception e)
            {
                ShowMessageBox("Error", e.Message);
            }
        }

        private void ShowMessageBox(string title="Title", string message = "Message")
        {
            NSAlert alert = new NSAlert();
            alert.MessageText = title;
            alert.InformativeText = message;
            alert.RunModal();
        }
    }
}
