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

        public VSOptions GetVSOptions()
        {
            return new VSOptions(ExecuteOnSave, FileExtensions, LineEnding);
        }
    }
}