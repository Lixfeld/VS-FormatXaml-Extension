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
                List<(int index, string symbol)> symbols = new List<(int index, string symbol)>();

                // Use xamlSymbols order to avoid substrings
                foreach (string xamlSymbol in xamlSymbols)
                {
                    int symbolIndex = Line.IndexOf(xamlSymbol, startIndex);
                    if (symbolIndex >= 0)
                    {
                        symbols.Add((symbolIndex, xamlSymbol));
                    }
                }

                if (symbols.Count == 0)
                {
                    // No symbols found
                    startIndex = -1;
                    return false;
                }
                else
                {
                    // Get next symbol with lowest index
                    int minIndex = symbols.Min(x => x.index);
                    var minSymbol = symbols.Where(x => x.index == minIndex).First();

                    index = minSymbol.index;
                    symbol = minSymbol.symbol;

                    // Set startIndex for next symbol
                    startIndex = index + symbol.Length;
                    return true;
                }
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