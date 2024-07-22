using FormatXaml;

namespace FormatXamlExtension.Configuration
{
    public class VSOptions
    {
        public bool ExecuteOnSave { get; }

        public string FileExtensions { get; }

        public LineEnding LineEnding { get; }

        public CommentIndentation CommentIndentation { get; }

        public WhitespaceBeforeEmptyTag WhitespaceBeforeEmptyTag { get; }

        public IndentationConfiguration Configuration { get; }

        public int CustomIndentSize { get; }

        /// <summary>
        /// All options from DialogPage
        /// </summary>
        public VSOptions(
            bool executeOnSave,
            string fileExtensions,
            LineEnding lineEnding,
            CommentIndentation commentIndentation,
            WhitespaceBeforeEmptyTag whitespaceBeforeEmptyTag,
            IndentationConfiguration configuration,
            int customIndentSize)
        {
            ExecuteOnSave = executeOnSave;
            FileExtensions = fileExtensions;
            LineEnding = lineEnding;
            CommentIndentation = commentIndentation;
            WhitespaceBeforeEmptyTag = whitespaceBeforeEmptyTag;
            Configuration = configuration;
            CustomIndentSize = customIndentSize;
        }

        public XamlFormatterOptions CreateXamlFormatterOptions(int indentSize)
        {
            return new XamlFormatterOptions(indentSize, LineEnding, CommentIndentation, WhitespaceBeforeEmptyTag);
        }
    }
}