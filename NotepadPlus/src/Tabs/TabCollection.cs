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
    class TabCollection
    {
        public TabCollection(TabControl tabControl, ContextMenuStrip contextMenuStrip)
        {
            _tabControl = tabControl;
            _contextMenuStrip = contextMenuStrip;
        }

        public void AddTab()
        {
            AddTab("untitled", null);
        }

        public void AddTab(string filePath)
        {
            AddTab(Path.GetFileName(filePath), filePath);
        }

        public Tab ActiveTab
        {
            get => _tabControl.SelectedIndex == -1 ? null : _tabs[_tabControl.SelectedIndex];
        }

        private void AddTab(string name, string filePath)
        {
            _tabControl.TabPages.Add(name);
            var tabPage = _tabControl.TabPages[_tabControl.TabCount - 1];
            var richTextBox = new RichTextBox { Dock = DockStyle.Fill };
            richTextBox.ContextMenuStrip = _contextMenuStrip;
            tabPage.Controls.Add(richTextBox);

            _tabs.Add(new Tab(name, filePath, tabPage));
        }

        private readonly List<Tab> _tabs = new List<Tab>();
        private readonly TabControl _tabControl;
        private readonly ContextMenuStrip _contextMenuStrip;
    }
}
