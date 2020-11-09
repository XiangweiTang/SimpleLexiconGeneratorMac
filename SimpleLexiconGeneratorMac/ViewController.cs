using System;
using System.IO;
using AppKit;
using Foundation;
using System.Collections.Generic;
using System.Net;
using System.Globalization;

namespace SimpleLexiconGeneratorMac
{
    public partial class ViewController : NSViewController
    {
        private Backend BE = new Backend();
        private bool Initialized = false;
        private bool OverallBlock = false;
        public ViewController(IntPtr handle) : base(handle)
        {
            InitBackend();
            if (DateExpired())
            {
                ShowMessageBox("Warning", "Your app may expire or not connecting to internet.");
                OverallBlock = true;
                Initialized = false;
            }
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
            try
            {
                string currentPath = Directory.GetCurrentDirectory().Replace("SLG.app/Contents/Resources", "");
                BE.InputAudioFolderPath = Path.Combine(currentPath, "Audio/");
                BE.InputTextFilePath = Path.Combine(currentPath, "Data.txt");
                BE.Init();
                ViewDidLoad();
                Sanity.Requires(File.Exists(BE.InputTextFilePath), "Missing text file.");
                Sanity.Requires(Directory.Exists(BE.InputAudioFolderPath), "Missing audio folder.");
            }
            catch
            {
                return;
            }
            if (!OverallBlock)
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
                SaveCurrentData();
                LoadCurrent();
            });
        }

        partial void BtnPlayClick(NSButton sender)
        {
            if (!Initialized)
                return;
            GeneralAction(() =>
            {
                BE.Play();
            });
        }

        partial void BtnSaveClick(NSButton sender)
        {
            GeneralAction(() =>
            {
                if (DateExpired())
                {
                    ShowMessageBox("Warning", "Your app may expire or not connecting to internet.");
                    OverallBlock = true;
                    Initialized = false;
                    return;
                }
                SaveCurrentData();
                BE.SaveAllData();
                ShowMessageBox("Info", "Saving is done!");
            });
        }

        private void SaveCurrentData()
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

        const string MS_URL = "http://www.microsoft.com";
        static readonly DateTime ExpiredDate = new DateTime(2021, 1, 1);

        private bool DateExpired()
        {
            try
            {
                HttpWebRequest req = WebRequest.CreateHttp(MS_URL);
                using(HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                {
                    string timeString = resp.Headers["date"];
                    DateTime dt = DateTime.ParseExact(timeString, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
                    return dt >= ExpiredDate;
                }
            }
            catch
            {
                return true;
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
