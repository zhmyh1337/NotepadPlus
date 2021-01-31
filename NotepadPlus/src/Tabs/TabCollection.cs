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

        /// <summary>
        /// Correct <see cref="TabControl.SelectedIndexChanged"/> event.
        /// There is a bug, which won't invoke the event when we added the first tab.
        /// </summary>
        private event EventHandler FixedSelectedIndexChanged;

        public TabCollection(TabControl tabControl, ContextMenuStrip rtbContextMenuStrip,
            MainFormTitleUpdatingEventHandler mainFormTitleUpdatingEventHandler)
        {
            _tabControl = tabControl;
            _rtbContextMenuStrip = rtbContextMenuStrip;

            MainFormTitleUpdating += mainFormTitleUpdatingEventHandler;
            _tabControl.Click += OnTabControlClick;
            _tabControl.SelectedIndexChanged += FixedSelectedIndexChanged;

            FixedSelectedIndexChanged += MainFormTitleUpdate;
        }

        public void ForEach(Action<Tab> action) => _tabs.ForEach(action);
        
        public Tab? ActiveTab
        {
            get => _tabControl.SelectedIndex == -1 ? null : _tabs[_tabControl.SelectedIndex];
        }

        public void AddTab(string? filePath = null, bool activate = true)
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

            // Watch the definition of FixedSelectedIndexChanged.
            if (_tabControl.TabCount == 1)
            {
                FixedSelectedIndexChanged.Invoke(_tabControl, EventArgs.Empty);
            }
        }

        public (bool tabWasClosed, bool lastTabWasClosed) TryCloseTab(Tab tab)
        {
            if (tab.TryClose(tabPage => _tabControl.SelectedTab = tabPage))
            {
                _tabs.Remove(tab);
                _tabControl.TabPages.Remove(tab.TabPage);
                if (_tabs.Count == 0)
                {
                    AddTab();
                    return (true, true);
                }
                return (true, false);
            }
            return (false, false);
        }

        /// <summary>
        /// Closes all tabs (with confirmation if unsaved).
        /// </summary>
        /// <returns>Whether all tabs have been closed.</returns>
        public bool CloseAllTabs()
        {
            bool closedAllTabs = true;

            for (int tabIndex = 0; tabIndex < _tabs.Count; tabIndex++)
            {
                var (tabWasClosed, lastTabWasClosed) = TryCloseTab(_tabs[tabIndex]);
                if (tabWasClosed == true && lastTabWasClosed == false)
                {
                    tabIndex--;
                }
                closedAllTabs &= tabWasClosed;
            }

            return closedAllTabs;
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
                        TryCloseTab(_tabs[i]);
                        break;
                    }
                }
            }
        }

        private void MainFormTitleUpdate(object? sender, EventArgs e)
        {
            if (ActiveTab != null)
            {
                MainFormTitleUpdating.Invoke(this, new MainFormTitleUpdatingEventArgs(ActiveTab.GetMainFormTitle()));
            }
        }

        private void OnTabUnsavedContentChanged(object sender, EventArgs e)
        {
            if (sender == ActiveTab)
            {
                MainFormTitleUpdate(sender, EventArgs.Empty);
            }
        }

        private readonly List<Tab> _tabs = new List<Tab>();
        private readonly TabControl _tabControl;
        private readonly ContextMenuStrip _rtbContextMenuStrip;
    }
}
