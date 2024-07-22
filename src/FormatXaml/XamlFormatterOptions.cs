namespace FormatXaml
{
    public class XamlFormatterOptions
    {
        public int IndentSize { get; }

        public LineEnding LineEnding { get; }

        public CommentIndentation CommentIndentation { get; }

        public WhitespaceBeforeEmptyTag WhitespaceBeforeEmptyTag { get; }

        public XamlFormatterOptions(int indentSize, LineEnding lineEnding, CommentIndentation commentIndentation, WhitespaceBeforeEmptyTag whitespaceBeforeEmptyTag)
        {
            IndentSize = indentSize;
            LineEnding = lineEnding;
            CommentIndentation = commentIndentation;
            WhitespaceBeforeEmptyTag = whitespaceBeforeEmptyTag;
        }
    }
}
