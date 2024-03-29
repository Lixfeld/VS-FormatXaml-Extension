using FormatXamlExtension.Classes;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace FormatXamlExtension.Configuration
{
    internal class OptionPage : DialogPage
    {
        // Menu
        public const string Category = "Format XAML";
        public const string Page = "General";

        // Groups
        public const string Execution = "Execution";
        public const string Indentation = "Indentation";
        public const string Miscellaneous = "Miscellaneous";

        [Category(Execution)]
        [DisplayName("Execute on save")]
        [Description("Formats the active xaml document when saving.\r\nDefault: true")]
        public bool ExecuteOnSave { get; set; } = true;

        [Category(Execution)]
        [DisplayName("File extensions")]
        [Description("Space separated list of all used file extensions.\r\nDefault: .xaml .axaml")]
        public string FileExtensions { get; set; } = ".xaml .axaml";

        [Category(Indentation)]
        [DisplayName("Comment Indentation")]
        [Description("Indentation level inside of multi-line comments.\r\nDefault: Same")]
        public CommentIndentation CommentIndentation { get; set; } = CommentIndentation.Same;

        [Category(Miscellaneous)]
        [DisplayName("Line Ending")]
        [Description("End of Line characters.\r\nDefault: Auto")]
        public LineEnding LineEnding { get; set; } = LineEnding.Auto;

        [Category(Miscellaneous)]
        [DisplayName("Whitespace before empty tag")]
        [Description("Number of whitespaces before a closing empty tag (/>).\r\nDefault: Ignore")]
        public WhitespaceBeforeEmptyTag WhitespaceBeforeEmptyTag { get; set; } = WhitespaceBeforeEmptyTag.Ignore;

        public VSOptions GetVSOptions()
        {
            return new VSOptions(
                ExecuteOnSave,
                FileExtensions,
                LineEnding,
                CommentIndentation,
                WhitespaceBeforeEmptyTag);
        }
    }
}