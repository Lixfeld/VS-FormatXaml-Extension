using CommandLine;
using System.Collections.Generic;

namespace FormatXaml.Tool
{
    internal class ToolOptions
    {
        [Value(0, MetaName = "file-or-directory", Required = true, HelpText = "Path of the file or directory.")]
        public string? FileOrDirectory { get; set; }

        [Option("verbose", Default = false, MetaValue = "BOOL", HelpText = "Show verbose messaging.")]
        public bool Verbose { get; set; }

        [Option("encoding", Default = "utf-8", MetaValue = "STRING", HelpText = "Encoding name (see .NET Encoding.GetEncoding Method).")]
        public string? EncodingName { get; set; }

        [Option("extensions", Default = new string[] { ".xaml", ".axaml" }, MetaValue = "LIST", HelpText = "Space separated list of file extensions.")]
        public IEnumerable<string>? Extensions { get; set; }

        /// <summary>
        /// Don't use "Default" in Option-Attribute
        /// If no value is specified (implicit 0) the EditorConfig is used.
        /// </summary>
        [Option("indent-size", MetaValue = "INT", HelpText = "(Default: EditorConfig) Number of spaces for indentation (Fallback: 4).")]
        public int IndentSize { get; set; }

        [Option("line-ending", Default = LineEnding.Auto, MetaValue = "ENUM", HelpText = "End of Line characters. Values: Auto, CRLF, LF")]
        public LineEnding LineEnding { get; set; }

        [Option("comment-indentation", Default = CommentIndentation.Same, MetaValue = "ENUM", HelpText = "Indentation level inside of multi-line comments. Values: Same, Extra")]
        public CommentIndentation CommentIndentation { get; set; }

        [Option("whitespace-before-empty-tag", Default = WhitespaceBeforeEmptyTag.Ignore, MetaValue = "ENUM", HelpText = "Number of whitespaces before a closing empty tag (/>). Values: Ignore, Zero, One")]
        public WhitespaceBeforeEmptyTag WhitespaceBeforeEmptyTag { get; set; }

        public XamlFormatterOptions CreateXamlFormatterOptions()
        {
            return new XamlFormatterOptions(LineEnding, CommentIndentation, WhitespaceBeforeEmptyTag);
        }
    }
}
