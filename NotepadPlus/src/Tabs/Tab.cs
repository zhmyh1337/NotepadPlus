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
        public Tab(TabPage tabPage, string filePath)
        {
            _tabPage = tabPage;
            FilePath = filePath;
        }

        public RichTextBox RichTextBox { get => _tabPage.Controls[0] as RichTextBox; }

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
                _unsavedContent = false;
            }
            catch (IOException e)
            {
                Debug.WriteLine($"[{e.GetType()}] {e.Message}");
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveAs()
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = FilePath == null ? string.Empty : Path.GetFileName(FilePath);
            dialog.Filter = "All Files (*.*)|*.*|Plain Text (*.txt)|*.txt|Rich text (*.rtf)|*.rtf|C# (*.cs)|*.cs";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = dialog.FileName;
                Save();
            }
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

        private string FilePath
        {
            get => _filePath;
            set
            {
                Name = value == null ? "untitled" : Path.GetFileName(value);
                _filePath = value;
            }
        }

        private readonly TabPage _tabPage;
        private string _name;
        private string _filePath;
        private bool _unsavedContent = false;
    }
}
