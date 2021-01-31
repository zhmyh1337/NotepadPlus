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
    class TabCollection
    {
        public event MainFormTitleUpdatingEventHandler MainFormTitleUpdating;

        public TabCollection(TabControl tabControl, ContextMenuStrip rtbContextMenuStrip,
            MainFormTitleUpdatingEventHandler mainFormTitleUpdatingEventHandler)
        {
            _tabControl = tabControl;
            _rtbContextMenuStrip = rtbContextMenuStrip;

            MainFormTitleUpdating += mainFormTitleUpdatingEventHandler;
            _tabControl.Click += OnTabControlClick;
        }

        public void ForEach(Action<Tab> action) => _tabs.ForEach(action);
        
        public Tab? ActiveTab
        {
            get => _tabControl.SelectedIndex == -1 ? null : _tabs[_tabControl.SelectedIndex];
        }

        public void AddTab(string? filePath = null, bool activate = false)
        {
            var richTextBox = new RichTextBox { Dock = DockStyle.Fill };
            richTextBox.ContextMenuStrip = _rtbContextMenuStrip;

            // Loading.
            if (filePath != null)
            {
                try
                {
                    richTextBox.LoadFile(filePath, Utilities.FileExtensionToRichTextBoxStreamType(Path.GetExtension(filePath)));
                }
                catch (IOException e)
                {
                    Debug.WriteLine($"[{e.GetType()}] {e.Message}");
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    richTextBox.Dispose();
                    return;
                }
            }

            _tabControl.TabPages.Add((string?)null);
            var newTabPage = _tabControl.TabPages[_tabControl.TabCount - 1];
            newTabPage.Controls.Add(richTextBox);

            var newTab = new Tab(newTabPage, filePath);
            newTab.UnsavedContentChanged += OnTabUnsavedContentChanged;
            _tabs.Add(newTab);

            if (activate)
            {
                _tabControl.SelectedTab = newTabPage;
            }
        }

        private void OnTabControlClick(object? sender, EventArgs e)
        {
            if (e is MouseEventArgs args && args.Button == MouseButtons.Middle)
            {
                for (int i = 0; i < _tabControl.TabCount; i++)
                {
                    if (_tabControl.GetTabRect(i).Contains(args.X, args.Y))
                    {
                        Debug.WriteLine($"Closing tab {i} by MMB.");
                    }
                }
            }
        }

        private void OnTabUnsavedContentChanged(object sender, EventArgs e)
        {
            if (sender == ActiveTab)
            {
                MainFormTitleUpdating.Invoke(this, new MainFormTitleUpdatingEventArgs(ActiveTab.GetMainFormTitle()));
            }
        }

        private readonly List<Tab> _tabs = new List<Tab>();
        private readonly TabControl _tabControl;
        private readonly ContextMenuStrip _rtbContextMenuStrip;
    }
}
