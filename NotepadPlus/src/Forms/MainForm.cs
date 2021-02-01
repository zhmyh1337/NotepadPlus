using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadPlus
{
    public partial class MainForm : Form
    {
        public event EventHandler ApplicationExitThread;
        private readonly EventHandler _onApplicationExitThread;

        private readonly TabCollection _tabCollection;

        public MainForm(EventHandler onApplicationExitThread)
        {
            InitializeComponent();

            _tabCollection = new TabCollection(_tabControl, _rtbContextMenuStrip,
                _versionHistoryToolStripMenuItem, OnMainFormTitleUpdating);

            _onApplicationExitThread = onApplicationExitThread;
            ApplicationExitThread += onApplicationExitThread;
        }

        private void OnMainFormTitleUpdating(object sender, MainFormTitleUpdatingEventArgs e)
        {
            Text = e.NewTitle;
        }

        private void OnMainFormLoad(object sender, EventArgs e)
        {
            foreach (var filePath in Program.Settings.LastOpenedTabs)
            {
                if (File.Exists(filePath))
                {
                    _tabCollection.AddTab(filePath);
                }
            }
            Program.Settings.LastOpenedTabs.Clear();
            if (_tabCollection.Empty)
            {
                _tabCollection.AddTab();
            }

            Program.Settings.AutosaveTimerTick += OnAutosaveTimerTick;
            Program.Settings.AutologgingTimerTick += OnAutologgingTimerTick;
        }

        private void OnMainFormClosing(object sender, FormClosingEventArgs e)
        {
            var filePaths = new List<string>();
            e.Cancel = !_tabCollection.CloseAllTabs(filePaths);
            // Still closing.
            if (!e.Cancel)
            {
                Program.Settings.LastOpenedTabs.Clear();
                Program.Settings.LastOpenedTabs.AddRange(filePaths.ToArray());
            }
        }

        private void OnMainFormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                ApplicationExitThread?.Invoke(this, EventArgs.Empty);
                Application.ExitThread();
            }
        }

        // "Exit" in "File" menu. Closes all windows.
        private void OnExitClick(object sender, EventArgs e)
        {
            // Closing all forms (can be cancelled).
            Application.OpenForms.Cast<Form>().ToList().ForEach(form => form.Close());
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

        private void OnSaveClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.TrySave();
        }

        private void OnSaveAsClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.TrySaveAs();
        }

        private void OnSaveAllClick(object sender, EventArgs e)
        {
            _tabCollection.ForEach(tab => tab.SaveFromSaveAll());
        }

        private void OnOpenClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = Program.FormatsFilter
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _tabCollection.AddTab(dialog.FileName);
            }
        }

        private void OnCloseFileClick(object sender, EventArgs e)
        {
            _tabCollection.TryCloseTab(_tabCollection.ActiveTab);
        }

        private void OnCloseAllFilesClick(object sender, EventArgs e)
        {
            _tabCollection.CloseAllTabs();
        }

        private void OnNewClick(object sender, EventArgs e)
        {
            _tabCollection.AddTab();
        }

        private void OnNewWindowClick(object sender, EventArgs e)
        {
            new MainForm(_onApplicationExitThread).Show();
        }

        private void OnCloseWindowClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnAutosaveTimerTick(object sender, EventArgs e)
        {
            Debug.WriteLine("Autosave timer tick.");
            _tabCollection.ForEach(tab => tab.SaveFromSaveAll());
        }

        private void OnAutologgingTimerTick(object sender, EventArgs e)
        {
            Debug.WriteLine("Autologging timer tick.");
            _tabCollection.ForEach(tab => Autologging.LogTab(tab));
            Autologging.UpdateLogsDropDownMenu(_versionHistoryToolStripMenuItem, _tabCollection.ActiveTab);
        }

        private void OnOptionsClick(object sender, EventArgs e)
        {
            new OptionsForm().ShowDialog();
        }

        private void OnCompileClick(object sender, EventArgs args)
        {
            CodeCompiling.CompileTab(_tabCollection.ActiveTab);
        }

        private void OnAutoformatAsCodeClick(object sender, EventArgs e)
        {
            _tabCollection.ActiveTab.FormatRtbTextAsCode();
        }
    }
}
