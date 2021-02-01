#nullable enable

using System;
using System.Windows.Forms;

namespace NotepadPlus
{
    static class Utilities
    {
        /// <summary>
        /// Does <see cref="Action"/> <paramref name="action"/>, which can modify <paramref name="richTextBox"/> selection.
        /// After that, restores the selection.
        /// </summary>
        public static void DoActionKeepingSelection(this RichTextBox richTextBox, Action<RichTextBox> action)
        {
            var oldSelectionStart = richTextBox.SelectionStart;
            var oldSelectionLength = richTextBox.SelectionLength;

            action.Invoke(richTextBox);

            richTextBox.Select(oldSelectionStart, oldSelectionLength);
        }

        /// <summary>
        /// Calls <see cref="FontDialog"/> and applies the result on <paramref name="richTextBox"/> selection.
        /// </summary>
        public static void SelectionFormatWithDialog(this RichTextBox richTextBox)
        {
            using var fontDialog = new FontDialog { Font = richTextBox.SelectionFont };
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SelectionFont = fontDialog.Font;
            }
        }

        public static RichTextBoxStreamType FileExtensionToRichTextBoxStreamType(string? ext)
        {
            return ext switch
            {
                ".rtf" => RichTextBoxStreamType.RichText,
                _ => RichTextBoxStreamType.PlainText
            };
        }
    }
}
