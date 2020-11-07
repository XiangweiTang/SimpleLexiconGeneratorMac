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
        public ViewController(IntPtr handle) : base(handle)
        {
            InitBackend();            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Label1Show.StringValue = BE.Column1String;
            Label2Show.StringValue = BE.Column2String;
            Label3Show.StringValue = BE.Column3String;
            Label4Show.StringValue = BE.Column4String;
            
            
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
            Initialized = false;
            try
            {
                string currentPath = Directory.GetCurrentDirectory().Replace("Contents/Resources", "");
                BE.InputAudioFolderPath = Path.Combine(currentPath, "Audio/");
                BE.InputTextFilePath = Path.Combine(currentPath, "Transcript.txt");
                Sanity.Requires(File.Exists(BE.InputTextFilePath), "Missing text file.");
                Sanity.Requires(Directory.Exists(BE.InputAudioFolderPath), "Missing audio folder.");
                BE.Init();
                ViewDidLoad();
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
            if (!Initialized)
                return;
        }

        partial void BtnPlayClick(NSButton sender)
        {
            if (!Initialized)
                return;
        }
        partial void BtnSaveClick(NSButton sender)
        {
            if (!Initialized)
                return;
        }
    }
}
