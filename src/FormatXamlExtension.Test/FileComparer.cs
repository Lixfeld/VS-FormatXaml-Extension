using FormatXamlExtension.Classes;
using System.IO;
using System.Runtime.CompilerServices;
using Xunit;
using static FormatXamlExtension.Classes.Constants;

namespace FormatXamlExtension.Test
{
    public static class FileComparer
    {
        private const string TestFilesDirectory = "TestFiles\\";

        public static void Verify(int indentSize = DefaultIndentSize, [CallerMemberName] string fileName = "")
        {
            string testFileName = TestFilesDirectory + fileName + ".test";
            string expectedFileName = TestFilesDirectory + fileName + ".expected";

            string testText = File.ReadAllText(testFileName);
            string expectedText = File.ReadAllText(expectedFileName);

            XamlText xamlText = new XamlText(testText);
            XamlFormatter xamlFormatter = new XamlFormatter(DefaultIndentSize);
            string actualText = xamlFormatter.Format(xamlText);

#if DEBUG
            string actualFileName = TestFilesDirectory + fileName + ".actual";
            File.WriteAllText(actualFileName, actualText);
#endif

            Assert.Equal(expectedText, actualText);
        }
    }
}