using NotepadPlus.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

            LoadSettings();
        }

        private void LoadSettings()
        {
            _autosavePanel.Controls.OfType<RadioButton>().Where(
                x => x.Name == Settings.Default.OptionsAutosaveRadiobutton).ToList().ForEach(x => x.Checked = true);

            _autologgingPanel.Controls.OfType<RadioButton>().Where(
                x => x.Name == Settings.Default.OptionsAutologgingRadiobutton).ToList().ForEach(x => x.Checked = true);
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

        private void OnOptionsFormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            Settings.Default.OptionsAutosaveRadiobutton = _autosavePanel.Controls.OfType<RadioButton>().Where(
                x => x.Checked == true).FirstOrDefault()?.Name;

            Settings.Default.OptionsAutologgingRadiobutton = _autologgingPanel.Controls.OfType<RadioButton>().Where(
                x => x.Checked == true).FirstOrDefault()?.Name;

            Settings.Default.Save();
        }
    }
}
