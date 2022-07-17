using FormatXamlExtension.Classes;
using FormatXamlExtension.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using Xunit;
using static FormatXamlExtension.Classes.Constants;

namespace FormatXamlExtension.Test
{
    public static class FileComparer
    {
        private const string TestFilesDirectory = "TestFiles\\";

        public static void Verify(VSOptions vsOptions = null, int indentSize = DefaultIndentSize, [CallerMemberName] string fileName = "")
        {
            if (vsOptions == null)
                vsOptions = new VSOptions(LineEnding.Auto);

            string testFileName = TestFilesDirectory + fileName + ".test";
            string expectedFileName = TestFilesDirectory + fileName + ".expected";

            string testText = File.ReadAllText(testFileName);
            string expectedText = File.ReadAllText(expectedFileName);

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