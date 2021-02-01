using System;
using System.Linq;

namespace NotepadPlus
{
    static class Autoformatting
    {
        private static void IterateBraceCounter(string s, ref int braceCounter,
            ref bool singlelineCommentOpened, ref bool multilineCommentOpened)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (i > 0 && s[i - 1] == '/' && s[i] == '/' && !multilineCommentOpened)
                {
                    singlelineCommentOpened = true;
                }
                if (i > 0 && s[i - 1] == '/' && s[i] == '*' && !singlelineCommentOpened)
                {
                    multilineCommentOpened = true;
                }
                if (i > 0 && s[i - 1] == '*' && s[i] == '/')
                {
                    multilineCommentOpened = false;
                }

                if (!singlelineCommentOpened && !multilineCommentOpened)
                {
                    if (s[i] == '{')
                    {
                        braceCounter++;
                    }
                    if (s[i] == '}')
                    {
                        braceCounter--;
                    }
                }
            }
        }

        public static string FormatStringAsCode(string s)
        {
            var lines = s.Split('\n');

            var braceCounter = 0;
            var singlelineCommentOpened = false;
            var multilineCommentOpened = false;

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
                var closingBracketCorrection = lines[i].FirstOrDefault() == '}' ? 1 : 0;
                lines[i] = lines[i].Insert(0,
                    new string(' ', TabulationSize * Math.Max(0, braceCounter - closingBracketCorrection))
                );

                IterateBraceCounter(lines[i], ref braceCounter, ref singlelineCommentOpened, ref multilineCommentOpened);
                singlelineCommentOpened = false;
            }

            return string.Join(Environment.NewLine, lines);
        }

        private const int TabulationSize = 4;
    }
}
