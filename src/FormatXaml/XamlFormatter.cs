using FormatXaml.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FormatXaml
{
    public class XamlFormatter
    {
        private readonly int indentSize;
        private readonly VSOptions vsOptions;

        private int depth;
        private int attributeIndentation;

        private bool isComment;
        private string lastSymbolInText;

        public XamlFormatter(int indentSize, VSOptions vsOptions)
        {
            this.indentSize = indentSize;
            this.vsOptions = vsOptions;
        }

        private void ResetFields()
        {
            depth = 0;
            attributeIndentation = 0;
            isComment = false;
            lastSymbolInText = null;
        }

        public string Format(XamlText xamlText)
        {
            if (xamlText == null)
                throw new ArgumentNullException(nameof(xamlText));

            ResetFields();

            List<string> formattedLines = new List<string>();
            foreach (XamlLine xamlLine in xamlText.GetXamlLines())
            {
                int lineDepth = depth;
                string firstSymbol = null;
                string lastSymbolInLine = null;

                // Analyse current line
                while (xamlLine.TryGetNextSymbol(out string symbol, out int _))
                {
                    if (isComment && symbol != Constants.CloseCommentTag)
                    {
                        // Ignore all symbols inside the comment
                        continue;
                    }

                    if (firstSymbol == null)
                    {
                        // Set once
                        firstSymbol = symbol;
                    }

                    if (symbol == Constants.OpenCommentTag)
                    {
                        isComment = true;
                        if (vsOptions.CommentIndentation == CommentIndentation.Extra)
                        {
                            // Add extra indentation
                            lineDepth++;
                        }
                    }

                    if (symbol == Constants.CloseCommentTag)
                    {
                        isComment = false;
                        if (vsOptions.CommentIndentation == CommentIndentation.Extra)
                        {
                            // Remove extra indentation
                            lineDepth--;
                        }
                    }

                    if (symbol == Constants.CloseTag && lastSymbolInText == Constants.OpenEndTag)
                    {
                        lineDepth--;
                    }
                    else if (symbol == Constants.CloseTag)
                    {
                        lineDepth++;
                    }

                    lastSymbolInLine = symbol;
                    lastSymbolInText = symbol;
                }

                // Indentation for attributes
                if (lastSymbolInLine == Constants.OpenTag)
                {
                    SetAttributeIndentation(xamlLine, lastSymbolInLine);
                }

                // Format line
                formattedLines.Add(FormatLine(xamlLine, firstSymbol));

                // Set new line depth after formating current line
                depth = lineDepth;
            }

            string lineEnding = GetLineEnding(xamlText.LineEnding);
            string formattedText = GetFormattedText(formattedLines, lineEnding);
            return formattedText;
        }

        private string GetFormattedText(List<string> formattedLines, string lineEnding)
        {
            int whitespaceCount = GetWhitespaceCount();
            if (whitespaceCount != -1)
            {
                string whitespace = new string(' ', whitespaceCount);
                for (int i = 0; i < formattedLines.Count; i++)
                {
                    string line = formattedLines[i];
                    // Only change lines which ends with closing empty tag BUT also includes other characters
                    if (line.EndsWith(Constants.CloseEmptyTag, StringComparison.InvariantCulture) && line.Trim() != Constants.CloseEmptyTag)
                    {
                        // Remove closing empty tag before trimming and adding X number of whitespaces
                        string trimmedLine = line.Remove(line.Length - 2, 2).TrimEnd();
                        string newLine = trimmedLine + whitespace + Constants.CloseEmptyTag;

                        // Replace existing line
                        formattedLines[i] = newLine;
                    }
                }
            }

            return string.Join(lineEnding, formattedLines);
        }

        private void SetAttributeIndentation(XamlLine xamlLine, string lastSymbol)
        {
            int index = xamlLine.Line.LastIndexOf(lastSymbol, StringComparison.InvariantCulture);

            // Get offset from symbol to first attribute
            int offset = xamlLine.Line
                .Substring(index)
                .TakeWhile(x => !char.IsWhiteSpace(x))
                .Count() + 1; // + 1 whitespace as separator

            if (xamlLine.Line.Length >= offset)
            {
                attributeIndentation = depth * indentSize + offset;
            }
            else
            {
                attributeIndentation = (depth + 1) * indentSize;
            }
        }

        private string FormatLine(XamlLine xamlLine, string firstSymbol)
        {
            if (string.IsNullOrWhiteSpace(xamlLine.Line))
            {
                // No indentation for empty line
                return string.Empty;
            }

            if (firstSymbol == Constants.CloseTag)
            {
                return Indent(xamlLine.Line, attributeIndentation);
            }

            if (lastSymbolInText == Constants.OpenTag && firstSymbol != Constants.OpenTag
                || lastSymbolInText == Constants.CloseEmptyTag && firstSymbol == Constants.CloseEmptyTag)

            {
                // No Xaml Symbols => Attribute
                return Indent(xamlLine.Line, attributeIndentation);
            }

            if (vsOptions.CommentIndentation == CommentIndentation.Extra && xamlLine.Line == Constants.CloseCommentTag)
            {
                // Line contains only closing comment tag
                int closeCommentDepth = depth - 1;
                return IndentWithDepth(xamlLine.Line, closeCommentDepth);
            }

            int elementDepth = firstSymbol == Constants.OpenEndTag ? depth - 1 : depth;
            return IndentWithDepth(xamlLine.Line, elementDepth);
        }

        private string IndentWithDepth(string text, int depth)
        {
            return Indent(text, depth * indentSize);
        }

        private static string Indent(string text, int indentationSize)
        {
            string indendation = new string(' ', indentationSize);
            return indendation + text;
        }

        private string GetLineEnding(string xamlLineEnding)
        {
            switch (vsOptions.LineEnding)
            {
                case LineEnding.CRLF:
                    return Constants.WindowsLineEnding;

                case LineEnding.LF:
                    return Constants.UnixLineEnding;

                case LineEnding.Auto:
                default:
                    return xamlLineEnding;
            }
        }

        private int GetWhitespaceCount()
        {
            switch (vsOptions.WhitespaceBeforeEmptyTag)
            {
                case WhitespaceBeforeEmptyTag.Zero:
                    return 0;

                case WhitespaceBeforeEmptyTag.One:
                    return 1;

                case WhitespaceBeforeEmptyTag.Ignore:
                default:
                    return -1;
            }
        }
    }
}