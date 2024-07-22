using CommandLine;

namespace FormatXaml.Tool
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ToolOptions>(args).WithParsed(options =>
            {
                RootCommand rootCommand = new RootCommand(options);
                rootCommand.Execute();
            });
        }
    }
}
