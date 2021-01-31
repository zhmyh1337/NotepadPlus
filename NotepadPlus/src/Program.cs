using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            new MainForm().Show();
            Application.Run();
        }

        public const string FormatsFilter = "All Files (*.*)|*.*|Plain Text (*.txt)|*.txt|Rich text (*.rtf)|*.rtf|C# (*.cs)|*.cs";
    }
}
