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
#nullable disable
        public event NonNullableEventHandler UnsavedContentChanged;

        public RichTextBox RichTextBox { get => _tabPage.Controls[0] as RichTextBox; }

        public Tab(TabPage tabPage, string filePath)
        {
            _tabPage = tabPage;
            FilePath = filePath;

            RichTextBox.TextChanged += OnRtbTextChanged;
        }
#nullable enable

        public void Save()
        {
            if (FilePath == null)
            {
                SaveAs();
                return;
            }

            try
            {
                RichTextBox.SaveFile(FilePath, Utilities.FileExtensionToRichTextBoxStreamType(Path.GetExtension(FilePath)));
                UnsavedContent = false;
            }
            catch (IOException e)
            {
                Debug.WriteLine($"[{e.GetType()}] {e.Message}");
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveAs()
        {
            var dialog = new SaveFileDialog
            {
                FileName = FilePath == null ? string.Empty : Path.GetFileName(FilePath),
                Filter = "All Files (*.*)|*.*|Plain Text (*.txt)|*.txt|Rich text (*.rtf)|*.rtf|C# (*.cs)|*.cs"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = dialog.FileName;
                Save();
            }
        }

        public string GetMainFormTitle() => $"{Name}{(UnsavedContent ? " •" : "")} - Notepad+";

        private void OnRtbTextChanged(object? sender, EventArgs e)
        {
            UnsavedContent = true;
        }

        private string Name
        {
            get => _name;
            set
            {
                _tabPage.Text = value;
                _name = value;
            }
        }

        private string? FilePath
        {
            get => _filePath;
            set
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
                    UnsavedContentChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private readonly TabPage _tabPage;
        private string _name;
        private string? _filePath;
        private bool _unsavedContent = false;
    }
}
