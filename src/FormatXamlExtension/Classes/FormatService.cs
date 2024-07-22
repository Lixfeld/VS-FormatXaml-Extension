using EnvDTE;
using FormatXaml;
using FormatXamlExtension.Configuration;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using System;
using Constants = FormatXaml.Constants;

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
            XamlFormatterOptions xamlFormatterOptions = vsOptions.CreateXamlFormatterOptions(indentSize);

            XamlFormatter xamlFormatter = new XamlFormatter(xamlFormatterOptions);
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

            if (vsOptions.Configuration == IndentationConfiguration.EditorConfig)
            {
                string filepath = dte.ActiveDocument.FullName;
                if (EditorConfigHelper.TryGetIndentationSize(filepath, out int indentSize))
                {
                    return indentSize;
                }
            }
            else if (vsOptions.Configuration == IndentationConfiguration.VisualStudio)
            {
                Properties properties = dte.Properties[Constants.TextEditorCategory, Constants.XAMLPage];
                string indentSizeAsString = properties.Item(Constants.IndentSizeItem).Value.ToString();

                if (int.TryParse(indentSizeAsString, out int indentSize) && indentSize > 0)
                {
                    return indentSize;
                }
            }
            else if (vsOptions.Configuration == IndentationConfiguration.Custom)
            {
                return vsOptions.CustomIndentSize;
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