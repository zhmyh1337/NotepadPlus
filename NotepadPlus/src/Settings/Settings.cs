#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadPlus
{
    class Settings
    {
        public event EventHandler? AutosaveTimerTick;
        public event EventHandler? AutologgingTimerTick;

        public string? AutosaveRadiobutton { get; set; }
        public string? AutologgingRadiobutton { get; set; }
        public string? CompilingRadiobutton { get; set; }
        public string? CompilingCompilerPath { get; set; }

        private readonly Timer _autosaveTimer = new Timer();
        private readonly Timer _autologgingTimer = new Timer();

        public StringCollection LastOpenedTabs { get; set; }

        public Settings()
        {
            _autosaveTimer.Tick += (sender, e) => AutosaveTimerTick?.Invoke(sender, e);
            _autologgingTimer.Tick += (sender, e) => AutologgingTimerTick?.Invoke(sender, e);

            LastOpenedTabs = Properties.Settings.Default.UnclosedTabs ?? new StringCollection();
        }

        public void SaveLastOpenedTabs()
        {
            Properties.Settings.Default.UnclosedTabs = LastOpenedTabs;

            Properties.Settings.Default.Save();
        }

        public void Load()
        {
            AutosaveRadiobutton = Properties.Settings.Default.OptionsAutosaveRadiobutton;
            AutologgingRadiobutton = Properties.Settings.Default.OptionsAutologgingRadiobutton;
            CompilingRadiobutton = Properties.Settings.Default.OptionsCompilingRadiobutton;
            CompilingCompilerPath = Properties.Settings.Default.OptionsCompilingCompilerPath;
        }

        public void Save()
        {
            Properties.Settings.Default.OptionsAutosaveRadiobutton = AutosaveRadiobutton;
            Properties.Settings.Default.OptionsAutologgingRadiobutton = AutologgingRadiobutton;
            Properties.Settings.Default.OptionsCompilingRadiobutton = CompilingRadiobutton;
            Properties.Settings.Default.OptionsCompilingCompilerPath = CompilingCompilerPath;

            Properties.Settings.Default.Save();
        }

        public void Apply()
        {
            ApplyAutosave();
            ApplyAutologging();
        }

        private void ApplyAutosave()
        {
            _autosaveTimer.Stop();

            var interval = AutosaveRadiobutton switch
            {
                "_autosaveRadioButton2" => 1 * 60 * 1000,
                "_autosaveRadioButton3" => 5 * 60 * 1000,
                "_autosaveRadioButton4" => 10 * 60 * 1000,
                "_autosaveRadioButton5" => 30 * 60 * 1000,
                _ => -1
            };

            if (interval != -1)
            {
                _autosaveTimer.Interval = interval;
                _autosaveTimer.Start();
            }
        }

        private void ApplyAutologging()
        {
            _autologgingTimer.Stop();

            var interval = AutologgingRadiobutton switch
            {
                "_autologgingRadioButton2" => 1 * 60 * 1000,
                "_autologgingRadioButton3" => 5 * 60 * 1000,
                "_autologgingRadioButton4" => 10 * 60 * 1000,
                "_autologgingRadioButton5" => 30 * 60 * 1000,
                _ => -1
            };

            if (interval != -1)
            {
                _autologgingTimer.Interval = interval;
                _autologgingTimer.Start();
            }
        }
    }
}
