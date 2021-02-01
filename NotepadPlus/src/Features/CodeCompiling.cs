#nullable enable

using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace NotepadPlus
{
    static class CodeCompiling
    {
        public static void CompileTab(Tab tab)
        {
            if (!tab.TrySave() || tab.FilePath == null)
            {
                return;
            }
            // tab.FilePath is not null now.

            if (Program.Settings.CompilingCompilerPath == null ||
                Program.Settings.CompilingCompilerPath.Length == 0)
            {
                MessageBox.Show("You haven't specified the compiler path in the settings yet.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = Program.Settings.CompilingCompilerPath,
                    Arguments = tab.FilePath,
                    StandardOutputEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage),
                    StandardErrorEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage),
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                Process process = new Process { StartInfo = startInfo };
                process.Start();
                process.WaitForExit();

                bool readFromStdout = Program.Settings.CompilingRadiobutton == "_compilationRedirectStdoutRadioButton";
                string resultText = readFromStdout ? process.StandardOutput.ReadToEnd() : process.StandardError.ReadToEnd();

                bool success = process.ExitCode == 0;
                string caption = $"{(success ? "Success" : "Error")}. Exit code: {process.ExitCode}.";

                MessageBox.Show(resultText, caption, MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (SystemException e)
            {
                Debug.WriteLine($"[{e.GetType()}] {e.Message} (in compilation).");
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
