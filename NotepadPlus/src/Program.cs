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
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings.Load();
            Settings.Apply();

            new MainForm(onApplicationExitThread: (sender, e) => Settings.SaveLastOpenedTabs()).Show();
            Application.Run();
        }

        public static NotepadPlus.Settings Settings { get; } = new NotepadPlus.Settings();

        /// <summary>
        /// Filters for opening/saving files.
        /// </summary>
        public const string FormatsFilter = "All Files (*.*)|*.*|Plain Text (*.txt)|*.txt|Rich text (*.rtf)|*.rtf|C# (*.cs)|*.cs";
    }
}
