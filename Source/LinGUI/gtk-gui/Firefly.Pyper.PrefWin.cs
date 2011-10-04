
// This file has been generated by the GUI designer. Do not modify.
namespace Firefly.Pyper
{
	public partial class PrefWin
	{
		private global::Gtk.Notebook PrefNoteBook;

		private global::Gtk.Alignment alignment1;

		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Label label3;

		private global::Gtk.Entry PipeFontEntry;

		private global::Gtk.Button PipeFontBrowseButton;

		private global::Gtk.HBox hbox7;

		private global::Gtk.CheckButton CommandAutoCompletionCheckBox;

		private global::Gtk.Label label1;

		private global::Gtk.Alignment alignment2;

		private global::Gtk.VBox vbox3;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Label label4;

		private global::Gtk.Entry TextFontEntry;

		private global::Gtk.Button TextFontBrowseButton;

		private global::Gtk.HBox hbox3;

		private global::Gtk.CheckButton WrapTextCheckBox;

		private global::Gtk.Label label2;

		private global::Gtk.Alignment alignment3;

		private global::Gtk.VBox vbox4;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Label label7;

		private global::Gtk.Entry ErrorsFontEntry;

		private global::Gtk.Button ErrorsFontBrowseButton;

		private global::Gtk.Label label6;

		private global::Gtk.Alignment alignment4;

		private global::Gtk.VBox vbox5;

		private global::Gtk.HBox hbox6;

		private global::Gtk.Label label10;

		private global::Gtk.Entry DebugFileEntry;

