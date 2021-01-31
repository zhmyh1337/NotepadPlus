#nullable enable

using System;

namespace NotepadPlus
{
    public class MainFormTitleUpdatingEventArgs : EventArgs
    {
        public MainFormTitleUpdatingEventArgs(string newTitle)
        {
            NewTitle = newTitle;
        }

        public string NewTitle { get; }
    }

    public delegate void MainFormTitleUpdatingEventHandler(object? sender, MainFormTitleUpdatingEventArgs e);
}
