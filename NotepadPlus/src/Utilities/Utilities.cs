using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadPlus
{
    static class Utilities
    {
        public static void DoActionKeepingSelection(this RichTextBox richTextBox, Action<RichTextBox> action)
        {
            var oldSelectionStart = richTextBox.SelectionStart;
            var oldSelectionLength = richTextBox.SelectionLength;

            action.Invoke(richTextBox);

            richTextBox.Select(oldSelectionStart, oldSelectionLength);
        }

        public static void SelectionFormatWithDialog(this RichTextBox richTextBox)
        {
            using (var fontDialog = new FontDialog())
            {
                fontDialog.Font = richTextBox.SelectionFont;
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox.SelectionFont = fontDialog.Font;
                }
            }
        }
    }
}
