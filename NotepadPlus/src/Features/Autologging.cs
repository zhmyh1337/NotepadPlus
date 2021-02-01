#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NotepadPlus
{
    /// <summary>
    /// This class performs auto-logging (backing up) files with timestamps.
    /// </summary>
    static class Autologging
    {
        static Autologging()
        {
            Directory.CreateDirectory(_autologgingDir);
        }

        /// <summary>
        /// Looks for the existing path where all versions of the file were saved.
        /// </summary>
        /// <param name="pathInfo">Path of the file.</param>
        /// <returns>Found path or null if not found.</returns>
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

        /// <summary>
        /// Creates a path where all versions of the file will be saved.
        /// </summary>
        /// <param name="pathInfo">Path of the file.</param>
        /// <returns>Created path.</returns>
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

        /// <summary>
        /// Does the backup copy of <paramref name="tab"/>'s associated file with a timestamp.
        /// </summary>
        public static void LogTab(Tab tab)
        {
            var pathInfo = tab.FilePath ?? tab.Name;
            var pathToStore = SearchForStoringPath(pathInfo) ?? CreateStoringPath(pathInfo);

            var fileName = $"{tab.Name}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";
            tab.SilentSave(Path.Combine(pathToStore, fileName));
        }

        /// <summary>
        /// Updates the dropdown menu, which contains all file's versions.
        /// </summary>
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

        /// <summary>
        /// Gets all the paths to the backups of <paramref name="tab"/>'s associated file.
        /// </summary>
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
