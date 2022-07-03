using System;
using System.Collections.Generic;
using System.Linq;

namespace FormatXamlExtension.Classes
{
    public class XamlText
    {
        public string Text { get; }
        public string LineEnding { get; }

        public XamlText(string text)
        {
            Text = text;

            int lfIndex = text.IndexOf(Constants.UnixLineEnding);
            int crlfIndex = text.IndexOf(Constants.WindowsLineEnding);

            // Determine line ending by the first match
            if (crlfIndex == -1 || crlfIndex > lfIndex)
            {
                LineEnding = Constants.UnixLineEnding;
            }
            else
            {
                LineEnding = Constants.WindowsLineEnding;
            }
        }

        public XamlLine[] GetXamlLines()
        {
            // Split with multiple seperators because the file could have inconsistent line endings
            string[] lines = Text.Split(new[] { Constants.WindowsLineEnding, Constants.UnixLineEnding }, StringSplitOptions.None);
            return lines.Select(x => new XamlLine(x)).ToArray();
        }
    }
}