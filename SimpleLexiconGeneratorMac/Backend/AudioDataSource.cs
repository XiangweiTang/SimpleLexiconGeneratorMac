using System;
using AppKit;
using System.Collections.Generic;

namespace SimpleLexiconGeneratorMac
{
    class AudioDataSource : NSTableViewDataSource
    {
        public string[] AudioNameArray { get; set; }
        public AudioDataSource()
        {

        }
        public AudioDataSource(string[] array)
        {
            AudioNameArray = array;
        }
        public override nint GetRowCount(NSTableView tableView)
        {
            return AudioNameArray.Length;
        }
    }

    class AudioViewDelegate : NSTableViewDelegate
    {
        private AudioDataSource DataSource;
        public AudioViewDelegate(AudioDataSource dataSource)
        {
            DataSource = dataSource;
        }
        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, System.nint row)
        {
            NSTextField view = (NSTextField)tableView.MakeView("", this);
            if (view == null)
            {
                view = new NSTextField()
                {
                    Identifier = "",
                    BackgroundColor = NSColor.Clear,
                    Bordered = false,
                    Selectable = false,
                    Editable = false
                };
            }
            view.StringValue = DataSource.AudioNameArray[(int)row];
            return view;
        }
    }
}
