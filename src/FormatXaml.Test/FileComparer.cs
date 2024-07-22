using FormatXaml;
using FormatXaml.Configuration;
using System.Runtime.CompilerServices;
using static FormatXaml.Constants;

namespace FormatXamlExtension.Test
{
    public static class FileComparer
    {
        private const string TestFilesDirectory = "TestFiles\\";

        public static void Verify(VSOptions? vsOptions = null, int indentSize = DefaultIndentSize, [CallerMemberName] string fileName = "")
        {
            if (vsOptions == null)
            {
                // Default options
                vsOptions = new VSOptions();
            }

            string testFileName = TestFilesDirectory + fileName + ".test";
            string expectedFileName = TestFilesDirectory + fileName + ".expected";

            string testText = File.ReadAllText(testFileName);
            string expectedText = File.ReadAllText(expectedFileName);

            // Formatting
            XamlText xamlText = new XamlText(testText);
            XamlFormatter xamlFormatter = new XamlFormatter(indentSize, vsOptions);
            string actualText = xamlFormatter.Format(xamlText);

#if DEBUG
            string actualFileName = TestFilesDirectory + fileName + ".actual";
            File.WriteAllText(actualFileName, actualText);
#endif

            Assert.Equal(expectedText, actualText);
        }
    }
}