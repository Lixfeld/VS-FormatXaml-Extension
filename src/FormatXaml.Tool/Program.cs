using CommandLine;
using System;

namespace FormatXaml.Tool
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Parser parser = new Parser(config =>
            {
                config.CaseSensitive = false;
                config.CaseInsensitiveEnumValues = true;
                config.HelpWriter = Console.Out;
            });

            parser.ParseArguments<ToolOptions>(args).WithParsed(options =>
            {
                RootCommand rootCommand = new RootCommand(options);
                rootCommand.Execute();
            });
        }
    }
}
