using System;
using System.Windows.Forms;

namespace NotepadPlus
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings.Load();
            Settings.Apply();

            new MainForm().Show();
            Application.Run();
        }

        public static NotepadPlus.Settings Settings { get; } = new NotepadPlus.Settings();

        public const string FormatsFilter = "All Files (*.*)|*.*|Plain Text (*.txt)|*.txt|Rich text (*.rtf)|*.rtf|C# (*.cs)|*.cs";
    }
}
