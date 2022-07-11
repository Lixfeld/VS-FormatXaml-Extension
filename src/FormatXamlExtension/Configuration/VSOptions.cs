using FormatXamlExtension.Classes;

namespace FormatXamlExtension.Configuration
{
    public class VSOptions
    {
        public bool ExecuteOnSave { get; }

        public string FileExtensions { get; }

        public LineEnding LineEnding { get; }

        /// <summary>
        /// All options from DialogPage
        /// </summary>
        public VSOptions(bool executeOnSave, string fileExtensions, LineEnding lineEnding)
        {
            ExecuteOnSave = executeOnSave;
            FileExtensions = fileExtensions;
            LineEnding = lineEnding;
        }

        /// <summary>
        /// Only for Tests
        /// </summary>
        public VSOptions(LineEnding lineEnding)
        {
            LineEnding = lineEnding;
        }
    }
}