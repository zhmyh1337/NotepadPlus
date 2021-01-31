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
        public TabCollection(TabControl tabControl, ContextMenuStrip rtbContextMenuStrip)
        {
            _tabControl = tabControl;
            _rtbContextMenuStrip = rtbContextMenuStrip;
        }

        public void ForEach(Action<Tab> action) => _tabs.ForEach(action);
        
        public Tab ActiveTab
        {
            get => _tabControl.SelectedIndex == -1 ? null : _tabs[_tabControl.SelectedIndex];
        }

        public void AddTab(string filePath = null)
        {
            _tabControl.TabPages.Add((string)null);
            var tabPage = _tabControl.TabPages[_tabControl.TabCount - 1];
            var richTextBox = new RichTextBox { Dock = DockStyle.Fill };
            richTextBox.ContextMenuStrip = _rtbContextMenuStrip;
            tabPage.Controls.Add(richTextBox);

            _tabs.Add(new Tab(tabPage, filePath));
        }

        private readonly List<Tab> _tabs = new List<Tab>();
        private readonly TabControl _tabControl;
        private readonly ContextMenuStrip _rtbContextMenuStrip;
    }
}
