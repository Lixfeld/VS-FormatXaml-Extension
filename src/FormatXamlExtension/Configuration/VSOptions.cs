namespace FormatXamlExtension.Configuration
{
    public class VSOptions
    {
        public bool ExecuteOnSave { get; }

        public string FileExtensions { get; }

        public VSOptions(bool executeOnSave, string fileExtensions)
        {
            ExecuteOnSave = executeOnSave;
            FileExtensions = fileExtensions;
        }
    }
}