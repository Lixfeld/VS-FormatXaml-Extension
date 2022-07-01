﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;

namespace FormatXamlExtension.Classes
{
    public static class TextViewHelper
    {
        public static IWpfTextView GetActiveTextView()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            IComponentModel componentModel = Package.GetGlobalService(typeof(SComponentModel)) as IComponentModel;
            if (componentModel == null)
                return null;

            IVsTextView activeView = GetActiveView();
            IVsEditorAdaptersFactoryService vsEditorAdapter = componentModel.GetService<IVsEditorAdaptersFactoryService>();
            return vsEditorAdapter.GetWpfTextView(activeView);
        }

        public static string GetText(IWpfTextView textView)
        {
            ITextSnapshot textSnapshot = textView.TextSnapshot;
            return textSnapshot.GetText();
        }

        public static void ReplaceText(IWpfTextView textView, string newText)
        {
            using (ITextEdit edit = textView.TextBuffer.CreateEdit())
            {
                edit.Replace(0, textView.TextBuffer.CurrentSnapshot.Length, newText);
                edit.Apply();
            }
        }

        private static IVsTextView GetActiveView()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            IVsTextManager vsTextManager = ServiceProvider.GlobalProvider.GetService(typeof(SVsTextManager)) as IVsTextManager;
            Assumes.Present(vsTextManager);

            ErrorHandler.ThrowOnFailure(vsTextManager.GetActiveView(1, null, out IVsTextView activeView));
            return activeView;
        }
    }
}