using EnvDTE;
using FormatXamlExtension.Configuration;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using System;

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

            // Load settings
            int indentSize = dte.ActiveDocument.IndentSize;

            XamlFormatter xamlFormatter = new XamlFormatter(indentSize);
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