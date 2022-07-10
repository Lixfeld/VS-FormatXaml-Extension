namespace FormatXamlExtension.Classes
{
    public class Constants
    {
        public const string UnixLineEnding = "\n";
        public const string WindowsLineEnding = "\r\n";

        public const int DefaultIndentSize = 4;

        // Processing Instruction (PI) Tag: <?...?>
        public const string OpenPITag = "<?";
        public const string ClosePITag = "?>";

        // EndTag: </...>
        public const string OpenEndTag = "</";

        // EmptyTag: <.../>
        public const string CloseEmptyTag = "/>";

        // StartTag: <...>
        public const string OpenTag = "<";
        public const string CloseTag = ">";
    }
}