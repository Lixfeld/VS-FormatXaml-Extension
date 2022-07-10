using EnvDTE;
using FormatXamlExtension.Classes;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;

namespace FormatXamlExtension.EventHandler
{
    internal class BeforeSaveEventHandler : RunningDocTableEventsHandler
    {
        private readonly DTE dte;

        public BeforeSaveEventHandler(DTE dte)
        {
            this.dte = dte;
        }

        public override int OnBeforeSave(uint _)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            FormatService formatService = new FormatService(dte);
            formatService.FormatActiveDocument();

            return VSConstants.S_OK;
        }
    }
}