using System;
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
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void OnOptionsFormLoad(object sender, EventArgs e)
        {
            _listbox.SelectedIndex = 0;
        }

        private void OnListboxSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var panel in Controls.OfType<Panel>())
            {
                panel.Visible = false;
            }

            switch (_listbox.SelectedIndex)
            {
                case 0:
                    _autosavePanel.Visible = true;
                    break;
                case 1:
                    _autologgingPanel.Visible = true;
                    break;
            }
        }
    }
}
