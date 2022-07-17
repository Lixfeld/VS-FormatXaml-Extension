using FormatXamlExtension.Classes;
using FormatXamlExtension.Configuration;
using Xunit;

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
            VSOptions vsOptions = new VSOptions(LineEnding.LF);
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
    }
}