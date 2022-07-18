using FormatXamlExtension.Classes;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace FormatXamlExtension.Configuration
{
    internal class OptionPage : DialogPage
    {
        public const string Category = "Format Xaml";
        public const string SubCategory = "General";

        [Category(Category)]
        [DisplayName("Execute on save")]
        [Description("Formats the active xaml document when saving.\r\nDefault: true")]
        public bool ExecuteOnSave { get; set; } = true;

        [Category(Category)]
        [DisplayName("File extensions")]
        [Description("Space separated list of all used file extensions.\r\nDefault: .xaml .axaml")]
        public string FileExtensions { get; set; } = ".xaml .axaml";

        [Category(Category)]
        [DisplayName("Line Ending")]
        [Description("End of Line characters.\r\nDefault: Auto")]
        public LineEnding LineEnding { get; set; } = LineEnding.Auto;

        [Category(Category)]
        [DisplayName("Comment Indentation")]
        [Description("Indentation level inside of multi-line comments.\r\nDefault: Same")]
        public CommentIndentation CommentIndentation { get; set; } = CommentIndentation.Same;

        [Category(Category)]
        [DisplayName("Whitespace before empty tag")]
        [Description("Number of whitespaces before a closing empty tag (/>).\r\nDefault: Ignore")]
        public WhitespaceBeforeEmptyTag WhitespaceBeforeEmptyTag { get; set; } = WhitespaceBeforeEmptyTag.Ignore;

        public VSOptions GetVSOptions()
        {
            return new VSOptions(ExecuteOnSave, FileExtensions, LineEnding, CommentIndentation, WhitespaceBeforeEmptyTag);
        }
    }
}