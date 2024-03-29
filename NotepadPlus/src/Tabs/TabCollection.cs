﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NotepadPlus
{
    /// <summary>
    /// Container for <see cref="Tab"/>s.
    /// </summary>
    class TabCollection
    {
        /// <summary>
        /// To update MainForm title (for example, when changing tabs, creating unsaved content).
        /// </summary>
        public event MainFormTitleUpdatingEventHandler MainFormTitleUpdating;

        /// <summary>
        /// Correct <see cref="TabControl.SelectedIndexChanged"/> event.
        /// There is a bug, which won't invoke the event when we added the first tab.
        /// </summary>
        private event EventHandler FixedSelectedIndexChanged;

        public TabCollection(TabControl tabControl, ContextMenuStrip rtbContextMenuStrip,
            ToolStripMenuItem versionHistoryToolStripMenuItem,
            MainFormTitleUpdatingEventHandler mainFormTitleUpdatingEventHandler)
        {
            _tabControl = tabControl;
            _rtbContextMenuStrip = rtbContextMenuStrip;

            MainFormTitleUpdating += mainFormTitleUpdatingEventHandler;
            FixedSelectedIndexChanged += MainFormTitleUpdate;
            FixedSelectedIndexChanged += (sender, e) =>
            {
                if (ActiveTab != null)
                {
                    Autologging.UpdateLogsDropDownMenu(versionHistoryToolStripMenuItem, ActiveTab);
                }
            };

            _tabControl.Click += OnTabControlClick;
            _tabControl.SelectedIndexChanged += (sender, e) => FixedSelectedIndexChanged.Invoke(sender, e);
        }

        public bool Empty { get => !_tabs.Any(); }

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
                catch (SystemException e)
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

        public (bool tabWasClosed, bool lastTabWasClosed, string? tabPath) TryCloseTab(Tab tab)
        {
            if (tab.TryClose(tabPage => _tabControl.SelectedTab = tabPage))
            {
                _tabs.Remove(tab);
                _tabControl.TabPages.Remove(tab.TabPage);
                if (_tabs.Count == 0)
                {
                    AddTab();
                    return (true, true, tab.FilePath);
                }
                return (true, false, tab.FilePath);
            }
            return (false, false, tab.FilePath);
        }

        /// <summary>
        /// Closes all tabs (with confirmation if unsaved).
        /// </summary>
        /// <returns>Whether all tabs have been closed.</returns>
        public bool CloseAllTabs(List<string>? tabPaths = null)
        {
            bool closedAllTabs = true;

            for (int tabIndex = 0; tabIndex < _tabs.Count; tabIndex++)
            {
                var (tabWasClosed, lastTabWasClosed, tabPath) = TryCloseTab(_tabs[tabIndex]);
                if (tabWasClosed == true && lastTabWasClosed == false)
                {
                    tabIndex--;
                }
                closedAllTabs &= tabWasClosed;

                if (tabPath != null)
                {
                    tabPaths?.Add(tabPath);
                }
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
