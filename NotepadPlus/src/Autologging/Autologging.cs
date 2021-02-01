#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadPlus
{
    static class Autologging
    {
        static Autologging()
        {
            Directory.CreateDirectory(_autologgingDir);
        }

        private static string? SearchForStoringPath(string pathInfo)
        {
            if (!Directory.Exists(_autologgingDir))
            {
                return null;
            }
            foreach (var dir in new DirectoryInfo(_autologgingDir).EnumerateDirectories())
            {
                try
                {
                    var pathInfoFilePath = Path.Combine(dir.FullName, PathInfoFileName);
                    if (File.Exists(pathInfoFilePath) && File.ReadAllText(pathInfoFilePath) == pathInfo)
                    {
                        return dir.FullName;
                    }
                }
                catch (SystemException e)
                {
                    Debug.WriteLine($"[{e.GetType()}] {e.Message} (in SearchForPath).");
                }
            }
            return null;
        }

        private static string CreateStoringPath(string pathInfo)
        {
            string path;
            do
            {
                path = Path.Combine(_autologgingDir, Path.GetRandomFileName());
            } while (Directory.Exists(path));

            Directory.CreateDirectory(path);
            File.WriteAllText(Path.Combine(path, PathInfoFileName), pathInfo);

            return path;
        }

        public static void LogTab(Tab tab)
        {
            var pathInfo = tab.FilePath ?? tab.Name;
            var pathToStore = SearchForStoringPath(pathInfo) ?? CreateStoringPath(pathInfo);

            var fileName = $"{tab.Name}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";
            tab.SilentSave(Path.Combine(pathToStore, fileName));
        }

        public static void UpdateLogsDropDownMenu(ToolStripMenuItem parentItem, Tab tab)
        {
            parentItem.DropDownItems.Clear();

            var allTabLogPaths = GetAllTabLogPaths(tab);
            if (!allTabLogPaths.Any())
            {
                parentItem.Enabled = false;
                return;
            }

            // Reversing to keep the latest logs on top.
            var allTabLogPathsReversed = allTabLogPaths.Reverse();

            foreach (var logPath in allTabLogPathsReversed)
            {
                parentItem.DropDownItems.Add(Path.GetFileName(logPath)).Click +=
                    (sender, e) => tab.SilentLoad(logPath);
            }
            parentItem.Enabled = true;
        }

        private static IEnumerable<string> GetAllTabLogPaths(Tab tab)
        {
            var pathInfo = tab.FilePath ?? tab.Name;
            var storingPath = SearchForStoringPath(pathInfo);
            if (storingPath == null)
            {
                yield break;
            }
            foreach (var file in new DirectoryInfo(storingPath).EnumerateFiles())
            {
                if (file.Name != PathInfoFileName)
                {
                    yield return file.FullName;
                }
            }
        }

        private const string PathInfoFileName = "path";
        private static readonly string _autologgingDir = Path.Combine(Application.LocalUserAppDataPath, "Autologging");
    }
}
