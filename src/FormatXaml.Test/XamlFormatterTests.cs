using FormatXaml;

namespace FormatXamlExtension.Test
{
    public class XamlFormatterTests
    {
        /// <summary>
        /// Only for Tests
        /// </summary>
        internal static XamlFormatterOptions TestOptions(
            int indentSize = Constants.DefaultIndentSize,
            LineEnding lineEnding = LineEnding.Auto,
            CommentIndentation commentIndentation = CommentIndentation.Same,
            WhitespaceBeforeEmptyTag whitespaceBeforeEmptyTag = WhitespaceBeforeEmptyTag.Ignore)
        {
            return new XamlFormatterOptions(indentSize, lineEnding, commentIndentation, whitespaceBeforeEmptyTag);
        }

        [Fact]
        public void NewLineAtEnd() => FileComparer.Verify();

        [Fact]
        public void NoNewLineAtEnd() => FileComparer.Verify();

        [Fact]
        public void AttributesStartsOnSecondLine() => FileComparer.Verify();

        [Fact]
        public void XmlVersion() => FileComparer.Verify();

        [Fact]
        public void NestedElements() => FileComparer.Verify();

        [Fact]
        public void LineEndingLF()
        {
            XamlFormatterOptions options = TestOptions(lineEnding: LineEnding.LF);
            FileComparer.Verify(options);
        }

        [Fact]
        public void SingleLineComments() => FileComparer.Verify();

        [Fact]
        public void CommentOnSameLine() => FileComparer.Verify();

        [Fact]
        public void AttributesOnMultipleLines() => FileComparer.Verify();

        [Fact]
        public void CloseAngleBracketOnNewLine() => FileComparer.Verify();

        [Fact]
        public void IndenSizeTwo()
        {
            XamlFormatterOptions options = TestOptions(indentSize: 2);
            FileComparer.Verify(options);
        }

        [Fact]
        public void MultiLineCommentsSameIndent()
        {
            XamlFormatterOptions options = TestOptions(commentIndentation: CommentIndentation.Same);
            FileComparer.Verify(options);
        }

        [Fact]
        public void MultiLineCommentsExtraIndent()
        {
            XamlFormatterOptions options = TestOptions(commentIndentation: CommentIndentation.Extra);
            FileComparer.Verify(options);
        }

        [Fact]
        public void NoSpaceBeforeEmptyTag()
        {
            XamlFormatterOptions options = TestOptions(whitespaceBeforeEmptyTag: WhitespaceBeforeEmptyTag.Zero);
            FileComparer.Verify(options);
        }

        [Fact]
        public void OneSpaceBeforeEmptyTag()
        {
            XamlFormatterOptions options = TestOptions(whitespaceBeforeEmptyTag: WhitespaceBeforeEmptyTag.One);
            FileComparer.Verify(options);
        }

        [Fact]
        public void AvaloniaChildSelector() => FileComparer.Verify();
    }
}