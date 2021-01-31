using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadPlus
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            _tabCollection = new TabCollection(_tabControl, _rtbContextMenuStrip);
        }

        private void OnTabControlClick(object sender, EventArgs e)
        {
            if (e is MouseEventArgs args && args.Button == MouseButtons.Middle)
            {
                var tabControl = sender as TabControl;
                for (int i = 0; i < tabControl.TabCount; i++)
                {
                    if (tabControl.GetTabRect(i).Contains(args.X, args.Y))
                    {
                        Debug.WriteLine($"Closing tab {i} by MMB.");
                    }
                }
            }
        }

        private void OnMainFormLoad(object sender, EventArgs e)
        {
            _tabCollection.AddTab();
            _tabCollection.AddTab();
            _tabCollection.AddTab();
            _tabCollection.AddTab();
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnSelectAllClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.RichTextBox.SelectAll();
        }

        private void OnFormatSelectionClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.RichTextBox.SelectionFormatWithDialog();
        }

        private void OnFormatAllClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.RichTextBox.DoActionKeepingSelection(
                rtb => { rtb.SelectAll(); rtb.SelectionFormatWithDialog(); }
            );
        }

        private readonly TabCollection _tabCollection;

        private void OnCutClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.RichTextBox.Cut();
        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.RichTextBox.Copy();
        }

        private void OnPasteClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.RichTextBox.Paste();
        }

        private void OnUndoClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.RichTextBox.Undo();
        }

        private void OnRedoClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.RichTextBox.Redo();
        }
    }
}
