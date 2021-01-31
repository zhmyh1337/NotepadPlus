﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadPlus
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private RichTextBox ActiveRichTextBox
        {
            get => _tabControl.SelectedTab.Controls[0] as RichTextBox;
        }

        private void AppendTabPage(string name)
        {
            _tabControl.TabPages.Add(name);
            var appendedTab = _tabControl.TabPages[_tabControl.TabCount - 1];
            var richTextBox = new RichTextBox { Dock = DockStyle.Fill };
            richTextBox.ContextMenuStrip = _contextMenuStrip;
            appendedTab.Controls.Add(richTextBox);
        }

        private void OnTabControlClick(object sender, EventArgs e)
        {
            if (e is MouseEventArgs args && args.Button == MouseButtons.Middle)
            {
                var tabControl = sender as TabControl;
                for (int i = 0; i < tabControl.TabCount; i++)
                {
                    if (tabControl.GetTabRect(i).Contains(args.X, args.Y))
                    {
                        Debug.WriteLine($"Closing tab {i} by MMB.");
                    }
                }
            }
        }

        private void OnMainFormLoad(object sender, EventArgs e)
        {
            AppendTabPage("untitled");
            AppendTabPage("new 2");
            AppendTabPage("new 3");
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void RichTextBoxSelectionFormatWithDialog(RichTextBox richTextBox)
        {
            using (var fontDialog = new FontDialog())
            {
                fontDialog.Font = richTextBox.SelectionFont;
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox.SelectionFont = fontDialog.Font;
                }
            }
        }

        private void OnSelectAllClick(object sender, EventArgs e)
        {
            ActiveRichTextBox.SelectAll();
        }

        private void OnFormatSelectionClick(object sender, EventArgs e)
        {
            RichTextBoxSelectionFormatWithDialog(ActiveRichTextBox);
        }
    }
}