using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static FormatXaml.Constants;

namespace FormatXaml
{
    public class XamlLine
    {
        private int startIndex = 0;

        private readonly string escapedLine;

        public string Line { get; }
        public string RawLine { get; }

        public XamlLine(string rawLine)
        {
            if (rawLine == null)
                throw new ArgumentNullException(nameof(rawLine));

            Line = rawLine.Trim();
            RawLine = rawLine;

            escapedLine = CreateEscapedLine();
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
            else if (startIndex >= Line.Length)
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
                    int symbolIndex = escapedLine.IndexOf(xamlSymbol, startIndex, StringComparison.InvariantCulture);
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
                    var minSymbol = symbols.First(x => x.index == minIndex);

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

        /// <summary>
        /// Currently only necessary for Avalonia Child Selectors.
        /// Example: Selector="StackPanel > Button"
        /// </summary>
        private string CreateEscapedLine()
        {
            Match match = Regex.Match(Line, @"Selector\s*=\s*['""].*?(>).*?['""]");
            if (match.Success)
            {
                int symbolIndex = match.Groups[1].Index;

                // Replace the ">" character in the selector with any other single character
                char[] charArray = Line.ToCharArray();
                charArray[symbolIndex] = 'x';
                return new string(charArray);
            }

            return Line;
        }
    }
}