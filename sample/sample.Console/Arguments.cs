namespace sample.Console.Utils {
  using System;
  public class Arguments {
        private string[] _args;
        private string _command;
        public Arguments(string[] args) {
            _command = this.ResolveCommand(args);
            _args = args;
        }

        private string ResolveCommand(string[] args) {
            if (string.IsNullOrEmpty(args[0]) || args[0].StartsWith("--")) {
                throw new InvalidOperationException("Invalid command provided.");
            }
            return args[0];
        }

        public string GetArgument(string name) {
            if (_args == null || _args.Length == 0) {
                throw new InvalidOperationException("No arguments provided.");
            }
            int index = Array.IndexOf(_args, name);
            if (index >= 0 && index + 1 < _args.Length && !string.IsNullOrEmpty(_args[index + 1]) && !_args[index + 1].StartsWith("--")) {
                return _args[index + 1];
            }
            throw new InvalidOperationException($"{name} is missing or not provided.");
        }

        public string GetCommand() => _command;
    }
}