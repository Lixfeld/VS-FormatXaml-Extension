using EnvDTE;
using FormatXamlExtension.Classes;
using FormatXamlExtension.Configuration;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;

namespace FormatXamlExtension.EventHandler
{
    internal class BeforeSaveEventHandler : RunningDocTableEventsHandler
    {
        private readonly DTE dte;
        private readonly FormatXamlExtensionPackage package;

        public BeforeSaveEventHandler(DTE dte, FormatXamlExtensionPackage package)
        {
            this.dte = dte;
            this.package = package;
        }

        public override int OnBeforeSave(uint _)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            VSOptions vsOptions = package.VSOptions;
            if (vsOptions.ExecuteOnSave)
            {
                FormatService formatService = new FormatService(dte, vsOptions);
                formatService.FormatActiveDocument();
            }

            return VSConstants.S_OK;
        }
    }
}