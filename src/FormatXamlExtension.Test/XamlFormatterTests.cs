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
    }
}