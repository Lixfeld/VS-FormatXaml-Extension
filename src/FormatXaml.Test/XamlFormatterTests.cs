using FormatXaml;
using FormatXaml.Configuration;

namespace FormatXamlExtension.Test
{
    public class XamlFormatterTests
    {
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
            VSOptions vsOptions = new VSOptions(lineEnding: LineEnding.LF);
            FileComparer.Verify(vsOptions);
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
        public void IndenSizeTwo() => FileComparer.Verify(indentSize: 2);

        [Fact]
        public void MultiLineCommentsSameIndent()
        {
            VSOptions vsOptions = new VSOptions(commentIndentation: CommentIndentation.Same);
            FileComparer.Verify(vsOptions);
        }

        [Fact]
        public void MultiLineCommentsExtraIndent()
        {
            VSOptions vsOptions = new VSOptions(commentIndentation: CommentIndentation.Extra);
            FileComparer.Verify(vsOptions);
        }

        [Fact]
        public void NoSpaceBeforeEmptyTag()
        {
            VSOptions vsOptions = new VSOptions(whitespaceBeforeEmptyTag: WhitespaceBeforeEmptyTag.Zero);
            FileComparer.Verify(vsOptions);
        }

        [Fact]
        public void OneSpaceBeforeEmptyTag()
        {
            VSOptions vsOptions = new VSOptions(whitespaceBeforeEmptyTag: WhitespaceBeforeEmptyTag.One);
            FileComparer.Verify(vsOptions);
        }

        [Fact]
        public void AvaloniaChildSelector() => FileComparer.Verify();
    }
}