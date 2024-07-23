namespace FormatXaml
{
    public class XamlFormatterOptions
    {
        public LineEnding LineEnding { get; }

        public CommentIndentation CommentIndentation { get; }

        public WhitespaceBeforeEmptyTag WhitespaceBeforeEmptyTag { get; }

        public XamlFormatterOptions(LineEnding lineEnding, CommentIndentation commentIndentation, WhitespaceBeforeEmptyTag whitespaceBeforeEmptyTag)
        {
            LineEnding = lineEnding;
            CommentIndentation = commentIndentation;
            WhitespaceBeforeEmptyTag = whitespaceBeforeEmptyTag;
        }
    }
}
