﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FormatXamlExtension.Classes
{
    public class XamlFormatter
    {
        private readonly int indentSize;

        private int depth;
        private int attributeIndentation;

        private string lastSymbolInText;

        public XamlFormatter(int indentSize)
        {
            this.indentSize = indentSize;
        }

        private void ResetFields()
        {
            depth = 0;
            attributeIndentation = 0;
            lastSymbolInText = null;
        }

        public string Format(XamlText xamlText)
        {
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
                    if (firstSymbol == null)
                    {
                        // Set once
                        firstSymbol = symbol;
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

            string formattedText = string.Join(xamlText.LineEnding, formattedLines);
            return formattedText;
        }

        private void SetAttributeIndentation(XamlLine xamlLine, string lastSymbol)
        {
            int index = xamlLine.Line.LastIndexOf(lastSymbol);

            // Get index of first attribute
            int offset = xamlLine.Line
                .Substring(index)
                .TakeWhile(x => !char.IsWhiteSpace(x))
                .Count();

            int attributeIndex = offset + 1; // 1 whitespace as separator
            if (xamlLine.Line.Length - 1 >= attributeIndex)
            {
                attributeIndentation = offset + 1; // 1 whitespace as separator
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

            if (lastSymbolInText == Constants.OpenTag && firstSymbol != Constants.OpenTag)
            {
                // No Xaml Symbols => Attribute
                return Indent(xamlLine.Line, attributeIndentation);
            }

            int elementDepth = firstSymbol == Constants.OpenEndTag ? depth - 1 : depth;
            return IndentWithDepth(xamlLine.Line, elementDepth);
        }

        private string IndentWithDepth(string text, int depth) => Indent(text, depth * indentSize);

        private string Indent(string text, int indentationSize)
        {
            string indendation = new string(' ', indentationSize);
            return indendation + text;
        }
    }
}