		private global::Gtk.Label label8;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Firefly.Pyper.PrefWin
			this.Name = "Firefly.Pyper.PrefWin";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child Firefly.Pyper.PrefWin.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.PrefNoteBook = new global::Gtk.Notebook ();
			this.PrefNoteBook.CanFocus = true;
			this.PrefNoteBook.Name = "PrefNoteBook";
			this.PrefNoteBook.CurrentPage = 3;
			// Container child PrefNoteBook.Gtk.Notebook+NotebookChild
			this.alignment1 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment1.Name = "alignment1";
			this.alignment1.BorderWidth = ((uint)(3));
			// Container child alignment1.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Homogeneous = true;
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Font");
			this.hbox1.Add (this.label3);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.label3]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.PipeFontEntry = new global::Gtk.Entry ();
			this.PipeFontEntry.CanFocus = true;
			this.PipeFontEntry.Name = "PipeFontEntry";
			this.PipeFontEntry.IsEditable = true;
			this.PipeFontEntry.InvisibleChar = '●';
			this.hbox1.Add (this.PipeFontEntry);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.PipeFontEntry]));
			w3.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.PipeFontBrowseButton = new global::Gtk.Button ();
			this.PipeFontBrowseButton.CanFocus = true;
			this.PipeFontBrowseButton.Name = "PipeFontBrowseButton";
			this.PipeFontBrowseButton.UseUnderline = true;
			this.PipeFontBrowseButton.Label = global::Mono.Unix.Catalog.GetString ("...");
			this.hbox1.Add (this.PipeFontBrowseButton);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.PipeFontBrowseButton]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox7 = new global::Gtk.HBox ();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.CommandAutoCompletionCheckBox = new global::Gtk.CheckButton ();
			this.CommandAutoCompletionCheckBox.CanFocus = true;
			this.CommandAutoCompletionCheckBox.Name = "CommandAutoCompletionCheckBox";
			this.CommandAutoCompletionCheckBox.Label = global::Mono.Unix.Catalog.GetString ("Auto-complete pipe commands?");
			this.CommandAutoCompletionCheckBox.DrawIndicator = true;
			this.CommandAutoCompletionCheckBox.UseUnderline = true;
			this.hbox7.Add (this.CommandAutoCompletionCheckBox);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.CommandAutoCompletionCheckBox]));
			w6.Position = 0;
			this.vbox2.Add (this.hbox7);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox7]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			this.alignment1.Add (this.vbox2);
			this.PrefNoteBook.Add (this.alignment1);
			// Notebook tab
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Pipe");
			this.PrefNoteBook.SetTabLabel (this.alignment1, this.label1);
			this.label1.ShowAll ();
			// Container child PrefNoteBook.Gtk.Notebook+NotebookChild
			this.alignment2 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment2.Name = "alignment2";
			this.alignment2.BorderWidth = ((uint)(3));
			// Container child alignment2.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Homogeneous = true;
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Font");
			this.hbox2.Add (this.label4);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.label4]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.TextFontEntry = new global::Gtk.Entry ();
			this.TextFontEntry.CanFocus = true;
			this.TextFontEntry.Name = "TextFontEntry";
			this.TextFontEntry.IsEditable = true;
			this.TextFontEntry.InvisibleChar = '●';
			this.hbox2.Add (this.TextFontEntry);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.TextFontEntry]));
			w11.Position = 1;
			// Container child hbox2.Gtk.Box+BoxChild
			this.TextFontBrowseButton = new global::Gtk.Button ();
			this.TextFontBrowseButton.CanFocus = true;
			this.TextFontBrowseButton.Name = "TextFontBrowseButton";
			this.TextFontBrowseButton.UseUnderline = true;
			this.TextFontBrowseButton.Label = global::Mono.Unix.Catalog.GetString ("...");
			this.hbox2.Add (this.TextFontBrowseButton);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.TextFontBrowseButton]));
			w12.Position = 2;
			w12.Expand = false;
			w12.Fill = false;
			this.vbox3.Add (this.hbox2);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox2]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.WrapTextCheckBox = new global::Gtk.CheckButton ();
			this.WrapTextCheckBox.CanFocus = true;
			this.WrapTextCheckBox.Name = "WrapTextCheckBox";
			this.WrapTextCheckBox.Label = global::Mono.Unix.Catalog.GetString ("Wrap Text?");
			this.WrapTextCheckBox.DrawIndicator = true;
			this.WrapTextCheckBox.UseUnderline = true;
			this.hbox3.Add (this.WrapTextCheckBox);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.WrapTextCheckBox]));
			w14.Position = 0;
			this.vbox3.Add (this.hbox3);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox3]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			this.alignment2.Add (this.vbox3);
			this.PrefNoteBook.Add (this.alignment2);
			global::Gtk.Notebook.NotebookChild w17 = ((global::Gtk.Notebook.NotebookChild)(this.PrefNoteBook[this.alignment2]));
			w17.Position = 1;
			// Notebook tab
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("I/O Text");
			this.PrefNoteBook.SetTabLabel (this.alignment2, this.label2);
			this.label2.ShowAll ();
			// Container child PrefNoteBook.Gtk.Notebook+NotebookChild
			this.alignment3 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment3.Name = "alignment3";
			this.alignment3.BorderWidth = ((uint)(3));
			// Container child alignment3.Gtk.Container+ContainerChild
			this.vbox4 = new global::Gtk.VBox ();
			this.vbox4.Name = "vbox4";
			this.vbox4.Homogeneous = true;
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Font");
			this.hbox4.Add (this.label7);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.label7]));
			w18.Position = 0;
			w18.Expand = false;
			w18.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.ErrorsFontEntry = new global::Gtk.Entry ();
			this.ErrorsFontEntry.CanFocus = true;
			this.ErrorsFontEntry.Name = "ErrorsFontEntry";
			this.ErrorsFontEntry.IsEditable = true;
			this.ErrorsFontEntry.InvisibleChar = '●';
			this.hbox4.Add (this.ErrorsFontEntry);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.ErrorsFontEntry]));
			w19.Position = 1;
			// Container child hbox4.Gtk.Box+BoxChild
			this.ErrorsFontBrowseButton = new global::Gtk.Button ();
			this.ErrorsFontBrowseButton.CanFocus = true;
			this.ErrorsFontBrowseButton.Name = "ErrorsFontBrowseButton";
			this.ErrorsFontBrowseButton.UseUnderline = true;
			this.ErrorsFontBrowseButton.Label = global::Mono.Unix.Catalog.GetString ("...");
			this.hbox4.Add (this.ErrorsFontBrowseButton);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.ErrorsFontBrowseButton]));
			w20.Position = 2;
			w20.Expand = false;
			w20.Fill = false;
			this.vbox4.Add (this.hbox4);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.hbox4]));
			w21.Position = 0;
			w21.Expand = false;
			w21.Fill = false;
			this.alignment3.Add (this.vbox4);
			this.PrefNoteBook.Add (this.alignment3);
			global::Gtk.Notebook.NotebookChild w23 = ((global::Gtk.Notebook.NotebookChild)(this.PrefNoteBook[this.alignment3]));
			w23.Position = 2;
			// Notebook tab
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Errors");
			this.PrefNoteBook.SetTabLabel (this.alignment3, this.label6);
			this.label6.ShowAll ();
			// Container child PrefNoteBook.Gtk.Notebook+NotebookChild
			this.alignment4 = new global::Gtk.Alignment (0.5f, 0.5f, 1f, 1f);
			this.alignment4.Name = "alignment4";
			this.alignment4.BorderWidth = ((uint)(3));
			// Container child alignment4.Gtk.Container+ContainerChild
			this.vbox5 = new global::Gtk.VBox ();
			this.vbox5.Name = "vbox5";
			this.vbox5.Homogeneous = true;
			this.vbox5.Spacing = 6;
			// Container child vbox5.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.label10 = new global::Gtk.Label ();
			this.label10.Name = "label10";
			this.label10.LabelProp = global::Mono.Unix.Catalog.GetString ("Debug File");
			this.hbox6.Add (this.label10);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.label10]));
			w24.Position = 0;
			w24.Expand = false;
			w24.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.DebugFileEntry = new global::Gtk.Entry ();
			this.DebugFileEntry.CanFocus = true;
			this.DebugFileEntry.Name = "DebugFileEntry";
			this.DebugFileEntry.IsEditable = true;
			this.DebugFileEntry.InvisibleChar = '●';
			this.hbox6.Add (this.DebugFileEntry);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.DebugFileEntry]));
			w25.Position = 1;
			this.vbox5.Add (this.hbox6);
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.vbox5[this.hbox6]));
			w26.Position = 0;
			w26.Expand = false;
			w26.Fill = false;
			this.alignment4.Add (this.vbox5);
			this.PrefNoteBook.Add (this.alignment4);
			global::Gtk.Notebook.NotebookChild w28 = ((global::Gtk.Notebook.NotebookChild)(this.PrefNoteBook[this.alignment4]));
			w28.Position = 3;
			// Notebook tab
			this.label8 = new global::Gtk.Label ();
			this.label8.Name = "label8";
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString ("Debug");
			this.PrefNoteBook.SetTabLabel (this.alignment4, this.label8);
			this.label8.ShowAll ();
			w1.Add (this.PrefNoteBook);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(w1[this.PrefNoteBook]));
			w29.Position = 0;
			// Internal child Firefly.Pyper.PrefWin.ActionArea
			global::Gtk.HButtonBox w30 = this.ActionArea;
			w30.Name = "dialog1_ActionArea";
			w30.Spacing = 6;
			w30.BorderWidth = ((uint)(5));
			w30.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w31 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w30[this.buttonCancel]));
			w31.Expand = false;
			w31.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w32 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w30[this.buttonOk]));
			w32.Position = 1;
			w32.Expand = false;
			w32.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 508;
			this.DefaultHeight = 300;
			this.Show ();
			this.PipeFontBrowseButton.Clicked += new global::System.EventHandler (this.OnPipeFontBrowseButtonClicked);
			this.TextFontBrowseButton.Clicked += new global::System.EventHandler (this.OnTextFontBrowseButtonClicked);
			this.ErrorsFontBrowseButton.Clicked += new global::System.EventHandler (this.OnErrorsFontBrowseButtonClicked);
		}
	}
}
