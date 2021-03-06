// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SimpleLexiconGeneratorMac
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTableView AudioTable { get; set; }

		[Outlet]
		AppKit.NSTextField Label1Show { get; set; }

		[Outlet]
		AppKit.NSTextField Label2Show { get; set; }

		[Outlet]
		AppKit.NSTextField Label3Show { get; set; }

		[Outlet]
		AppKit.NSTextField Label4Show { get; set; }

		[Outlet]
		AppKit.NSTextFieldCell LabelContextShow { get; set; }

		[Outlet]
		AppKit.NSTextField TextHighGermanShow { get; set; }

		[Outlet]
		AppKit.NSTextField TextLexiconShow { get; set; }

		[Outlet]
		AppKit.NSTextField TextTransliterationShow { get; set; }

		[Action ("BtnLoadClick:")]
		partial void BtnLoadClick (AppKit.NSButton sender);

		[Action ("BtnNextClick:")]
		partial void BtnNextClick (Foundation.NSObject sender);

		[Action ("BtnPlayClick:")]
		partial void BtnPlayClick (AppKit.NSButton sender);

		[Action ("BtnPreviousClick:")]
		partial void BtnPreviousClick (AppKit.NSButton sender);

		[Action ("BtnSaveClick:")]
		partial void BtnSaveClick (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AudioTable != null) {
				AudioTable.Dispose ();
				AudioTable = null;
			}

			if (Label1Show != null) {
				Label1Show.Dispose ();
				Label1Show = null;
			}

			if (Label2Show != null) {
				Label2Show.Dispose ();
				Label2Show = null;
			}

			if (Label3Show != null) {
				Label3Show.Dispose ();
				Label3Show = null;
			}

			if (Label4Show != null) {
				Label4Show.Dispose ();
				Label4Show = null;
			}

			if (LabelContextShow != null) {
				LabelContextShow.Dispose ();
				LabelContextShow = null;
			}

			if (TextHighGermanShow != null) {
				TextHighGermanShow.Dispose ();
				TextHighGermanShow = null;
			}

			if (TextTransliterationShow != null) {
				TextTransliterationShow.Dispose ();
				TextTransliterationShow = null;
			}

			if (TextLexiconShow != null) {
				TextLexiconShow.Dispose ();
				TextLexiconShow = null;
			}
		}
	}
}
