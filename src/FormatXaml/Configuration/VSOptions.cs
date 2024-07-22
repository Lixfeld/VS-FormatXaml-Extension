namespace FormatXaml.Configuration
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

        /// <summary>
        /// Only for Tests
        /// </summary>
        public VSOptions(
            LineEnding lineEnding = LineEnding.Auto,
            CommentIndentation commentIndentation = CommentIndentation.Same,
            WhitespaceBeforeEmptyTag whitespaceBeforeEmptyTag = WhitespaceBeforeEmptyTag.Ignore)
        {
            LineEnding = lineEnding;
            CommentIndentation = commentIndentation;
            WhitespaceBeforeEmptyTag = whitespaceBeforeEmptyTag;
        }
    }
}