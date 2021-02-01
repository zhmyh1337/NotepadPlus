#nullable enable

using System;
using System.Data;
using System.Diagnostics;
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
            _totallyNotARickroll.VisibleChanged += (sender, e) => _totallyNotARickroll.Visible = true;
            _listbox.SelectedIndex = 0;

            LoadSettings();
        }

        private void LoadSettings()
        {
            _autosavePanel.Controls.OfType<RadioButton>().Where(
                x => x.Name == Program.Settings.AutosaveRadiobutton).ToList().ForEach(x => x.Checked = true);

            _autologgingPanel.Controls.OfType<RadioButton>().Where(
                x => x.Name == Program.Settings.AutologgingRadiobutton).ToList().ForEach(x => x.Checked = true);

            _compilationPanel.Controls.OfType<RadioButton>().Where(
                x => x.Name == Program.Settings.CompilingRadiobutton).ToList().ForEach(x => x.Checked = true);

            _compilerPathTextBox.Text = Program.Settings.CompilingCompilerPath;
        }

        private void SaveSettings()
        {
            Program.Settings.AutosaveRadiobutton = _autosavePanel.Controls.OfType<RadioButton>().Where(
                x => x.Checked == true).FirstOrDefault()?.Name;

            Program.Settings.AutologgingRadiobutton = _autologgingPanel.Controls.OfType<RadioButton>().Where(
                x => x.Checked == true).FirstOrDefault()?.Name;

            Program.Settings.CompilingRadiobutton = _compilationPanel.Controls.OfType<RadioButton>().Where(
                x => x.Checked == true).FirstOrDefault()?.Name;

            Program.Settings.CompilingCompilerPath = _compilerPathTextBox.Text;

            Program.Settings.Save();
            Program.Settings.Apply();
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
                case 2:
                    _compilationPanel.Visible = true;
                    break;
            }
        }

        private void OnOptionsFormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void OnTotallyNotARickrollDoubleClick(object sender, EventArgs args)
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://www.youtube.com/watch?v=DLzxrzFCyOs") { UseShellExecute = true });
            }
            catch (SystemException e)
            {
                Debug.WriteLine($"[{e.GetType()}] {e.Message}.");
            }
        }

        private void OnCompilerPathTextboxDoubleClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Executable file (*.exe)|*.exe"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _compilerPathTextBox.Text = dialog.FileName;
            }
        }
    }
}
