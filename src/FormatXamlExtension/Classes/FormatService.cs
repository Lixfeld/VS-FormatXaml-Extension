using EditorConfig.Core;
using EnvDTE;
using FormatXamlExtension.Configuration;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;

namespace FormatXamlExtension.Classes
{
    internal class FormatService
    {
        private readonly DTE dte;
        private readonly VSOptions vsOptions;

        public FormatService(DTE dte, VSOptions vsOptions)
        {
            this.dte = dte;
            this.vsOptions = vsOptions;
        }

        public void FormatActiveDocument()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!ShouldFormat())
                return;

            IWpfTextView textView = TextViewHelper.GetActiveTextView();
            string text = TextViewHelper.GetText(textView);
            XamlText xamlText = new XamlText(text);

            int indentSize = GetIndentationSize();
            XamlFormatter xamlFormatter = new XamlFormatter(indentSize, vsOptions);
            string newText = xamlFormatter.Format(xamlText);

            try
            {
                dte.UndoContext.Open("Format xaml");
                TextViewHelper.ReplaceText(textView, newText);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
            finally
            {
                dte.UndoContext.Close();
            }
        }

        private int GetIndentationSize()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            EditorConfigParser parser = new EditorConfigParser();
            FileConfiguration configuration = parser.Parse(dte.ActiveDocument.FullName);
            IReadOnlyDictionary<string, string> properties = configuration.Properties;

            if (properties.TryGetValue(Constants.IndentSizeKey, out string indentSizeAsString))
            {
                if (int.TryParse(indentSizeAsString, out int indentSize) && indentSize > 0)
                {
                    return indentSize;
                }
            }

            return Constants.DefaultIndentSize;
        }

        private bool ShouldFormat()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            string name = dte.ActiveDocument.Name;
            if (!string.IsNullOrWhiteSpace(vsOptions.FileExtensions))
            {
                char[] seperators = new char[] { ' ', ';' };
                string[] fileExtensions = vsOptions.FileExtensions.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                foreach (string extension in fileExtensions)
                {
                    if (name.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
            }

            return false;
        }
    }
}