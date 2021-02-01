#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadPlus
{
    class Tab
    {
        public event NonNullableEventHandler UnsavedContentChanged;

        public TabPage TabPage { get; }

#nullable disable
        public RichTextBox RichTextBox { get => TabPage.Controls[0] as RichTextBox; }

        public Tab(TabPage tabPage, string filePath)
        {
            TabPage = tabPage;
            FilePath = filePath;

            RichTextBox.TextChanged += OnRtbTextChanged;
        }
#nullable enable

        public void SaveFromSaveAll()
        {
            if (FilePath != null || UnsavedContent)
            {
                TrySave();
            }
        }

        public bool TrySave()
        {
            if (FilePath == null)
            {
                return TrySaveAs();
            }

            try
            {
                RichTextBox.SaveFile(FilePath, Utilities.FileExtensionToRichTextBoxStreamType(Path.GetExtension(FilePath)));
                UnsavedContent = false;
                return true;
            }
            catch (IOException e)
            {
                Debug.WriteLine($"[{e.GetType()}] {e.Message}");
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool TrySaveAs()
        {
            var dialog = new SaveFileDialog
            {
                FileName = FilePath == null ? string.Empty : Path.GetFileName(FilePath),
                Filter = Program.FormatsFilter
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = dialog.FileName;
                return TrySave();
            }
            return false;
        }

        public string GetMainFormTitle() => $"{Name}{(UnsavedContent ? " •" : "")} - Notepad+";

        public bool TryClose(Action<TabPage> activateTabPage)
        {
            if (!UnsavedContent)
            {
                return true;
            }

            activateTabPage.Invoke(TabPage);

            bool untitledFile = FilePath == null;
            string fileName = untitledFile ? "New file" : Name;

            var dialogResult = MessageBox.Show($"{fileName} has been modified, save changes?",
                "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            switch (dialogResult)
            {
                case DialogResult.Yes:
                {
                    return TrySave();
                }
                case DialogResult.No:
                {
                    return true;
                }
                case DialogResult.Cancel:
                {
                    return false;
                }
            }

            return false;
        }

        private void OnRtbTextChanged(object? sender, EventArgs e)
        {
            UnsavedContent = true;
        }

        private void UpdateDisplayedName()
        {
            TabPage.Text = Name + (UnsavedContent ? " •" : "");
        }

        private string Name
        {
            get => _name;
            set
            {
                _name = value;
                UpdateDisplayedName();
            }
        }

        public string? FilePath
        {
            get => _filePath;
            private set
            {
                Name = value == null ? "untitled" : Path.GetFileName(value);
                _filePath = value;
            }
        }

        public bool UnsavedContent
        {
            get => _unsavedContent;
            private set
            {
                bool changed = _unsavedContent != value;
                _unsavedContent = value;
                if (changed)
                {
                    UnsavedContentChanged.Invoke(this, EventArgs.Empty);
                    UpdateDisplayedName();
                }
            }
        }

        private string _name;
        private string? _filePath;
        private bool _unsavedContent = false;
    }
}
