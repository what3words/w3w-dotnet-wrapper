/**
 * Simple console application using the what3words-dotnet-wrapper.
 */

using what3words.dotnet.wrapper;
using what3words.dotnet.wrapper.models;
using what3words.dotnet.wrapper.response;
using System.Linq;
using System.Globalization;

namespace sample.Console
{
    using System;
    using Utils;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = new Arguments(args);
                if (arguments.GetCommand().Equals("help"))
                {
                    PrintUsage();
                    return;
                }
                string apiKey = arguments.GetArgument("--api-key");
                if (apiKey != null)
                {
                    What3WordsV3 api = new What3WordsV3(apiKey);
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
                            Console.WriteLine("Command is not supported.");
                            PrintUsage();
                            break;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Something went wrong, " + error.Message);
                PrintUsage();
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: <command> [options]");
            Console.WriteLine("Required parameters:");
            Console.WriteLine("  --api-key <key>");
            Console.WriteLine("Commands:");
            Console.WriteLine("  convert-to-coordinates --3wa <3 word address>");
            Console.WriteLine("  convert-to-3wa --lat <latitude> --lng <longitude>");
            Console.WriteLine("  autosuggest --input <input>");
        }

        static void ConvertToCoordinates(What3WordsV3 api, Arguments arg)
        {
            var threeWords = arg.GetArgument("--3wa");
            var result = api.ConvertToCoordinates(threeWords).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                Console.WriteLine("Coordinates: " + result.Data.Coordinates.Lat + ", " + result.Data.Coordinates.Lng);
            }
            else
            {
                Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        static void ConvertTo3WA(What3WordsV3 api, Arguments arg)
        {
            double latitude, longitude;
            var lat = double.TryParse(arg.GetArgument("--lat"), NumberStyles.Any, CultureInfo.InvariantCulture, out latitude) ? latitude : 0.0;
            var lng = double.TryParse(arg.GetArgument("--lng"), NumberStyles.Any, CultureInfo.InvariantCulture, out longitude) ? longitude : 0.0;
            var coordinates = new Coordinates(lat, lng);
            var result = api.ConvertTo3WA(coordinates).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                Console.WriteLine($"3 word address: https://w3w.co/{result.Data.Words}");
            }
            else
            {
                Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }

        static void AutoSuggest(What3WordsV3 api, Arguments arg)
        {
            var input = arg.GetArgument("--input");
            var result = api.Autosuggest(input).RequestAsync().Result;
            if (result.IsSuccessful)
            {
                if (result.Data.Suggestions.Count > 0)
                {
                    Console.WriteLine("Suggestions: " + string.Join(", ", result.Data.Suggestions.Select(x => $"https://w3w.co/{x.Words}")));
                }
                else
                {
                    Console.WriteLine("No suggestions found.");
                }
            }
            else
            {
                Console.WriteLine(result.Error.Code + " - " + result.Error.Message);
            }
        }
    }
}
