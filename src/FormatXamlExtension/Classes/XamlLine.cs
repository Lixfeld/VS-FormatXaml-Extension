using System;
using System.Collections.Generic;
using System.Linq;
using static FormatXamlExtension.Classes.Constants;

namespace FormatXamlExtension.Classes
{
    public class XamlLine
    {
        private int startIndex = 0;

        public string Line { get; }
        public string RawLine { get; }

        public XamlLine(string rawLine)
        {
            Line = rawLine.Trim();
            RawLine = rawLine;
        }

        public bool TryGetNextSymbol(out string symbol, out int index)
        {
            index = -1;
            symbol = null;

            if (string.IsNullOrEmpty(Line) || startIndex == -1)
            {
                // Only whitespaces or no symbols found before
                return false;
            }
            else if (startIndex >= Line.Length - 1)
            {
                // End of line (max index)
                return false;
            }
            else
            {
                foreach (string xamlSymbol in xamlSymbols)
                {
                    int symbolIndex = Line.IndexOf(xamlSymbol, startIndex);
                    if (symbolIndex >= 0)
                    {
                        index = symbolIndex;
                        symbol = xamlSymbol;

                        // Set startIndex for next symbol
                        startIndex = symbolIndex + symbol.Length;
                        return true;
                    }
                }
                startIndex = -1;
                return false;
            }
        }

        /// <summary>
        /// Order is IMPORTANT!
        /// Symbols which are a substring of another symbol must be at the end.
        /// </summary>
        private static readonly string[] xamlSymbols = new string[]
        {
            OpenPITag,
            ClosePITag,
            OpenCommentTag,
            CloseCommentTag,
            OpenEndTag,
            CloseEmptyTag,
            OpenTag,
            CloseTag
        };
    }
}