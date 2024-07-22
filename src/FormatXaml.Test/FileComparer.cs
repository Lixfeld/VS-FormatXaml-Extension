using FormatXaml;
using System.Runtime.CompilerServices;

namespace FormatXamlExtension.Test
{
    public static class FileComparer
    {
        private const string TestFilesDirectory = "TestFiles\\";

        public static void Verify(XamlFormatterOptions? options = null, [CallerMemberName] string fileName = "")
        {
            if (options == null)
            {
                // Default options
                options = XamlFormatterTests.TestOptions();
            }

            string testFileName = TestFilesDirectory + fileName + ".test";
            string expectedFileName = TestFilesDirectory + fileName + ".expected";

            string testText = File.ReadAllText(testFileName);
            string expectedText = File.ReadAllText(expectedFileName);

            // Formatting
            XamlText xamlText = new XamlText(testText);
            XamlFormatter xamlFormatter = new XamlFormatter(options);
            string actualText = xamlFormatter.Format(xamlText);

#if DEBUG
            string actualFileName = TestFilesDirectory + fileName + ".actual";
            File.WriteAllText(actualFileName, actualText);
#endif

            Assert.Equal(expectedText, actualText);
        }
    }
}