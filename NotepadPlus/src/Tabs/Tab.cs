using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadPlus
{
    class Tab
    {
        public Tab(string name, string filePath, TabPage tabPage)
        {
            _name = name;
            _filePath = filePath;
            _tabPage = tabPage;
        }

        public RichTextBox RichTextBox { get => _tabPage.Controls[0] as RichTextBox; }

        private string _name;
        private string _filePath;
        private readonly TabPage _tabPage;
    }
}
