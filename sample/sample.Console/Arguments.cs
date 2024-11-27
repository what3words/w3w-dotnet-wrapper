/**
 * A simple utility class to parse command line arguments.
 */
using System;

namespace sample.Console.Utils
{
    public class Arguments
    {
        private readonly string[] args;
        private readonly string command;
        public Arguments(string[] args)
        {
            command = ResolveCommand(args);
            this.args = args;
        }

        private static string ResolveCommand(string[] args)
        {
            return string.IsNullOrEmpty(args[0]) || args[0].StartsWith("--", StringComparison.Ordinal)
                ? args[0].StartsWith("--", StringComparison.Ordinal) && args[0].Equals("--help", StringComparison.OrdinalIgnoreCase)
                    ? "help"
                    : throw new InvalidOperationException("Invalid command provided.")
                : args[0];
        }

        public string GetArgument(string name)
        {
            if (args == null || args.Length == 0)
            {
                throw new InvalidOperationException("No arguments provided.");
            }
            var index = Array.IndexOf(args, name);
            return index >= 0 && index + 1 < args.Length && !string.IsNullOrEmpty(args[index + 1]) && !args[index + 1].StartsWith("--", StringComparison.Ordinal)
                ? args[index + 1]
                : throw new InvalidOperationException($"{name} is missing or not provided.");
        }

        public string GetCommand() => command;
    }
}