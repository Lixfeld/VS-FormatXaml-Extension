using EditorConfig.Core;
using System.Collections.Generic;

namespace FormatXaml
{
    public static class EditorConfigHelper
    {
        public static bool TryGetIndentationSize(string filepath, out int indentSize)
        {
            EditorConfigParser parser = new EditorConfigParser();
            FileConfiguration configuration = parser.Parse(filepath);
            IReadOnlyDictionary<string, string> properties = configuration.Properties;

            if (properties.TryGetValue(Constants.IndentSizeKey, out string indentSizeAsString)
                && int.TryParse(indentSizeAsString, out indentSize)
                && indentSize > 0)
            {
                return true;
            }

            indentSize = 0;
            return false;
        }
    }
}
