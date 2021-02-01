#nullable enable

using System;
using System.Data;
using System.Linq;
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
                x => x.Name == Program.Settings.AutosaveRadiobutton).ToList().ForEach(x => x.Checked = true);

            _autologgingPanel.Controls.OfType<RadioButton>().Where(
                x => x.Name == Program.Settings.AutologgingRadiobutton).ToList().ForEach(x => x.Checked = true);
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
            Program.Settings.AutosaveRadiobutton = _autosavePanel.Controls.OfType<RadioButton>().Where(
                x => x.Checked == true).FirstOrDefault()?.Name;

            Program.Settings.AutologgingRadiobutton = _autologgingPanel.Controls.OfType<RadioButton>().Where(
                x => x.Checked == true).FirstOrDefault()?.Name;

            Program.Settings.Save();
            Program.Settings.Apply();
        }
    }
}
