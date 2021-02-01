﻿#nullable enable

using System;
using System.Collections.Generic;
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

        private readonly Timer _autosaveTimer = new Timer();
        private readonly Timer _autologgingTimer = new Timer();

        public Settings()
        {
            _autosaveTimer.Tick += (sender, e) => AutosaveTimerTick?.Invoke(sender, e);
            _autologgingTimer.Tick += (sender, e) => AutologgingTimerTick?.Invoke(sender, e);
        }

        public void Load()
        {
            AutosaveRadiobutton = Properties.Settings.Default.OptionsAutosaveRadiobutton;
            AutologgingRadiobutton = Properties.Settings.Default.OptionsAutologgingRadiobutton;
        }

        public void Save()
        {
            Properties.Settings.Default.OptionsAutosaveRadiobutton = AutosaveRadiobutton;
            Properties.Settings.Default.OptionsAutologgingRadiobutton = AutologgingRadiobutton;

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