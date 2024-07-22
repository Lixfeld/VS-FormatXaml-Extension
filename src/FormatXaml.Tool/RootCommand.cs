using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FormatXaml.Tool
{
    internal class RootCommand
    {
        private readonly ToolOptions options;

        public RootCommand(ToolOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void Execute()
        {
            List<string> filepaths = new List<string>();
            if (File.Exists(options.FileOrDirectory))
            {
                filepaths.Add(options.FileOrDirectory);
            }

            if (Directory.Exists(options.FileOrDirectory) && options.Extensions != null)
            {
                foreach (string extension in options.Extensions.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    // Allow extension with and without a dot
                    string searchPattern = "*." + extension.TrimStart('.');

                    string[] extensionfilepaths = Directory.GetFiles(options.FileOrDirectory, searchPattern, SearchOption.AllDirectories);
                    filepaths.AddRange(extensionfilepaths);
                }
            }

            XamlFormatter xamlFormatter = new XamlFormatter(options.CreateXamlFormatterOptions());
            FormatFiles(xamlFormatter, filepaths.ToArray());
        }

        public static void FormatFiles(XamlFormatter xamlFormatter, string[] filepaths)
        {
            foreach (string path in filepaths)
            {
                string text = File.ReadAllText(path);
                XamlText xamlText = new XamlText(text);

                string formattedText = xamlFormatter.Format(xamlText);
                File.WriteAllText(path, formattedText);
            }
        }
    }
}
