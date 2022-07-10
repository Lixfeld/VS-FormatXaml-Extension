using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using System;

namespace FormatXamlExtension.Classes
{
    public class FormatService
    {
        private readonly DTE dte;

        public FormatService(DTE dte)
        {
            this.dte = dte;
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
            bool shouldFormat = name.EndsWith(".XAML", StringComparison.InvariantCultureIgnoreCase);
            return shouldFormat;
        }
    }
}