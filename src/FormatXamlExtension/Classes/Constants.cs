﻿namespace FormatXamlExtension.Classes
{
    public enum LineEnding
    {
        Auto,
        CRLF,
        LF
    }

    public class Constants
    {
        public const string UnixLineEnding = "\n";
        public const string WindowsLineEnding = "\r\n";

        public const int DefaultIndentSize = 4;

        // Processing Instruction (PI) Tag: <?...?>
        public const string OpenPITag = "<?";
        public const string ClosePITag = "?>";

        // CommentTag: <!--...-->
        public const string OpenCommentTag = "<!--";
        public const string CloseCommentTag = "-->";

        // EndTag: </...>
        public const string OpenEndTag = "</";

        // EmptyTag: <.../>
        public const string CloseEmptyTag = "/>";

        // StartTag: <...>
        public const string OpenTag = "<";
        public const string CloseTag = ">";
    }
}