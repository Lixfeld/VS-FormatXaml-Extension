using FormatXaml;

namespace FormatXamlExtension.Test
{
    public class XamlFormatterTests
    {
        /// <summary>
        /// Only for Tests
        /// </summary>
        internal static XamlFormatterOptions TestOptions(
            LineEnding lineEnding = LineEnding.Auto,
            CommentIndentation commentIndentation = CommentIndentation.Same,
            WhitespaceBeforeEmptyTag whitespaceBeforeEmptyTag = WhitespaceBeforeEmptyTag.Ignore)
        {
            return new XamlFormatterOptions(lineEnding, commentIndentation, whitespaceBeforeEmptyTag);
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
            FileComparer.Verify(options: options);
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
            FileComparer.Verify(indentSize: 2);
        }

        [Fact]
        public void MultiLineCommentsSameIndent()
        {
            XamlFormatterOptions options = TestOptions(commentIndentation: CommentIndentation.Same);
            FileComparer.Verify(options: options);
        }

        [Fact]
        public void MultiLineCommentsExtraIndent()
        {
            XamlFormatterOptions options = TestOptions(commentIndentation: CommentIndentation.Extra);
            FileComparer.Verify(options: options);
        }

        [Fact]
        public void NoSpaceBeforeEmptyTag()
        {
            XamlFormatterOptions options = TestOptions(whitespaceBeforeEmptyTag: WhitespaceBeforeEmptyTag.Zero);
            FileComparer.Verify(options: options);
        }

        [Fact]
        public void OneSpaceBeforeEmptyTag()
        {
            XamlFormatterOptions options = TestOptions(whitespaceBeforeEmptyTag: WhitespaceBeforeEmptyTag.One);
            FileComparer.Verify(options: options);
        }

        [Fact]
        public void AvaloniaChildSelector() => FileComparer.Verify();
    }
}