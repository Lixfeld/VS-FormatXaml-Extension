using FormatXamlExtension.Classes;

namespace FormatXamlExtension.Configuration
{
    public class VSOptions
    {
        public bool ExecuteOnSave { get; }

        public string FileExtensions { get; }

        public LineEnding LineEnding { get; }

        public CommentIndentation CommentIndentation { get; }

        /// <summary>
        /// All options from DialogPage
        /// </summary>
        public VSOptions(bool executeOnSave, string fileExtensions, LineEnding lineEnding, CommentIndentation commentIndentation)
        {
            ExecuteOnSave = executeOnSave;
            FileExtensions = fileExtensions;
            LineEnding = lineEnding;
            CommentIndentation = commentIndentation;
        }

        /// <summary>
        /// Only for Tests
        /// </summary>
        public VSOptions(LineEnding lineEnding = LineEnding.Auto, CommentIndentation commentIndentation = CommentIndentation.Same)
        {
            LineEnding = lineEnding;
            CommentIndentation = commentIndentation;
        }
    }
}