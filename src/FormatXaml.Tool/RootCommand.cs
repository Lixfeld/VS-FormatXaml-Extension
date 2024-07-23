using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
            if (!Validate(out Encoding encoding))
                return;

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

            if (filepaths.Count <= 0)
            {
                Console.WriteLine("No files found");
                return;
            }

            XamlFormatter xamlFormatter = new XamlFormatter(options.CreateXamlFormatterOptions());
            FormatFiles(xamlFormatter, filepaths.ToArray(), encoding);
        }

        private bool Validate(out Encoding encoding)
        {
            try
            {
                if (options.EncodingName != null)
                {
                    encoding = Encoding.GetEncoding(options.EncodingName);
                    return true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Encoding '{options.EncodingName}' not found");
            }

            encoding = Encoding.Default;
            return false;
        }

        private void FormatFiles(XamlFormatter xamlFormatter, string[] filepaths, Encoding encoding)
        {
            foreach (string path in filepaths)
            {
                if (options.Verbose)
                {
                    Console.WriteLine(path);
                }

                int indentSize = options.IndentSize;

                string text = File.ReadAllText(path, encoding);
                XamlText xamlText = new XamlText(text);

                string formattedText = xamlFormatter.Format(xamlText, indentSize);
                File.WriteAllText(path, formattedText, encoding);
            }

            if (filepaths.Length == 1)
            {
                Console.WriteLine("1 file formatted");
            }
            else
            {
                Console.WriteLine($"{filepaths.Length} files formatted");
            }
        }
    }
}
