/**
 * Simple console application using the what3words-dotnet-wrapper.
 */

using sample.Console.Utils;
using System.Globalization;
using System.Linq;
using what3words.dotnet.wrapper;
using what3words.dotnet.wrapper.models;

namespace sample.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var arguments = new Arguments(args);
                if (arguments.GetCommand().Equals("help"))
                {
                    PrintUsage();
                    return;
                }
                var apiKey = arguments.GetArgument("--api-key");
                if (apiKey != null)
                {
                    var api = new What3WordsV3(apiKey);
                    switch (arguments.GetCommand())
                    {
                        case "convert-to-coordinates":
                            ConvertToCoordinates(api, arguments);
                            break;
                        case "convert-to-3wa":
                            ConvertTo3WA(api, arguments);
                            break;
                        case "autosuggest":
                            AutoSuggest(api, arguments);
                            break;
                        default:
                            System.Console.WriteLine("Command is not supported.");
                            PrintUsage();
                            break;
                    }
                }
            }
            catch (System.Exception error)
            {
                System.Console.WriteLine("Something went wrong, " + error.Message);
                PrintUsage();
            }
        }

        private static void PrintUsage()
        {
            System.Console.WriteLine("Usage: <command> [options]");
            System.Console.WriteLine("Required parameters:");
            System.Console.WriteLine("  --api-key <key>");
            System.Console.WriteLine("Commands:");
            System.Console.WriteLine("  convert-to-coordinates --3wa <3 word address>");
            System.Console.WriteLine("  convert-to-3wa --lat <latitude> --lng <longitude>");
            System.Console.WriteLine("  autosuggest --input <input>");
        }

        private static void ConvertToCoordinates(What3WordsV3 api, Arguments arg)
        {
            var threeWords = arg.GetArgument("--3wa");
            var result = api.ConvertToCoordinates(threeWords).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                System.Console.WriteLine("Coordinates: " + result.Data.Coordinates.Lat + ", " + result.Data.Coordinates.Lng);
            }
            else
            {
                System.Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        private static void ConvertTo3WA(What3WordsV3 api, Arguments arg)
        {
            var lat = double.TryParse(arg.GetArgument("--lat"), NumberStyles.Any, CultureInfo.InvariantCulture, out var latitude) ? latitude : 0.0;
            var lng = double.TryParse(arg.GetArgument("--lng"), NumberStyles.Any, CultureInfo.InvariantCulture, out var longitude) ? longitude : 0.0;
            var coordinates = new Coordinates(lat, lng);
            var result = api.ConvertTo3WA(coordinates).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                System.Console.WriteLine($"3 word address: https://w3w.co/{result.Data.Words}");
            }
            else
            {
                System.Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        private static void AutoSuggest(What3WordsV3 api, Arguments arg)
        {
            var input = arg.GetArgument("--input");
            var result = api.Autosuggest(input).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                if (result.Data.Suggestions.Count > 0)
                {
                    System.Console.WriteLine("Suggestions: " + string.Join(", ", result.Data.Suggestions.Select(x => $"https://w3w.co/{x.Words}")));
                }
                else
                {
                    System.Console.WriteLine("No suggestions found.");
                }
            }
            else
            {
                System.Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }
    }
}
